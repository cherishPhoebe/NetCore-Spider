using CoreMVC_Spider.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
            var urlList = new List<string>();
            var houseList = new List<HouseViewModel>();

            var baseUrl = @"https://cs.newhouse.fang.com/house/s/b9";
            for (int i = 1; i <= 1; i++)
            {
                urlList.Add(baseUrl + i);
            }

            HttpClient client = _httpClientFactory.CreateClient("House");
            foreach (var url in urlList)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                using (var response = await client.SendAsync(request))
                {
                    using (var content = response.Content)
                    {
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
                                        houseModel.Url = "https:" + titleNodes.First().GetAttributeValue("href", "");
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
                    }
                }
            }

            foreach (var house in houseList)
            {
                var houseHomeRequest = new HttpRequestMessage(HttpMethod.Get, house.Url);

                using (var homeResponse = await client.SendAsync(houseHomeRequest))
                {
                    using (var content = homeResponse.Content)
                    {
                        var result = await content.ReadAsStringAsync();
                        var document = new HtmlDocument();
                        document.LoadHtml(result);

                        var detailLinkNodes = document.DocumentNode.SelectNodes("//*[@id=\"orginalNaviBox\"]/a[2]");
                        if (detailLinkNodes != null && detailLinkNodes.Count > 0)
                        {
                            house.HomeUrl = "https:" + detailLinkNodes.First().GetAttributeValue("href", "");
                            Regex reg = new Regex(@"house.{1}(\d*).{1}housedetail", RegexOptions.IgnoreCase);
                            var id = reg.Match(house.HomeUrl).Groups.LastOrDefault()?.Value;
                            house.Id = id;

                            var detailRequest = new HttpRequestMessage(HttpMethod.Get, house.HomeUrl);

                            using (var detailResponse = await client.SendAsync(detailRequest))
                            {
                                using (var detailContent = detailResponse.Content)
                                {
                                    var detailResult = await detailContent.ReadAsStringAsync();
                                    var detailDoc = new HtmlDocument();
                                    detailDoc.LoadHtml(detailResult);
                                    // 基础信息
                                    var baseInfoNode = detailDoc.DocumentNode.SelectNodes("//div[@class=\"main-item\"]")?[0];
                                    var pointNodes = baseInfoNode?.SelectNodes(".//a/span[2]");
                                    house.Point = pointNodes?.FirstOrDefault()?.InnerText;
                                    var baseInfoLiNodes = baseInfoNode?.SelectNodes(".//ul/li");
                                    var baseInfoDic = new Dictionary<string, string>();
                                    for (int i = 0; i < 8; i++)
                                    {
                                        var keyNode = baseInfoLiNodes?[i].SelectSingleNode(".//div[1]");
                                        var valueNode = baseInfoLiNodes?[i].SelectSingleNode(".//div[2]");
                                        if (keyNode is null)
                                            break;
                                        baseInfoDic.Add(keyNode.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("：", ""),
                                            valueNode.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                                    }
                                    house.BaseInfoJson = JsonConvert.SerializeObject(baseInfoDic);

                                    //销售信息
                                    var saleInfoNode = detailDoc.DocumentNode.SelectNodes("//div[@class=\"main-item\"]")?[1];
                                    var saleInfoLiNodes = saleInfoNode?.SelectNodes(".//ul/li");
                                    var saleInfoDic = new Dictionary<string, string>();
                                    for (int i = 0; i < (saleInfoLiNodes?.Count ?? 0) - 1; i++)
                                    {
                                        var keyNode = saleInfoLiNodes?[i].SelectSingleNode(".//div[1]");
                                        var valueNode = saleInfoLiNodes?[i].SelectSingleNode(".//div[2]");
                                        if (keyNode is null)
                                            break;
                                        saleInfoDic.Add(keyNode.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("：", ""),
                                            valueNode.InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                                    }
                                    house.SaseInfoJson = JsonConvert.SerializeObject(saleInfoDic);


                                    // 预售信息
                                    var preSaleTables = saleInfoNode?.SelectNodes(".//table");
                                    var perSaleTrs = preSaleTables?.LastOrDefault()?.SelectNodes(".//tr");
                                    var perSaleList = new List<PerSaleInfo>();
                                    for (int i = 1; i < perSaleTrs.Count; i++)
                                    {
                                        var perSaleInfo = new PerSaleInfo();
                                        perSaleInfo.License = perSaleTrs[i].SelectNodes(".//td[1]")?.FirstOrDefault()?.InnerText;
                                        perSaleInfo.IssueDate = perSaleTrs[i].SelectNodes(".//td[2]")?.FirstOrDefault()?.InnerText;
                                        perSaleInfo.BindBuilding = perSaleTrs[i].SelectNodes(".//td[3]")?.FirstOrDefault()?.InnerText;
                                        perSaleList.Add(perSaleInfo);
                                    }
                                    house.PerSaleList = perSaleList;

                                    // 价格信息

                                    var priceTable = detailDoc.DocumentNode.SelectNodes("//div[@class=\"main-item\"]")?[4]?.SelectNodes(".//table");
                                    var priceTrs = priceTable?.LastOrDefault()?.SelectNodes(".//tr");
                                    var priceList = new List<PriceInfo>();
                                    foreach (var tr in priceTrs)
                                    {
                                        var priceInfo = new PriceInfo();
                                        priceInfo.RecordDate = tr.SelectNodes(".//td[1]")?.FirstOrDefault()?.InnerText;
                                        priceInfo.AvgPrice = tr.SelectNodes(".//td[2]")?.FirstOrDefault()?.InnerText.Replace("&nbsp;","");
                                        priceInfo.StartingPrice = tr.SelectNodes(".//td[3]")?.FirstOrDefault()?.InnerText.Replace("&nbsp;", "");
                                        priceInfo.PriceDescription = tr.SelectNodes(".//td[4]")?.FirstOrDefault()?.InnerText;
                                        priceList.Add(priceInfo);
                                    }
                                    house.PriceList = priceList;


                                }

                            }
                        }
                    }
                }
            }


            _logger.LogInformation(JsonConvert.SerializeObject(houseList));

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
