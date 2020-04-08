using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class SubjectModel
    {
        public int SchoolLevelId { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public List<ThemeModel> Themes { get; set; }

        public List<CategoryModel> Categories { get; set; }
    }
}
