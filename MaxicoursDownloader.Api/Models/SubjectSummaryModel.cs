using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class SubjectSummaryModel
    {
        public int SchoolLevelId { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
