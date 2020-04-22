using MaxicoursDownloader.Api.Models;
using System;

namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IExportService : IDisposable
    {
        ExportResultModel ExportLesson(string levelTag, int subjectId, int lessonId);

        ExportResultModel ExportLessons(string levelTag);

        ExportResultModel ExportLessons(string levelTag, int subjectId);

        ExportResultModel ExportLessons(string levelTag, int subjectId, int themeId);

        ExportResultModel ExportSummarySheet(string levelTag, int subjectId, int summarySheetId);

        ExportResultModel ExportSummarySheets(string levelTag);

        ExportResultModel ExportSummarySheets(string levelTag, int subjectId);

        ExportResultModel ExportTest(string levelTag, int subjectId, int testId);

        ExportResultModel ExportTests(string levelTag);

        ExportResultModel ExportVideoLesson(string levelTag, int subjectId, int videoLessonId);

        ExportResultModel ExportVideoLessons(string levelTag);

        ExportResultModel ExportVideoLessons(string levelTag, int subjectId);

        ExportResultModel ExportVideoLessons(string levelTag, int subjectId, int themeId);
    }
}