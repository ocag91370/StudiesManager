using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class SubjectModel
    {
        public SummarySubjectModel SubjectSummary { get; set; }

        public List<ThemeModel> Themes { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<ItemModel> Items { get; set; }
    }
}
