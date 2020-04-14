﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Models
{
    public class ReferenceModel
    {
        public ReferenceModel(List<int> arbo, int schoolLevelId, int subjectId, int themeId = int.MinValue, string categoryId = null, int itemId = int.MinValue)
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