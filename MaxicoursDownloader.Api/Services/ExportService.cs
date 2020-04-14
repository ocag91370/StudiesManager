using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public class ExportService : IExportService
    {
        private readonly IMapper _mapper;
        private readonly IMaxicoursService _maxicoursService;
        private readonly IPdfConverterService _pdfConverterService;
        private readonly IDirectoryService _directoryService;

        public ExportService(IMapper mapper, IMaxicoursService maxicoursService, IPdfConverterService pdfConverterService, IDirectoryService directoryService)
        {
            _mapper = mapper;
            _maxicoursService = maxicoursService;
            _pdfConverterService = pdfConverterService;
            _directoryService = directoryService;
        }

        public bool ExportThemeLessons(string levelTag, int subjectId, string categoryId, int themeId)
        {
            try
            {
                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId).Where(o => o.Id == themeId);

                foreach (var item in itemList)
                {
                    var lesson = _maxicoursService.GetLesson(item);
                    SaveUrlAsPdf(lesson);
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
                var itemList = subject.Items.Where(o => o.Category.Id == categoryId);

                foreach(var item in itemList)
                {
                    var lesson = _maxicoursService.GetLesson(item);

                    SaveUrlAsPdf(lesson);
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

                SaveUrlAsPdf(lesson);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SaveUrlAsPdf(LessonModel lesson)
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

        private void SaveHtmlAsPdf(string levelTag, int subjectId, LessonModel lesson)
        {
            try
            {
                var filename = GetFilename(lesson.Item);

                _pdfConverterService.SaveHtmlAsPdf(lesson.PageSource, filename);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw ex;
            }
        }

        private string GetFilename(string levelTag, int subjectId, ItemModel item)
        {
            return $"{levelTag}_{subjectId}_{item.Category.Id}_{item.Theme.Id}_{item.Id}.pdf";
        }

        private string GetFilename(ItemModel item)
        {
            return $"{item.Id} - {item.Name.CleanName()}.pdf";
        }
    }
}