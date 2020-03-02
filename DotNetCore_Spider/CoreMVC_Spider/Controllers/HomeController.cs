using CoreMVC_Spider.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            
            HttpClient client = _httpClientFactory.CreateClient("House");

            using (var response = await client.SendAsync(request))
            {
                using (var content = response.Content)
                {
                    var houseList = new List<HouseViewModel>();

                    var result = await content.ReadAsStringAsync();
                    var document = new HtmlDocument();
                    document.LoadHtml(result);
                    var nodes = document.DocumentNode.SelectNodes("//*[@id='newhouse_loupai_list']/ul");
                    if (nodes != null && nodes.Count > 0)
                    {
                        var loupanUlNode = nodes.First();
                        var loupanLiNodes = loupanUlNode.SelectNodes(".//li");
                        foreach (var li in loupanLiNodes)
                        {
                            var houseModel = new HouseViewModel();
                            var titleNodes = li.SelectNodes(".//div[@class=\"nlcd_name\"]/a");
                            var priceNodes = li.SelectNodes(".//div[@class=\"nhouse_price\"]/span");
                            var houseTypeNodes = li.SelectNodes(".//div[@class=\"house_type clearfix\"]");
                            var addressNodes = li.SelectNodes(".//div[@class=\"address\"]");
                            var telNodes = li.SelectNodes(".//div[@class=\"tel\"]");
                            var buildingTypeNodes = li.SelectNodes(".//div[@class=\"fangyuan\"]/a");

                            if (titleNodes != null && titleNodes.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(titleNodes.First().InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "")))
                                {
                                    houseModel.Name = titleNodes.First().InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                    houseModel.Price = priceNodes?.FirstOrDefault()?.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                    houseModel.RoomType = houseTypeNodes?.FirstOrDefault()?.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                    houseModel.Address = addressNodes?.FirstOrDefault()?.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                    houseModel.Tel = telNodes?.FirstOrDefault()?.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");

                                    List<string> buildingTypeList = new List<string>();
                                    foreach (var buildingType in buildingTypeNodes)
                                    {
                                        buildingTypeList.Add(buildingType?.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                                    }
                                    houseModel.BuildingType = string.Join(",", buildingTypeList);

                                    houseList.Add(houseModel);
                                }
                            }
                        }
                    }
                    _logger.LogInformation(JsonConvert.SerializeObject(houseList));
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
