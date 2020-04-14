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
        private readonly SchoolLevelEntity _schoolLevelEntity;

        private IWebElement ContainerElement => Driver.FindElement(By.ClassName("mes-matieres"));

        private IWebElement TitleElement => ContainerElement.FindElement(By.ClassName("lsi-txt"));

        private IEnumerable<IWebElement> SubjectElementList => ContainerElement.FindElements(By.XPath("//*[@class='td-label']/a"));

        public SchoolLevelPage(IWebDriver driver, SchoolLevelEntity schoolLevelEntity) : base(driver, schoolLevelEntity.Url)
        {
            _schoolLevelEntity = schoolLevelEntity;
        }

        public string Title => TitleElement.Text;

        public List<SubjectSummaryEntity> GetAllSubjects()
        {
            return SubjectElementList.Select(o => GetSubjectSummary(o)).ToList();
        }

        private SubjectSummaryEntity GetSubjectSummary(IWebElement subjectElement)
        {
            var url = subjectElement.GetAttribute("href");
            var name = subjectElement.GetAttribute("title");

            var reference = FromUrl(url);
            _schoolLevelEntity.Id = reference.SchoolLevelId;
            return new SubjectSummaryEntity
            {
                SchoolLevel = _schoolLevelEntity,
                Id = reference.SubjectId,
                Tag = name.CleanName(),
                Name = name,
                Url = url
            };
        }
    }
}
