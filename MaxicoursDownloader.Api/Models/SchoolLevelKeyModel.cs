using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models
{
    public class SchoolLevelKeyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NbSubjects { get; set; }

        public int NbItems { get; set; }

        public List<SubjectKeyModel> Subjects { get; set; }
    }
}
