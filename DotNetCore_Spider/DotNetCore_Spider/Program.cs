using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace DotNetCore_Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".net core 使用 Selenium!");

            var url = "https://www.baidu.com/";
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            Console.WriteLine(driver.PageSource);



            driver.Quit();
            Console.Read();

        }
    }
}
