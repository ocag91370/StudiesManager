using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
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
    public partial class ExportService : IExportService
    {
        private readonly string _lessonsCategoryKey = "lessons";

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

        private ExportResultModel ExportLesson(LessonModel lesson)
        {
            try
            {
                var nbFiles = SaveAsPdf(lesson);

                var item = lesson.Item;
                var index = item.Index.ToString().PadLeft(3, '0');
                var filename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}");

                if (lesson.HasSwfs())
                {
                    lesson.SwfUrls.ForEach(url =>
                    {
                        var name = url.Split("/").Last();
                        var uri = new Uri(url);
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(uri, $"{filename} - {name}");
                        }
                    });
                }

                if (lesson.HasMindMap())
                {
                    var name = lesson.MindMapUrl.Split("/").Last();
                    var uri = new Uri(lesson.MindMapUrl);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(uri, $"{filename} - {name}");
                    }
                }

                return new ExportResultModel(1, 0, nbFiles);
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
                string categoryId = _maxicoursSettings.Categories[_lessonsCategoryKey];

                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportLessons(itemList));
                });

                return new ExportResultModel(resultList);
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
                string categoryId = _maxicoursSettings.Categories[_lessonsCategoryKey];

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

                return new ExportResultModel(resultList);
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
                string categoryId = _maxicoursSettings.Categories[_lessonsCategoryKey];

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

                return new ExportResultModel(nbLessons, nbLessons - nbDistincts, nbFiles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportLessons(string levelTag, int subjectId, List<ItemKeyModel> itemKeyList)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_lessonsCategoryKey];
                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);

                var resultList = new List<ExportResultModel>();
                itemKeyList.ForEach((itemKey) =>
                {
                    var item = itemList.FirstOrDefault(o => o.Id == itemKey.Id && o.Index == itemKey.Index);
                    var lesson = _maxicoursService.GetLesson(item);
                    resultList.Add(ExportLesson(lesson));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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