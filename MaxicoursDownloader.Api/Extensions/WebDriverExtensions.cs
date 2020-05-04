using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            try
            {
                if (timeoutInSeconds > 0)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    return wait.Until(drv => drv.FindElement(by));
                }

                return driver.FindElement(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds, int nbRetry)
        {
            var count = nbRetry;
            while (count-- > 0)
            {
                var element = FindElement(driver, by, timeoutInSeconds);
                if (element.IsNotNull())
                    return element;
            }

            return null;
            //return driver.FindElement(by);
        }
    }
}
