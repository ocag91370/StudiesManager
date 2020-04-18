using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursHomePage : BasePage
    {
        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("choix-des-classes"));

        private IEnumerable<IWebElement> SchoolLevelListElements => ContainerElement.FindElements(By.XPath("//li/a"));

        public MaxicoursHomePage(MaxicoursSettingsModel settings, IWebDriver driver, string url) : base(settings, driver, url)
        {
        }

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            return SchoolLevelListElements.Select(o => {
                var url = o.GetAttribute("href");
                var name = o.GetAttribute("title");
                var tag = url.GetUrlParameter("cla");

                var result = new SchoolLevelModel {
                    Tag = tag,
                    Name = name,
                    Url = url
                };

                return result;
            }).ToList();
        }
    }
}
