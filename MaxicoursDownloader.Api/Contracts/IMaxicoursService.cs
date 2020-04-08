using MaxicoursDownloader.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Contracts
{
    public interface IMaxicoursService
    {
        List<SchoolLevelModel> GetAllSchoolLevels();

        SchoolLevelModel GetSchoolLevel(string levelTag);

        List<SubjectSummaryModel> GetAllSubjects(string levelTag);

        SubjectModel GetSubject(string levelTag, int subjectId);

        void SaveClosedPageAsPdf();
    }
}
