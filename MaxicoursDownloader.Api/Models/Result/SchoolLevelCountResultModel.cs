using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models.Result
{
    public class SchoolLevelCountResultModel
    {
        public SchoolLevelResultModel SchoolLevel { get; set; }

        public int NbSubjects { get; set; }

        public int NbItems { get; set; }

        public List<SubjectCountResultModel> Subjects { get; set; }
    }
}
