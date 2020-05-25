using EcoleDirecteDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Web;

namespace EcoleDirecteDownloader.Api.Pages
{
    public class BasePom
    {
        public string Url { get; set; }

        protected readonly EcoleDirecteSettingsModel _ecoleDirecteSettings;

        public IWebDriver Driver { get; set; }

        public BasePom(IWebDriver driver)
        {
            Driver = driver;
            Url = string.Empty;
            _ecoleDirecteSettings = null;
        }

        public BasePom(EcoleDirecteSettingsModel ecoleDirecteSettings, IWebDriver driver, string url)
        {
            _ecoleDirecteSettings = ecoleDirecteSettings;
            Driver = driver;
            Url = url;

            GoTo();
        }

        public BasePom(IWebDriver driver, string url)
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