using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using StudiesManager.Common;
using MaxicoursDownloader.Api.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace MaxicoursDownloader.Api.Services
{
    public partial class MaxicoursService : IMaxicoursService
    {
        private readonly string _videoLessonsCategoryKey = "video_lessons";

        public VideoLessonModel GetVideoLesson(string levelTag, int subjectId, int lessonId)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_videoLessonsCategoryKey], lessonId);
            Debug.Assert(item.IsNotNull());

            var result = GetVideoLesson(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public VideoLessonModel GetVideoLesson(ItemModel item)
        {
            var page = GetVideoLessonPage(_mapper.Map<ItemEntity>(item));
            Debug.Assert(page.IsNotNull());

            var videoLesson = page.GetVideoLesson();
            Debug.Assert(videoLesson.IsNotNull());

            var result = _mapper.Map<VideoLessonModel>(videoLesson);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public VideoLessonModel GetVideoLesson(string levelTag, int subjectId, ItemKeyModel itemKey)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_videoLessonsCategoryKey], itemKey);
            Debug.Assert(item.IsNotNull());

            var result = GetVideoLesson(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetVideoLessons(string levelTag, int subjectId)
        {
            var result = GetItemsOfCategory(levelTag, subjectId, _maxicoursSettings.Categories[_videoLessonsCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetVideoLessons(SummarySubjectModel summarySubject)
        {
            var result = GetItemsOfCategory(summarySubject, _maxicoursSettings.Categories[_videoLessonsCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
