using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class SubjectSummaryEntity
    {
        public int SchoolLevelId { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
