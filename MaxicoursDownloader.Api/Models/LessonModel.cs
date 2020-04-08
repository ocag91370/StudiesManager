using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models
{
    public class LessonModel
    {
        public int ThemeId { get; set; }

        public string ThemeName { get; set; }

        public int LessonId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
