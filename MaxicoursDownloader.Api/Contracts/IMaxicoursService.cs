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

        HeaderModel GetHeader(string levelTag, int subjectId);

        List<ThemeModel> GetAllThemes(string levelTag, int subjectId);

        List<CategoryModel> GetAllCategories(string levelTag, int subjectId);

        List<ItemModel> GetAllItems(string levelTag, int subjectId);

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId);

        ItemModel GetItem(string levelTag, int subjectId, string categoryId, int itemId);

        LessonModel GetLesson(string levelTag, int subjectId, string categoryId, int lessonId);

        LessonModel GetLesson(ItemModel item);
    }
}
