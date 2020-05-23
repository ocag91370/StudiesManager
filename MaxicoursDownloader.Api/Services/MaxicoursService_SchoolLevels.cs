﻿using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Models;
using StudiesManager.Common.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public partial class MaxicoursService : IMaxicoursService
    {
        public List<SchoolLevelModel> GetSchoolLevels()
        {
            var page = GetHomePage();
            Debug.Assert(page.IsNotNull());

            var result = page.GetAllSchoolLevels();
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public SchoolLevelModel GetSchoolLevel(string levelTag)
        {
            var schoolLevelList = GetSchoolLevels();
            Debug.Assert(schoolLevelList.IsNotNull());

            var result = schoolLevelList.FirstOrDefault(o => o.Tag.IsSameAs(levelTag));
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
