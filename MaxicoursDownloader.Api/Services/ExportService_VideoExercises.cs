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
        private readonly string _videoExercisesCategoryKey = "video_lessons";

        public ExportResultModel ExportVideoExercise(string levelTag, int subjectId, int lessonId)
        {
            try
            {
                var videoExercise = _maxicoursService.GetVideoExercise(levelTag, subjectId, lessonId);

                return ExportVideoExercise(videoExercise);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportVideoExercise(VideoExerciseModel videoExercise)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(videoExercise.VideoUrl))
                    return new ExportResultModel(1, 0, 0);

                var item = videoExercise.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var videoFilename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}.mp4");
                var uri = new Uri(videoExercise.VideoUrl);
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

        public ExportResultModel ExportVideoExercises(string levelTag)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_videoExercisesCategoryKey];

                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportVideoExercises(itemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportVideoExercises(string levelTag, int subjectId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_videoExercisesCategoryKey];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);
                var groupByThemeIdItemList = itemList.GroupBy(
                    o => o.Theme?.Id,
                    o => o,
                    (themeId, itemList) => new { ThemeId = themeId, ItemList = itemList.ToList() }
                    ).ToList();

                var resultList = new List<ExportResultModel>();
                groupByThemeIdItemList.ForEach((group) =>
                {
                    resultList.Add(ExportVideoExercises(group.ItemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportVideoExercises(string levelTag, int subjectId, int themeId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_videoExercisesCategoryKey];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId)
                    .Where(o => o.Theme.Id == themeId)
                    .ToList();

                return ExportVideoExercises(itemList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportVideoExercises(List<ItemModel> itemList)
        {
            try
            {
                var videoExerciseList = itemList.Select(item => _maxicoursService.GetVideoExercise(item)).ToList();
                var fileList = videoExerciseList.Select(videoExercise => ExportVideoExercise(videoExercise)).ToList();

                return new ExportResultModel(fileList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}