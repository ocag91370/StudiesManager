using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class SummarySubjectEntity
    {
        public SchoolLevelEntity SchoolLevel { get; set; }

        public int Id { get; set; }

        public string Tag { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
