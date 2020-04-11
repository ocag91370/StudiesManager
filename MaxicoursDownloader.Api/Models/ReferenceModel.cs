using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models
{
    public class ReferenceModel
    {
        public int SchoolLevelId { get; set; }

        public int SubjectId { get; set; }

        public List<int> ThemeIdList { get; set; }

        public string CategoryId { get; set; }

        public int? ItemId { get; set; }
    }
}
