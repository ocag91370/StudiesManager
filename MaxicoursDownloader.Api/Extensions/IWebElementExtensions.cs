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

        public static string GetInnerHtml(this IWebElement @this)
        {
            try
            {
                var result = @this.GetAttribute("innerHTML").Replace("\r", "").Replace("\n", "")    ;

                return result;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public static string GetOuterHtml(this IWebElement @this)
        {
            try
            {
                var result = @this.GetAttribute("outerHTML").Replace("\r", "").Replace("\n", "");

                return result;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }
}
