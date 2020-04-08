using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
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

        public SchoolLevelPage(IWebDriver driver, string url) : base(driver, url)
        {
        }

        public string Title => TitleElement.Text;

        public List<SubjectSummaryEntity> GetAllSubjects()
        {
            return SubjectElementList.Select(o => GetSubjectSummary(o)).ToList();
        }

        private SubjectSummaryEntity GetSubjectSummary(IWebElement subjectElement)
        {
            var url = subjectElement.GetAttribute("href");
            var ids = url.SplitUrl();
            var name = subjectElement.FindElement(By.TagName("span")).Text;

            return new SubjectSummaryEntity
            {
                SchoolLevelId = ids.First(),
                SubjectId = ids.Last(),
                Name = name,
                Url = url
            };
        }
    }
}
