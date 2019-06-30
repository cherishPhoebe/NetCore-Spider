
using DotNetCore_Spider.Model;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace DotNetCore_Spider
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(".net core 使用 Selenium!");


            JobCrawler();

            Console.Read();

        }

        /// <summary>
        /// 爬取51job招聘信息
        /// </summary>
        public static  void JobCrawler() {
            var url = "https://www.51job.com/";
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            var keywordEl = driver.FindElement(By.Id("kwdselectid"));
            keywordEl.SendKeys(".net");
            var searchButton = driver.FindElement(By.XPath("/html/body/div[3]/div/div[1]/div/button"));
            searchButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
            wait.Until(d => d.FindElement(By.Id("resultList")));

            var results = driver.FindElement(By.Id("resultList"));
            var els = results.FindElements(By.ClassName("el")).ToList();
            els.RemoveAt(0);
            foreach (var el in els) {
                var jobModel = new JobModel();
                jobModel.Id = Convert.ToInt32(el.FindElement(By.ClassName("t1")).FindElement(By.Name("delivery_jobid")).GetAttribute("value"));
                var titleEl = el.FindElement(By.XPath(".//span/a"));
                jobModel.JobName = titleEl.Text;
                jobModel.JobUrl = titleEl.GetAttribute("href");
                jobModel.CompanyName = el.FindElement(By.ClassName("t2")).Text;
                jobModel.CompanyUrl = el.FindElement(By.ClassName("t2")).FindElement(By.XPath(".//a")).GetAttribute("href");
                jobModel.JobAddress = el.FindElement(By.ClassName("t3")).Text;
                jobModel.PublishDate = el.FindElement(By.ClassName("t4")).Text;

                Console.WriteLine(JsonConvert.SerializeObject(jobModel));

            }

        }

        public static void DyCrawler() {

            var url = "https://www.dy2018.com/";
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            //Console.WriteLine(driver.PageSource);

            var elements = driver.FindElements(By.ClassName("co_area2"));
            foreach (var el in elements)
            {
                // //*[@id="header"]/div/div[3]/div[4]/div[1]/div[1]/p[1]/span/a
                var titleEl = el.FindElements(By.XPath(".//p[1]/span")).FirstOrDefault();
                var title = titleEl?.Text;
                Console.WriteLine(title);

                var lis = el.FindElements(By.TagName("li"));
                foreach (var item in lis)
                {
                    var a = item.FindElement(By.TagName("a"));
                    var muri = a.GetAttribute("href");

                    Console.WriteLine(a.Text + "----" + muri);

                    //IWebDriver mvDriver = new ChromeDriver();
                    //mvDriver.Navigate().GoToUrl(muri);
                    //Console.WriteLine(mvDriver.PageSource);

                    driver.Navigate().GoToUrl(muri);

                    var zoomEl = driver.FindElement(By.Id("Zoom"));
                    Console.WriteLine(zoomEl.Text);
                }
                Console.WriteLine();
            }


            // driver.Quit();
        }
    }
}
