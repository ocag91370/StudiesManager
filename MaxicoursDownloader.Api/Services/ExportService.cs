using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private readonly string _basePath = @"C:\Travail\Maxicours\Export\Temp";

        public ExportService(IMapper mapper, IMaxicoursService maxicoursService, IPdfConverterService pdfConverterService, IDirectoryService directoryService)
        {
            _mapper = mapper;
            _maxicoursService = maxicoursService;
            _pdfConverterService = pdfConverterService;
            _directoryService = directoryService;
        }

        public ExportResultModel ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId)
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

        public ExportResultModel ExportLessons(string levelTag, string categoryId)
        {
            try
            {
                var subjectList = _maxicoursService.GetAllSubjects(levelTag);

                var resultList = new ConcurrentBag<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportLessons(itemList));
                });

                return ExportResultFactory.Create(resultList.Sum(o => o.NbItems), resultList.Sum(o => o.NbFiles), resultList.Sum(o => o.NbDuplicates));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportLessons(string levelTag, int subjectId, string categoryId)
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

        public ExportResultModel ExportLessons(string levelTag, int subjectId, string categoryId, int themeId)
        {
            try
            {
                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId)
                    .Where(o => o.Theme.Id == themeId)
                    .ToList();

                return ExportLessons(itemList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportLesson(LessonModel lesson)
        {
            try
            {
                var nbFiles = SaveAsPdf(lesson);

                return ExportResultFactory.Create(1, nbFiles, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportLessons(List<ItemModel> itemList)
        {
            try
            {
                var lessonList = new ConcurrentBag<LessonModel>();
                Parallel.ForEach(itemList, (item) => {
                    var lesson = _maxicoursService.GetLesson(item);
                    lessonList.Add(lesson);
                });
                var nbLessons = lessonList.Count();
                var nbDistincts = lessonList.Select(o => o.Item.Id).Distinct().Count();

                var fileList = new ConcurrentBag<int>();
                Parallel.ForEach(lessonList, (lesson) =>
                {
                    fileList.Add(SaveAsPdf(lesson));
                });
                var nbFiles = fileList.Sum(o => o);

                return ExportResultFactory.Create(nbLessons, nbLessons - nbDistincts, nbFiles);
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

        private int SaveAsPdf(LessonModel lesson)
        {
            try
            {
                var filename = GetFilename(lesson.Item);

                _pdfConverterService.SaveUrlAsPdf(lesson.PrintUrl, filename);

                //return File.Exists(filename) ? 1 : 0;
                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return 0;
            }
        }

        private string GetFilename(ItemModel item)
        {
            var index = item.Index.ToString().PadLeft(3, '0');

            var filename = $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag}.pdf";
            var result = Path.Combine(_basePath, filename);

            return result;
        }
    }
}