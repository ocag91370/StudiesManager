using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class LessonEntity
    {
        public ItemEntity Item { get; set; }

        public string HtmlLesson { get; set; }

        public string PrintUrl { get; set; }

        public List<string> SwfUrls { get; set; }

        public string MindMapUrl { get; set; }
    }
}
