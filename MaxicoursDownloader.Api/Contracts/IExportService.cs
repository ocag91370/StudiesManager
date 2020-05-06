using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IExportService : IDisposable
    {
        #region Lessons

        ExportResultModel ExportLesson(string levelTag, int subjectId, int lessonId);

        ExportResultModel ExportLessons(string levelTag);

        ExportResultModel ExportLessons(string levelTag, int subjectId);

        ExportResultModel ExportLessons(string levelTag, int subjectId, int themeId);

        ExportResultModel ExportLessons(string levelTag, int subjectId, List<ItemKeyModel> itemKeyList);

        #endregion

        #region Summary sheets

        ExportResultModel ExportSummarySheet(string levelTag, int subjectId, int summarySheetId);

        ExportResultModel ExportSummarySheets(string levelTag);

        ExportResultModel ExportSummarySheets(string levelTag, int subjectId);

        #endregion

        #region Tests

        ExportResultModel ExportTest(string levelTag, int subjectId, int testId);

        ExportResultModel ExportTests(string levelTag);

        #endregion

        #region Video lessons

        ExportResultModel ExportVideoLesson(string levelTag, int subjectId, int videoLessonId);

        ExportResultModel ExportVideoLessons(string levelTag);

        ExportResultModel ExportVideoLessons(string levelTag, int subjectId);

        ExportResultModel ExportVideoLessons(string levelTag, int subjectId, int themeId);

        ExportResultModel ExportVideoLessons(string levelTag, int subjectId, List<ItemKeyModel> itemKeyList);

        #endregion

        #region Video exercises

        ExportResultModel ExportVideoExercise(string levelTag, int subjectId, int videoExerciseId);

        ExportResultModel ExportVideoExercises(string levelTag);

        ExportResultModel ExportVideoExercises(string levelTag, int subjectId);

        ExportResultModel ExportVideoExercises(string levelTag, int subjectId, int themeId);

        ExportResultModel ExportVideoExercises(string levelTag, int subjectId, List<ItemKeyModel> itemKeyList);

        #endregion
    }
}