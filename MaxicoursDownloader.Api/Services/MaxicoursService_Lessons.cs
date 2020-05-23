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
        private readonly string _lessonsCategoryKey = "lessons";

        public LessonModel GetLesson(string levelTag, int subjectId, int lessonId)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_lessonsCategoryKey], lessonId);
            Debug.Assert(item.IsNotNull());

            var result = GetLesson(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public LessonModel GetLesson(ItemModel item)
        {
            var page = GetLessonPage(_mapper.Map<ItemEntity>(item));
            Debug.Assert(page.IsNotNull());

            var result = _mapper.Map<LessonModel>(page.GetLesson());
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetLessons(string levelTag, int subjectId)
        {
            var result = GetItemsOfCategory(levelTag, subjectId, _maxicoursSettings.Categories[_lessonsCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public LessonModel GetLesson(string levelTag, int subjectId, ItemKeyModel itemKey)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_lessonsCategoryKey], itemKey);
            Debug.Assert(item.IsNotNull());

            var result = GetLesson(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetLessons(SummarySubjectModel summarySubject)
        {
            var result = GetItemsOfCategory(summarySubject, _maxicoursSettings.Categories[_lessonsCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
