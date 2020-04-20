using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models
{
    public class ItemModel
    {
        public SummarySubjectModel SummarySubject { get; set; }

        public ThemeModel Theme { get; set; }

        public CategoryModel Category { get; set; }

        public int Id { get; set; }

        public string Tag { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Index { get; set; }
    }
}
