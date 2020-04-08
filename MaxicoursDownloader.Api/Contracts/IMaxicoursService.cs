using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Contracts
{
    public interface IMaxicoursService
    {
        List<SchoolLevelModel> GetAllSchoolLevels();

        SchoolLevelModel GetSchoolLevel(string levelTag);

        List<SubjectModel> GetAllSubjects(string levelTag);

        SubjectModel GetSubject(string levelTag, int subjectId);

        List<ThemeModel> GetAllThemes(string levelTag, int subjectId);
    }
}
