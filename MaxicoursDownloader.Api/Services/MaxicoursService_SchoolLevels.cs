using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public partial class MaxicoursService : IMaxicoursService
    {
        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            var page = GetHomePage();
            Debug.Assert(page.IsNotNull());

            var result = page.GetAllSchoolLevels();
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public SchoolLevelModel GetSchoolLevel(string levelTag)
        {
            var schoolLevelList = GetAllSchoolLevels();
            Debug.Assert(schoolLevelList.IsNotNull());

            var result = schoolLevelList.FirstOrDefault(o => o.Tag.IsSameAs(levelTag));
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
