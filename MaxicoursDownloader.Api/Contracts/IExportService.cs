using System;

namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IExportService : IDisposable
    {
        int ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId);

        int ExportLessons(string levelTag, string categoryId);

        int ExportLessons(string levelTag, int subjectId, string categoryId);

        int ExportLessons(string levelTag, int subjectId, string categoryId, int themeId);
    }
}