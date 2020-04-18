using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Contracts
{
    public interface IMaxicoursService : IDisposable
    {
        List<SchoolLevelModel> GetAllSchoolLevels();

        SchoolLevelModel GetSchoolLevel(string levelTag);

        List<SubjectSummaryModel> GetAllSubjects(string levelTag);

        SubjectModel GetSubject(string levelTag, int subjectId);

        List<ThemeModel> GetAllThemes(string levelTag, int subjectId);

        List<CategoryModel> GetAllCategories(string levelTag, int subjectId);

        List<ItemModel> GetAllItems(string levelTag, int subjectId);

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId);

        public List<ItemModel> GetLessons(string levelTag, int subjectId);

        LessonModel GetLesson(string levelTag, int subjectId, int lessonId);

        LessonModel GetLesson(ItemModel item);

        List<ItemModel> GetSummarySheets(string levelTag, int subjectId);

        SummarySheetModel GetSummarySheet(string levelTag, int subjectId, int summarySheetId);

        SummarySheetModel GetSummarySheet(ItemModel item);

        TestModel GetTest(ItemModel item);

        TestModel GetTest(string levelTag, int subjectId, int testId);

        List<ItemModel> GetTests(string levelTag, int subjectId);
    }
}
