using MaxicoursDownloader.Api.Entities;
using StudiesManager.Common.Extensions;
using MaxicoursDownloader.Models;
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

        private IEnumerable<IWebElement> SubjectElementList => Driver.FindElements(By.XPath("//*[@class = 'mes-matieres']//a[*[@class='matiere-label']]"));

        public SchoolLevelPage(MaxicoursSettingsModel settings, IWebDriver driver, SchoolLevelEntity schoolLevelEntity) : base(settings, driver, schoolLevelEntity.Url)
        {
            _schoolLevelEntity = schoolLevelEntity;
        }

        public List<SummarySubjectEntity> GetAllSummarySubjects()
        {
            return SubjectElementList.Select(o => GetSummarySubject(o)).ToList();
        }

        private SummarySubjectEntity GetSummarySubject(IWebElement subjectElement)
        {
            var url = subjectElement.GetAttribute("href");
            var name = subjectElement.GetAttribute("title");

            var reference = FromUrl(url);
            _schoolLevelEntity.Id = reference.SchoolLevelId;
            return new SummarySubjectEntity
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
