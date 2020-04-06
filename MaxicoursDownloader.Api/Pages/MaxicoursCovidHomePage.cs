using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursCovidHomePage : BasePage
    {
        public MaxicoursCovidHomePage(IWebDriver driver, string url) : base (driver, url)
        {
        }

        public string GetHtml()
        {
            return Driver.PageSource;
        }
    }
}
