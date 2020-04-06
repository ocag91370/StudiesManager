using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public class MaxicoursHomePage : BasePage
    {
        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("choix-des-classes"));

        private IEnumerable<IWebElement> SchoolLevelListElements => ContainerElement.FindElements(By.XPath("//li/a"));

        public MaxicoursHomePage(IWebDriver driver, string url) : base (driver, url)
        {
        }

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            return SchoolLevelListElements.Select(o => new SchoolLevelModel {
                Name = o.Text,
                Url = o.GetAttribute("href")
            }).ToList();
        }

        public SchoolLevelModel GetSchoolLevel(string level)
        {
            return GetAllSchoolLevels().FirstOrDefault(o => o.Name == level);
        }
    }
}
