using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class VideoExerciseEntity
    {
        public ItemEntity Item { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Solution { get; set; }

        public string VideoUrl { get; set; }
    }
}
