using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class LessonModel
    {
        public ItemModel Item { get; set; }

        public string PrintUrl { get; set; }

        public string PageSource { get; set; }
    }
}
