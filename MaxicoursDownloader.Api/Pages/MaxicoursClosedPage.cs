using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursClosedPage : BasePage
    {
        private static string HomeUrl = @"http://r.mail.cours.fr/tr/cl/uQVn67dGsu3VLVV8kZSVL5BVRagoDxwMejRp-6QpPslJOGZZ7IsABnHusm2CLvDHSJrTYgRE7wAa812MJ-111mkNI_j0KHFqVKHxQnkExhiPSZQdFn78Ac_F6SuRXAgM-ZMF9qGlkOesETjANgizI7i0qiuUKrzN6kPUdy8Rx4QZUB-SyZed_ULyMxB0jMBBddfQVxQqE7Jc099Q5RrkPK7Ue9xIuAzF4fGYkDcDiloiG1Uopfe3zzuzoZwXm88AR--Xo1KLKN733ejt6MufqY8nMVSwQjCiLy1Ub_CP1D7VNCKwwtKluWiRkmb0NaU8VXl5yQ";

        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("main"));

        public MaxicoursClosedPage(IWebDriver driver) : base(driver, HomeUrl)
        {
        }

        public string GetHtml()
        {
            return Driver.PageSource;
            //var js = Driver as IJavaScriptExecutor;
            //if (js == null)
            //    return string.Empty;

            //var element = Driver.FindElement(By.ClassName("main"));
            //return (string)js.ExecuteScript("return arguments[0].innerHTML;", element);
        }
    }
}
