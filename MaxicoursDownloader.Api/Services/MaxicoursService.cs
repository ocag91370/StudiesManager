using AutoMapper;
using IronPdf;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StudiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Services
{
    public class MaxicoursService : IMaxicoursService, IDisposable
    {
        private readonly IMapper _mapper;
        private readonly IPdfConverterService _pdfConverterService;
        private IWebDriver Driver;

        public MaxicoursService(IMapper mapper, IPdfConverterService pdfConverterService)
        {
            _mapper = mapper;
            _pdfConverterService = pdfConverterService;

            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        #region SchoolLevel

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            var homePage = new MaxicoursHomePage(Driver);

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
            //var schoolLevel = GetSchoolLevel(levelTag);
            var schoolLevel = new SchoolLevelModel { 
                Url = "https://entraide-covid19.maxicours.com/LSI/prod/Accueil?_cla=4e&_eid=fcafrfk6crdp0qmdnk3jllja16"
            };

            var schoolLevelPage = new SchoolLevelPage(Driver, schoolLevel.Url);
            var result = _mapper.Map<List<SubjectSummaryModel>>(schoolLevelPage.GetAllSubjects());

            return result;
        }

        public HeaderModel GetHeader(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<HeaderModel>(subjectPage.GetHeader());

            return result;
        }

        public SubjectModel GetSubject(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var subject = _mapper.Map<SubjectModel>(subjectPage.GetSubject());

            return subject;
        }

        public List<ThemeModel> GetAllThemes(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ThemeModel>>(subjectPage.GetAllThemes());

            return result;
        }

        public List<CategoryModel> GetAllCategories(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<CategoryModel>>(subjectPage.GetAllCategories());

            return result;
        }

        public List<ItemModel> GetAllItems(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ItemModel>>(subjectPage.GetAllItems());

            return result;
        }

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ItemModel>>(subjectPage.GetItemsOfCategory(categoryId));

            return result;
        }

        public ItemModel GetItem(string levelTag, int subjectId, string categoryId, int lessonId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<ItemModel>(subjectPage.GetItem(categoryId, lessonId));

            return result;
        }

        #endregion

        #region Lesson

        public LessonModel GetLesson(string levelTag, int subjectId, string categoryId, int lessonId)
        {
            var item = GetItem(levelTag, subjectId, categoryId, lessonId);

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

        //public bool ExportSubjectLessons(string levelTag, int subjectId, string categoryId)
        //{
        //    try
        //    {
        //        var schoolLevel = GetSchoolLevel(levelTag);
        //        var themeList = GetAllThemes(levelTag, subjectId);
        //        var categoryList = GetAllCategories(levelTag, subjectId);

        //        var lessons = GetItemsOfCategory(levelTag, subjectId, categoryId);

        //        foreach (var lesson in lessons)
        //        {
        //            var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(lesson));
        //            var url = lessonPage.GetPrintUrl();
        //            _pdfConverterService.SaveUrlAsPdf(url, $"{levelTag}_{subjectId}_{categoryId}_{lesson.ThemeId}_{lesson.ItemId}.pdf");
        //        }

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public bool ExportThemeLessons(string levelTag, int subjectId, string categoryId, int themeId)
        //{
        //    try
        //    {
        //        var schoolLevel = GetSchoolLevel(levelTag);
        //        var themeList = GetAllThemes(levelTag, subjectId);
        //        var categoryList = GetAllCategories(levelTag, subjectId);

        //        var itemList = GetItemsOfCategory(levelTag, subjectId, categoryId);
        //        var lessons = itemList.Where(o => o.ThemeId == themeId);

        //        foreach (var lesson in lessons)
        //        {
        //            var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(lesson));
        //            var url = lessonPage.GetPrintUrl();
        //            _pdfConverterService.SaveUrlAsPdf(url, $"{levelTag}_{subjectId}_{categoryId}_{lesson.ThemeId}_{lesson.ItemId}.pdf");
        //        }

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public bool ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId)
        //{
        //    try
        //    {
        //        var schoolLevel = GetSchoolLevel(levelTag);
        //        var themeList = GetAllThemes(levelTag, subjectId);
        //        var categoryList = GetAllCategories(levelTag, subjectId);

        //        var itemList = GetItemsOfCategory(levelTag, subjectId, categoryId);
        //        var lesson = itemList.Where(o => o.ItemId == lessonId).FirstOrDefault();

        //        var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(lesson));
        //        var url = lessonPage.GetPrintUrl();
        //        _pdfConverterService.SaveUrlAsPdf(url, $"{lessonId}.pdf");

        //        //var html = lessonPage.GetPrintFormat();
        //        //SaveHtmlAsPdf(html, $"{lessonId}.pdf");

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        #endregion

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
            Driver = null;
        }
    }
}
