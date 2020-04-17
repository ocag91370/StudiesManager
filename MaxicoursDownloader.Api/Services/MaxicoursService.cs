using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using MaxicoursDownloader.Api.Repositories;
using OpenQA.Selenium;
using StudiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public class MaxicoursService : IMaxicoursService
    {
        private readonly IMapper _mapper;
        private IWebDriver Driver;

        public MaxicoursService(IMapper mapper)
        {
            _mapper = mapper;

            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        #region SchoolLevel

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            //var homePage = new MaxicoursHomePage(Driver);
            var homePage = new MaxicoursHomePage(Driver, UrlRepository.Urls["HomeWithToken"]);

            var result = homePage.GetAllSchoolLevels();

            return result;
        }

        public SchoolLevelModel GetSchoolLevel(string levelTag)
        {
            var schoolLevelList = GetAllSchoolLevels();

            var result = schoolLevelList.FirstOrDefault(o => o.Tag.IsSameAs(levelTag));

            return result;
        }

        #endregion

        #region Subject

        public List<SubjectSummaryModel> GetAllSubjects(string levelTag)
        {
            var schoolLevel = GetSchoolLevel(levelTag);

            var schoolLevelPage = new SchoolLevelPage(Driver, _mapper.Map<SchoolLevelEntity>(schoolLevel));
            var result = _mapper.Map<List<SubjectSummaryModel>>(schoolLevelPage.GetAllSubjects());

            return result;
        }

        public SubjectModel GetSubject(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.Id == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var subject = _mapper.Map<SubjectModel>(subjectPage.GetSubject());

            return subject;
        }

        public List<ThemeModel> GetAllThemes(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.Id == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ThemeModel>>(subjectPage.GetAllThemes());

            return result;
        }

        public List<CategoryModel> GetAllCategories(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.Id == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<CategoryModel>>(subjectPage.GetAllCategories());

            return result;
        }

        public List<ItemModel> GetAllItems(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.Id == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ItemModel>>(subjectPage.GetAllItems());

            return result;
        }

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.Id == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ItemModel>>(subjectPage.GetItemsOfCategory(categoryId));

            return result;
        }

        public ItemModel GetItem(string levelTag, int subjectId, string categoryId, int itemId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.Id == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<ItemModel>(subjectPage.GetItem(categoryId, itemId));

            return result;
        }

        #endregion

        #region Lesson

        public List<ItemModel> GetLessons(string levelTag, int subjectId)
        {
            return GetItemsOfCategory(levelTag, subjectId, CategoryRepository.Types["lesson"]);
        }

        public LessonModel GetLesson(string levelTag, int subjectId, int lessonId)
        {
            var item = GetItem(levelTag, subjectId, CategoryRepository.Types["lesson"], lessonId);

            var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(item));

            var result = _mapper.Map<LessonModel>(lessonPage.GetLesson());

            return result;
        }

        public LessonModel GetLesson(ItemModel item)
        {
            var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(item));

            var result = _mapper.Map<LessonModel>(lessonPage.GetLesson());

            return result;
        }

        #endregion

        #region Summary Sheets

        public List<ItemModel> GetSummarySheets(string levelTag, int subjectId)
        {
            return GetItemsOfCategory(levelTag, subjectId, CategoryRepository.Types["summary_sheet"]);
        }

        public SummarySheetModel GetSummarySheet(string levelTag, int subjectId, int summarySheetId)
        {
            var item = GetItem(levelTag, subjectId, CategoryRepository.Types["summary_sheet"], summarySheetId);

            var summarySheetPage = new SummarySheetPage(Driver, _mapper.Map<ItemEntity>(item));

            var result = _mapper.Map<SummarySheetModel>(summarySheetPage.GetSummarySheet());

            return result;
        }

        public SummarySheetModel GetSummarySheet(ItemModel item)
        {
            var summarySheetPage = new SummarySheetPage(Driver, _mapper.Map<ItemEntity>(item));

            var result = _mapper.Map<SummarySheetModel>(summarySheetPage.GetSummarySheet());

            return result;
        }

        #endregion

        #region Tests

        public List<ItemModel> GetTests(string levelTag, int subjectId)
        {
            return GetItemsOfCategory(levelTag, subjectId, CategoryRepository.Types["test"]);
        }

        #endregion

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
