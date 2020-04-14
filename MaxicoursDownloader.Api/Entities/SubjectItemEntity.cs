using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Entities
{
    public class SubjectItemEntity
    {
        public SubjectSummaryEntity SubjectSummary { get; set; }

        public ThemeEntity Theme { get; set; }

        public CategoryEntity Category { get; set; }

        public ItemEntity Item { get; set; }
    }
}
