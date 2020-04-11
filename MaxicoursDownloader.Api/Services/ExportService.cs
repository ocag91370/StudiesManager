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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Services
{
    public class ExportService : IExportService
    {
        private readonly IMapper _mapper;
        private readonly IMaxicoursService _maxicoursService;
        private readonly IPdfConverterService _pdfConverterService;

        public ExportService(IMapper mapper, IMaxicoursService maxicoursService, IPdfConverterService pdfConverterService)
        {
            _mapper = mapper;
            _maxicoursService = maxicoursService;
            _pdfConverterService = pdfConverterService;
        }

        //public bool ExportSubjectLessons(string levelTag, int subjectId, string categoryId)
        //{
        //    try
        //    {
        //        var schoolLevel = _maxicoursService.GetSchoolLevel(levelTag);
        //        var themeList = _maxicoursService.GetAllThemes(levelTag, subjectId);
        //        var categoryList = _maxicoursService.GetAllCategories(levelTag, subjectId);

        //        var lessons = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);

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

        public bool ExportThemeLessons(string levelTag, int subjectId, string categoryId, int themeId)
        {
            try
            {
                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId)
                                    .Where(o => o.ThemeId == themeId);

                foreach (var item in itemList)
                {
                    var lesson = _maxicoursService.GetLesson(item);
                    SaveAsPdf(levelTag, subjectId, lesson);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExportLessons(string levelTag, int subjectId, string categoryId)
        {
            try
            {
                var subject = _maxicoursService.GetSubject(levelTag, subjectId);
                var itemList = subject.Items.Where(o => o.CategoryId == categoryId);

                foreach(var item in itemList)
                {
                    var lesson = _maxicoursService.GetLesson(item);

                    SaveAsPdf(levelTag, subjectId, lesson);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId)
        {
            try
            {
                var lesson = _maxicoursService.GetLesson(levelTag, subjectId, categoryId, lessonId);

                SaveAsPdf(levelTag, subjectId, lesson);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SaveAsPdf(string levelTag, int subjectId, LessonModel lesson)
        {
            try
            {
                var filename = GetFilename(lesson.Item);

                _pdfConverterService.SaveUrlAsPdf(lesson.PrintUrl, filename);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw ex;
            }
        }

        private string GetFilename(string levelTag, int subjectId, ItemModel item)
        {
            return $"{levelTag}_{subjectId}_{item.CategoryId}_{item.ThemeId}_{item.ItemId}.pdf";
        }

        private string GetFilename(ItemModel item)
        {
            return $"{item.Name.CleanName()}.pdf";
        }

    }
}