using OpenQA.Selenium;
using System;

namespace MaxicoursDownloader.Api.Pages
{
    public class BasePage
    {
        public string UrlPrefix => @"https://entraide-covid19.maxicours.com/";

        public string Url { get; set; }

        public IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Url = string.Empty;
        }

        public BasePage(IWebDriver driver, string url)
        {
            Driver = driver;
            Url = url;

            GoTo();
        }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(Url);
        }
    }
}