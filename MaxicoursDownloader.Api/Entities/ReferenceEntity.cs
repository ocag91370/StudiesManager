using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MaxicoursDownloader.Api.Entities
{
    public class ReferenceEntity
    {
        public ReferenceEntity(List<int> arbo, int schoolLevelId, int subjectId, int themeId = 0, string categoryId = null, int itemId = 0)
        {
            Arbo = arbo;
            SchoolLevelId = schoolLevelId;
            SubjectId = subjectId;
            ThemeId = themeId;
            CategoryId = categoryId;
            ItemId = itemId;
        }

        public List<int> Arbo { get; set; }

        public int SchoolLevelId { get; set; }

        public int SubjectId { get; set; }

        public int ThemeId { get; set; }

        public string CategoryId { get; set; }

        public int ItemId { get; set; }
    }
}
