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

        List<SummarySubjectModel> GetSummarySubjects(string levelTag);

        SubjectModel GetSubject(string levelTag, int subjectId);

        List<ThemeModel> GetThemes(string levelTag, int subjectId);

        List<CategoryModel> GetCategories(string levelTag, int subjectId);

        List<ItemModel> GetItems(string levelTag, int subjectId);

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

        VideoLessonModel GetVideoLesson(ItemModel item);

        VideoLessonModel GetVideoLesson(string levelTag, int subjectId, int testId);

        List<ItemModel> GetVideoLessons(string levelTag, int subjectId);

        VideoExerciseModel GetVideoExercise(ItemModel item);

        VideoExerciseModel GetVideoExercise(string levelTag, int subjectId, int testId);

        List<ItemModel> GetVideoExercises(string levelTag, int subjectId);
    }
}
