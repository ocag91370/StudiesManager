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
using System.Net;
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

        public ExportResultModel ExportLesson(string levelTag, int subjectId, int lessonId)
        {
            try
            {
                var lesson = _maxicoursService.GetLesson(levelTag, subjectId, lessonId);

                return ExportLesson(lesson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportLessons(string levelTag)
        {
            try
            {
                string categoryId = CategoryRepository.Types["lesson"];

                var subjectList = _maxicoursService.GetAllSubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportLessons(itemList));
                });

                return ExportResultFactory.Create(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportLessons(string levelTag, int subjectId)
        {
            try
            {
                string categoryId = CategoryRepository.Types["lesson"];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);
                var groupByThemeIdItemList = itemList.GroupBy(
                    o => o.Theme?.Id,
                    o => o,
                    (themeId, itemList) => new { ThemeId = themeId, ItemList = itemList.ToList() }
                    ).ToList();

                var resultList = new List<ExportResultModel>();
                groupByThemeIdItemList.ForEach((group) =>
                {
                    resultList.Add(ExportLessons(group.ItemList));
                });

                return ExportResultFactory.Create(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportLessons(string levelTag, int subjectId, int themeId)
        {
            try
            {
                string categoryId = CategoryRepository.Types["lesson"];

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

                return ExportResultFactory.Create(1, 0, nbFiles);
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
                Parallel.ForEach(itemList, (item) =>
                {
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
                var nbFiles = fileList.ToList().Sum(o => o);

                return ExportResultFactory.Create(nbLessons, nbLessons - nbDistincts, nbFiles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportSummarySheet(SummarySheetModel summarySheet)
        {
            try
            {
                var item = summarySheet.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var filename = $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag}.pdf";
                var result = Path.Combine(_basePath, filename);

                using (WebClient client = new WebClient())
                {
                    byte[] arr = client.DownloadData(summarySheet.PrintUrl);

                    File.WriteAllBytes(result, arr);

                }

                return ExportResultFactory.Create(1, 0, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportSummarySheet(string levelTag, int subjectId, int summarySheetId)
        {
            try
            {
                string categoryId = CategoryRepository.Types["summary_sheet"];

                var summarySheet = _maxicoursService.GetSummarySheet(levelTag, subjectId, summarySheetId);

                return ExportSummarySheet(summarySheet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportSummarySheets(List<ItemModel> itemList)
        {
            try
            {
                var summarySheetList = itemList.Select(item => _maxicoursService.GetSummarySheet(item)).ToList();
                var fileList = summarySheetList.Select(summarySheet => ExportSummarySheet(summarySheet)).ToList();

                return ExportResultFactory.Create(fileList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportSummarySheets(string levelTag)
        {
            try
            {
                string categoryId = CategoryRepository.Types["summary_sheet"];

                var subjectList = _maxicoursService.GetAllSubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportSummarySheets(itemList));
                });

                return ExportResultFactory.Create(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportSummarySheets(string levelTag, int subjectId)
        {
            try
            {
                string categoryId = CategoryRepository.Types["summary_sheet"];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);
                var groupByThemeIdItemList = itemList.GroupBy(
                    o => o.Theme?.Id,
                    o => o,
                    (themeId, itemList) => new { ThemeId = themeId, ItemList = itemList.ToList() }
                    ).ToList();

                var resultList = new List<ExportResultModel>();
                groupByThemeIdItemList.ForEach((group) =>
                {
                    resultList.Add(ExportSummarySheets(group.ItemList));
                });

                return ExportResultFactory.Create(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportTest(TestModel test)
        {
            try
            {
                var item = test.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var workFilename = Path.Combine(_basePath, $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag} - sujet.pdf");
                using (WebClient client = new WebClient())
                {
                    byte[] arr = client.DownloadData(test.WorkUrl);

                    File.WriteAllBytes(workFilename, arr);

                }

                var correctionFilename = Path.Combine(_basePath, $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag} - correction.pdf");
                using (WebClient client = new WebClient())
                {
                    byte[] arr = client.DownloadData(test.CorrectionUrl);

                    File.WriteAllBytes(correctionFilename, arr);

                }

                return ExportResultFactory.Create(1, 0, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportTest(string levelTag, int subjectId, int testId)
        {
            try
            {
                string categoryId = CategoryRepository.Types["test"];

                var test = _maxicoursService.GetTest(levelTag, subjectId, testId);

                return ExportTest(test);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportTests(string levelTag)
        {
            try
            {
                string categoryId = CategoryRepository.Types["test"];

                var subjectList = _maxicoursService.GetAllSubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportTests(itemList));
                });

                return ExportResultFactory.Create(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ExportResultModel ExportTests(List<ItemModel> itemList)
        {
            try
            {
                var testList = itemList.Select(item => _maxicoursService.GetTest(item)).ToList();
                var fileList = testList.Select(test => ExportTest(test)).ToList();

                return ExportResultFactory.Create(fileList);
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
                _pdfConverterService.SaveAsPdf(lesson);

                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return 0;
            }
        }
    }
}