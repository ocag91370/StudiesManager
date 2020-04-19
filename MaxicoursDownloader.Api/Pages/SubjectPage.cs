using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SubjectPage : BasePage
    {
        private readonly SummarySubjectEntity _subjectSummary;

        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[@class = 'lsi-arbo']"));

        public SubjectPage(MaxicoursSettingsModel settings, IWebDriver driver, SummarySubjectEntity subjectSummary) : base(settings, driver, subjectSummary.Url)
        {
            _subjectSummary = subjectSummary;
        }

        public SubjectEntity GetSubject()
        {
            return new SubjectEntity
            {
                SubjectSummary = _subjectSummary,
                Themes = GetAllThemes(),
                Categories = GetAllCategories(),
                Items = GetAllItems()
            };
        }
    }
}
