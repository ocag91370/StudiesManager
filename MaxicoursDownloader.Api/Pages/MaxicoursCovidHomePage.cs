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
        private static string HomeUrl => @"http://r.mail.cours.fr/tr/cl/uQVn67dGsu3VLVV8kZSVL5BVRagoDxwMejRp-6QpPslJOGZZ7IsABnHusm2CLvDHSJrTYgRE7wAa812MJ-111mkNI_j0KHFqVKHxQnkExhiPSZQdFn78Ac_F6SuRXAgM-ZMF9qGlkOesETjANgizI7i0qiuUKrzN6kPUdy8Rx4QZUB-SyZed_ULyMxB0jMBBddfQVxQqE7Jc099Q5RrkPK7Ue9xIuAzF4fGYkDcDiloiG1Uopfe3zzuzoZwXm88AR--Xo1KLKN733ejt6MufqY8nMVSwQjCiLy1Ub_CP1D7VNCKwwtKluWiRkmb0NaU8VXl5yQ";

        public MaxicoursCovidHomePage(IWebDriver driver) : base (driver, HomeUrl)
        {
        }

        public string GetHtml()
        {
            return Driver.PageSource;
        }
    }
}
