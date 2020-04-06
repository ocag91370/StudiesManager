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

        SchoolLevelModel GetSchoolLevel(string levelName);

        List<SubjectModel> GetAllSubjects(string levelName);

        SubjectModel GetSubject(string levelName, string subjectName);
    }
}
