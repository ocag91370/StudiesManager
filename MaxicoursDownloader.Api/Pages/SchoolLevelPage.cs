using MaxicoursDownloader.Api.Extensions;
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

        public SchoolLevelPage(IWebDriver driver, string url) : base(driver, url)
        {
        }

        public string Title => TitleElement.Text;

        public List<SubjectModel> GetAllSubjects()
        {
            return SubjectElementList.Select(o => GetSubject(o)).ToList();
        }

        private SubjectModel GetSubject(IWebElement subjectElement)
        {
            var url = subjectElement.GetAttribute("href");
            var ids = url.SplitUrl();

            var model = new SubjectModel
            {
                SchoolLevelId = ids[0],
                SubjectId = ids[1],
                Name = subjectElement.FindElement(By.TagName("span")).Text,
                Url = url
            };

            //var urlSplitted = url.Replace("https://entraide-covid19.maxicours.com/LSI/prod/Arbo/home/bo/", "").Replace("?", "/").Split('/');
            //int.TryParse(urlSplitted[0], out var schoolLevelId);
            //int.TryParse(urlSplitted[1], out var subjectId);

            //var model = new SubjectModel
            //{
            //    SchoolLevelId = schoolLevelId,
            //    SubjectId = subjectId,
            //    Name = subjectElement.FindElement(By.TagName("span")).Text,
            //    Url = url
            //};

            return model;
        }
    }
}
