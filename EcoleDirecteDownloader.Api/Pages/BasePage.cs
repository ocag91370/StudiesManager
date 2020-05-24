using EcoleDirecteDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Web;

namespace EcoleDirecteDownloader.Api.Pages
{
    public class BasePage
    {
        public string Url { get; set; }

        protected readonly EcoleDirecteSettingsModel _ecoleDirecteSettings;

        public IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Url = string.Empty;
            _ecoleDirecteSettings = null;
        }

        public BasePage(EcoleDirecteSettingsModel ecoleDirecteSettings, IWebDriver driver, string url)
        {
            _ecoleDirecteSettings = ecoleDirecteSettings;
            Driver = driver;
            Url = url;

            GoTo();
        }

        public BasePage(IWebDriver driver, string url)
        {
            _ecoleDirecteSettings = null;
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