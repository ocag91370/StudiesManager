using MaxicoursDownloader.Api.Models;
using System;

namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IExportService : IDisposable
    {
        ExportResultModel ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId);

        ExportResultModel ExportLessons(string levelTag, string categoryId);

        ExportResultModel ExportLessons(string levelTag, int subjectId, string categoryId);

        ExportResultModel ExportLessons(string levelTag, int subjectId, string categoryId, int themeId);
    }
}