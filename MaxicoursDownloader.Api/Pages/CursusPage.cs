using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public class CursusPage
    {
        private readonly IWebDriver _driver;
        private readonly string _uri;

        private IWebElement Titre => _driver.FindElement(By.Id("Name"));

        public CursusPage(IWebDriver driver, string uri)
        {
            _driver = driver;
            _uri = uri;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_uri);
    }
}
