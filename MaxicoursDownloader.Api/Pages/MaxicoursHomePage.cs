using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Repositories;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursHomePage : BasePage
    {
        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("choix-des-classes"));

        private IEnumerable<IWebElement> SchoolLevelListElements => ContainerElement.FindElements(By.XPath("//li/a"));

        public MaxicoursHomePage(IWebDriver driver) : base (driver, UrlRepository.Urls["Home"])
        {
        }

        public MaxicoursHomePage(IWebDriver driver, string url) : base(driver, url)
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
