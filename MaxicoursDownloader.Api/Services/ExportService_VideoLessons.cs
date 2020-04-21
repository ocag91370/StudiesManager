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
        private readonly string _videoLessonsCategoryKey = "video_lessons";

        public ExportResultModel ExportVideoLesson(string levelTag, int subjectId, int lessonId)
        {
            try
            {
                var videoLesson = _maxicoursService.GetVideoLesson(levelTag, subjectId, lessonId);

                return ExportVideoLesson(videoLesson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportVideoLesson(VideoLessonModel videoLesson)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(videoLesson.VideoUrl))
                    return new ExportResultModel(1, 0, 0);

                var item = videoLesson.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var videoFilename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}.mp4");
                var uri = new Uri(videoLesson.VideoUrl);
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(uri, videoFilename);
                }

                return new ExportResultModel(1, 0, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportVideoLessons(string levelTag)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_videoLessonsCategoryKey];

                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportVideoLessons(itemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportVideoLessons(string levelTag, int subjectId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_videoLessonsCategoryKey];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);
                var groupByThemeIdItemList = itemList.GroupBy(
                    o => o.Theme?.Id,
                    o => o,
                    (themeId, itemList) => new { ThemeId = themeId, ItemList = itemList.ToList() }
                    ).ToList();

                var resultList = new List<ExportResultModel>();
                groupByThemeIdItemList.ForEach((group) =>
                {
                    resultList.Add(ExportVideoLessons(group.ItemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportVideoLessons(string levelTag, int subjectId, int themeId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_videoLessonsCategoryKey];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId)
                    .Where(o => o.Theme.Id == themeId)
                    .ToList();

                return ExportVideoLessons(itemList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportVideoLessons(List<ItemModel> itemList)
        {
            try
            {
                var videoLessonList = itemList.Select(item => _maxicoursService.GetVideoLesson(item)).ToList();
                var fileList = videoLessonList.Select(videoLesson => ExportVideoLesson(videoLesson)).ToList();

                return new ExportResultModel(fileList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}