using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public class SchoolLevelPage : BasePage
    {
        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("mes-matieres"));

        private IWebElement TitleElement => ContainerElement.FindElement(By.ClassName("lsi-txt"));

        private IEnumerable<IWebElement> SubjectElementList => ContainerElement.FindElements(By.XPath("//*[@class='td-label']/a"));

        public SchoolLevelPage(IWebDriver driver, string url) : base (driver, url)
        {
        }

        public string Title => TitleElement.Text;

        public List<SubjectModel> GetAllSubjects()
        {
            return SubjectElementList.Select(o => new SubjectModel { Name = o.FindElement(By.TagName("span")).Text, Url = o.GetAttribute("href") }).ToList();
        }
    }
}
