using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StudiesManager.Services.Models;
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
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--disable-extensions");
                    chromeOptions.AddArguments("start-maximized");
                    chromeOptions.AddUserProfilePreference("download.default_directory", @"C:\Perso\Export");
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    return new ChromeDriver(chromeOptions);
            }
        }

        /// <summary>
        /// Initilizes IWebDriver base on the given WebBrowser name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IWebDriver CreateWebDriver(WebBrowserType name, WebDriverSettingsModel settings)
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
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--disable-extensions");
                    if (!string.IsNullOrWhiteSpace(settings.DownloadDefaultDirectory))
                        chromeOptions.AddUserProfilePreference("download.default_directory", settings.DownloadDefaultDirectory);
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    return new ChromeDriver(chromeOptions);
            }
        }
    }
}
