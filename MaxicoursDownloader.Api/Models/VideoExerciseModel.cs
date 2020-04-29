using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class VideoExerciseModel
    {
        public ItemModel Item { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Solution { get; set; }

        public string VideoUrl { get; set; }

        public bool IsTextOk()
        {
            return !string.IsNullOrWhiteSpace(Subject) && !string.IsNullOrWhiteSpace(Solution);
        }

        public bool IsVideoOk()
        {
            return !string.IsNullOrWhiteSpace(VideoUrl);
        }

        public bool IsOk()
        {
            return IsTextOk() || IsVideoOk();
        }
    }
}
