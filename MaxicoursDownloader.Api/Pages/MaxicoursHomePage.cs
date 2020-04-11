using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursHomePage : BasePage
    {
        private static string HomeUrl = @"http://r.mail.cours.fr/tr/cl/uQVn67dGsu3VLVV8kZSVL5BVRagoDxwMejRp-6QpPslJOGZZ7IsABnHusm2CLvDHSJrTYgRE7wAa812MJ-111mkNI_j0KHFqVKHxQnkExhiPSZQdFn78Ac_F6SuRXAgM-ZMF9qGlkOesETjANgizI7i0qiuUKrzN6kPUdy8Rx4QZUB-SyZed_ULyMxB0jMBBddfQVxQqE7Jc099Q5RrkPK7Ue9xIuAzF4fGYkDcDiloiG1Uopfe3zzuzoZwXm88AR--Xo1KLKN733ejt6MufqY8nMVSwQjCiLy1Ub_CP1D7VNCKwwtKluWiRkmb0NaU8VXl5yQ";
        //private static string HomeUrl = @"https://entraide-covid19.maxicours.com/LSI/prod/Accueil/?_eid=fcafrfk6crdp0qmdnk3jllja16";

        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("choix-des-classes"));

        private IEnumerable<IWebElement> SchoolLevelListElements => ContainerElement.FindElements(By.XPath("//li/a"));

        public MaxicoursHomePage(IWebDriver driver) : base (driver, HomeUrl)
        {
        }

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            return SchoolLevelListElements.Select(o => {
                var url = o.GetAttribute("href");
                var tag = url.GetUrlParameter("cla");

                var result = new SchoolLevelModel {
                    Tag = tag,
                    Name = o.Text,
                    Url = url
                };

                return result;
            }).ToList();
        }
    }
}
