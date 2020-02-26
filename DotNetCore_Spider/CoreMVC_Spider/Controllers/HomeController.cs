using CoreMVC_Spider.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreMVC_Spider.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // 抓取https://cs.newhouse.fang.com/house/s/b91/ 楼盘数据
            var url = @"https://cs.newhouse.fang.com/house/s/b91/";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.157 Safari/537.36");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");


            var handler = new HtmlTextHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip,
            };
            HttpClient client = new HttpClient(handler);

            using (var response = await client.SendAsync(request))
            {
                using (var content = response.Content)
                {
                    var result = await content.ReadAsStringAsync();

                    var document = new HtmlDocument();
                    document.LoadHtml(result);
                    var nodes = document.DocumentNode.SelectNodes("//*[@id='newhouse_loupai_list']/ul");
                    _logger.LogInformation(nodes.FirstOrDefault().InnerHtml);

                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
