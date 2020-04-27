using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using System.Collections.Generic;
using System.Diagnostics;

namespace MaxicoursDownloader.Api.Services
{
    public partial class MaxicoursService : IMaxicoursService
    {
        private readonly string _videoExercisesCategoryKey = "video_exercices";

        public VideoExerciseModel GetVideoExercise(string levelTag, int subjectId, int exerciseId)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_videoExercisesCategoryKey], exerciseId);
            Debug.Assert(item.IsNotNull());

            var result = GetVideoExercise(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public VideoExerciseModel GetVideoExercise(ItemModel item)
        {
            var page = GetVideoExercisePage(_mapper.Map<ItemEntity>(item));
            Debug.Assert(page.IsNotNull());

            var videoExercise = page.GetVideoExercise();
            Debug.Assert(videoExercise.IsNotNull());

            var result = _mapper.Map<VideoExerciseModel>(videoExercise);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> VideoExerciseModels(string levelTag, int subjectId)
        {
            var result = GetItemsOfCategory(levelTag, subjectId, _maxicoursSettings.Categories[_videoExercisesCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
