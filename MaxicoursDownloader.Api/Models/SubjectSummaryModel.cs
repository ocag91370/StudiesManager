using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class SubjectSummaryModel
    {
        public SchoolLevelModel SchoolLevel { get; set; }

        public string Tag { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
