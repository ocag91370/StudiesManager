using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursHomePage : BasePage
    {
        private static string HomeUrl = @"http://r.mail.cours.fr/tr/cl/uQVn67dGsu3VLVV8kZSVL5BVRagoDxwMejRp-6QpPslJOGZZ7IsABnHusm2CLvDHSJrTYgRE7wAa812MJ-111mkNI_j0KHFqVKHxQnkExhiPSZQdFn78Ac_F6SuRXAgM-ZMF9qGlkOesETjANgizI7i0qiuUKrzN6kPUdy8Rx4QZUB-SyZed_ULyMxB0jMBBddfQVxQqE7Jc099Q5RrkPK7Ue9xIuAzF4fGYkDcDiloiG1Uopfe3zzuzoZwXm88AR--Xo1KLKN733ejt6MufqY8nMVSwQjCiLy1Ub_CP1D7VNCKwwtKluWiRkmb0NaU8VXl5yQ";

        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("choix-des-classes"));

        private IEnumerable<IWebElement> SchoolLevelListElements => ContainerElement.FindElements(By.XPath("//li/a"));

        public MaxicoursHomePage(IWebDriver driver) : base (driver, HomeUrl)
        {
        }

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            return SchoolLevelListElements.Select(o => {
                var url = o.GetAttribute("href");
                var tag = HttpUtility.ParseQueryString(url).Get("cla");

                var result = new SchoolLevelModel {
                    Tag = tag,
                    Name = o.Text,
                    Url = url
                };

                return result;
            }).ToList();
        }

        public SchoolLevelModel GetSchoolLevelByTag(string tag)
        {
            return GetAllSchoolLevels().FirstOrDefault(o => o.Tag == tag);
        }
    }
}
