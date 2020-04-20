using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using StudiesManager.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public partial class MaxicoursService : IMaxicoursService
    {
        private readonly MaxicoursSettingsModel _maxicoursSettings;
        private readonly IMapper _mapper;
        private IWebDriver Driver;

        public MaxicoursService(IOptions<MaxicoursSettingsModel> configuration, IMapper mapper)
        {
            _maxicoursSettings = configuration.Value;
            _mapper = mapper;

            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        private MaxicoursHomePage GetHomePage()
        {
            var homePage = new MaxicoursHomePage(_maxicoursSettings, Driver, _maxicoursSettings.StartUpUrl);
            Debug.Assert(homePage.IsNotNull());

            return homePage;
        }

        private SchoolLevelPage GetSchoolLevelPage(string levelTag)
        {
            var schoolLevel = GetSchoolLevel(levelTag);

            var schoolLevelPage = new SchoolLevelPage(_maxicoursSettings, Driver, _mapper.Map<SchoolLevelEntity>(schoolLevel));
            Debug.Assert(schoolLevelPage.IsNotNull());

            return schoolLevelPage;
        }

        private SubjectPage GetSubjectPage(SummarySubjectEntity summarySubject)
        {
            var subjectPage = new SubjectPage(_maxicoursSettings, Driver, summarySubject);
            Debug.Assert(subjectPage.IsNotNull());

            return subjectPage;
        }

        public SubjectPage GetSubjectPage(string levelTag, int subjectId)
        {
            var summarySubject = GetSummarySubject(levelTag, subjectId);
            Debug.Assert(summarySubject.IsNotNull());

            var subjectPage = GetSubjectPage(_mapper.Map<SummarySubjectEntity>(summarySubject));
            Debug.Assert(subjectPage.IsNotNull());

            return subjectPage;
        }

        private LessonPage GetLessonPage(ItemEntity item)
        {
            var lessonPage = new LessonPage(Driver, item);
            Debug.Assert(lessonPage.IsNotNull());

            return lessonPage;
        }

        private VideoLessonPage GetVideoLessonPage(ItemEntity item)
        {
            var videoLessonPage = new VideoLessonPage(_maxicoursSettings, Driver, item);
            Debug.Assert(videoLessonPage.IsNotNull());

            return videoLessonPage;
        }

        public SummarySheetPage GetSummarySheetPage(ItemEntity item)
        {
            var summarySheetPage = new SummarySheetPage(_maxicoursSettings, Driver, item);
            Debug.Assert(summarySheetPage.IsNotNull());

            return summarySheetPage;
        }

        public TestPage GetTestPage(ItemEntity item)
        {
            var testPage = new TestPage(_maxicoursSettings, Driver, item);
            Debug.Assert(testPage.IsNotNull());

            return testPage;
        }

        public void Dispose()
        {
            if (Driver == null)
                return;

            Driver.Quit();
            Driver.Dispose();
            Driver = null;
        }
    }
}
