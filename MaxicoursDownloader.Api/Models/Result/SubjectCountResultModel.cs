using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models.Result
{
    public class SubjectCountResultModel
    {
        public SubjectResultModel Subject { get; set; }

        public int NbItems { get; set; }
    }
}
