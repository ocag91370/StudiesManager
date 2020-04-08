using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SubjectPage : BasePage
    {
        private readonly SubjectSummaryEntity _subjectSummary;

        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[@class = 'lsi-arbo']"));

        public SubjectPage(IWebDriver driver, SubjectSummaryEntity subjectSummary) : base(driver, subjectSummary.Url)
        {
            _subjectSummary = subjectSummary;
        }

        public SubjectEntity GetSubject()
        {
            return new SubjectEntity
            {
                Header = GetHeader(),
                Themes = GetAllThemes(),
                Categories = GetAllCategories(),
                Lessons = GetAllLessons()
            };
        }
    }
}
