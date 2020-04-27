using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class VideoExerciseModel
    {
        public ItemModel Item { get; set; }

        public string Subject { get; set; }

        public string Solution { get; set; }

        public string VideoUrl { get; set; }
    }
}
