using System.Collections.Generic;
using System.Linq;
using StudiesManager.Common.Extensions;

namespace MaxicoursDownloader.Api.Models
{
    public class LessonModel
    {
        public ItemModel Item { get; set; }

        public string HtmlLesson { get; set; }

        public string PrintUrl { get; set; }

        public List<string> SwfUrls { get; set; }

        public string MindMapUrl { get; set; }

        public bool HasSwfs()
        {
            return SwfUrls.IsNotNull() && SwfUrls.Any();
        }

        public bool HasMindMap()
        {
            return !string.IsNullOrWhiteSpace(MindMapUrl);
        }
    }
}
