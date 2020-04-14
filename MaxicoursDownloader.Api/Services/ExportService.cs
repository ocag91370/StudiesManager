using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        public int ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId)
        {
            try
            {
                var lesson = _maxicoursService.GetLesson(levelTag, subjectId, categoryId, lessonId);

                return ExportLesson(lesson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExportLessons(string levelTag, string categoryId)
        {
            try
            {
                var subjectList = _maxicoursService.GetAllSubjects(levelTag);

                int count = 0;
                Parallel.ForEach(subjectList, (subject) => {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    count += ExportLessons(itemList);
                });

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExportLessons(string levelTag, int subjectId, string categoryId)
        {
            try
            {
                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);

                return ExportLessons(itemList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExportLessons(string levelTag, int subjectId, string categoryId, int themeId)
        {
            try
            {
                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId)
                    .Where(o => o.Id == themeId)
                    .ToList();

                return ExportLessons(itemList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int ExportLesson(LessonModel lesson)
        {
            try
            {
                SaveAsPdf(lesson);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int ExportLessons(List<ItemModel> itemList)
        {
            try
            {
                var lessonList = new ConcurrentBag<LessonModel>();
                Parallel.ForEach(itemList, (item) => {
                    var lesson = _maxicoursService.GetLesson(item);
                    lessonList.Add(lesson);
                });

                Parallel.ForEach(lessonList, (lesson) => {
                    SaveAsPdf(lesson);
                });

                return itemList.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _maxicoursService.Dispose();
        }

        private void SaveAsPdf(LessonModel lesson)
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

        private string GetFilename(ItemModel item)
        {
            return $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {item.Theme.Tag} - {item.Id} - {item.Tag}.pdf";
        }
    }
}