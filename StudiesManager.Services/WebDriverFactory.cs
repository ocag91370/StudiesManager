using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace StudiesManager.Services
{
    public static class WebDriverFactory
    {
        /// <summary>
        /// Initilizes IWebDriver base on the given WebBrowser name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IWebDriver CreateWebDriver(WebBrowserType name)
        {
            switch (name)
            {
                //case WebBrowserType.Firefox:
                //    return new FirefoxDriver();
                //case WebBrowserType.IE:
                //case WebBrowserType.InternetExplorer:
                //    InternetExplorerOptions ieOption = new InternetExplorerOptions();
                //    ieOption.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                //    ieOption.EnsureCleanSession = true;
                //    ieOption.RequireWindowFocus = true;
                //    return new InternetExplorerDriver(@"./", ieOption);
                //case "safari":
                //    return new RemoteWebDriver(new Uri("http://mac-ip-address:the-opened-port"), DesiredCapabilities.Safari());
                case WebBrowserType.Chrome:
                default:
                    var chromeOption = new ChromeOptions();
                    chromeOption.AddArguments("--disable-extensions");
                    return new ChromeDriver(chromeOption);
            }
        }
    }
}
