using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Entities
{
    public class WorkEntity
    {
        public string Title { get; set; }

        public List<SubjectEntity> Subjects { get; set; }

        public string Html { get; set; }
    }
}
