using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models.Result
{
    public class SchoolLevelKeyResultModel
    {
        public int Id { get; set; }

        public List<SubjectKeyResultModel> Subjects { get; set; }
    }
}
