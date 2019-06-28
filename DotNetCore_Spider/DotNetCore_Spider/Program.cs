
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

            var url = "https://www.dy2018.com/";
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            //Console.WriteLine(driver.PageSource);

            var elements = driver.FindElements(By.ClassName("co_area2"));
            foreach(var el in elements) {
                var titleEl = el.FindElement(By.ClassName("title_all"));
                var title = titleEl.Text;
                Console.WriteLine(title);
            }


            // driver.Quit();
            Console.Read();

        }
    }
}
