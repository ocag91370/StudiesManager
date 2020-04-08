using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Extensions
{
    public static class IWebElementExtensions
    {
        public static bool ExistsElement(this IWebElement @this, By by)
        {
            try
            {
                @this.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
