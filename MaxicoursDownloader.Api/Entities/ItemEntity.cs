using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Entities
{
    public class ItemEntity
    {
        public SubjectSummaryEntity SubjectSummary { get; set; }

        public ThemeEntity Theme { get; set; }

        public CategoryEntity Category { get; set; }

        public int Id { get; set; }

        public string Tag { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Index { get; set; }
    }
}
