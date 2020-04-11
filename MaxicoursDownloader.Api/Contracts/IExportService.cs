namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IExportService
    {
        bool ExportLesson(string levelTag, int subjectId, string categoryId, int lessonId);

        bool ExportThemeLessons(string levelTag, int subjectId, string categoryId, int themeId);

        bool ExportLessons(string levelTag, int subjectId, string categoryId);
    }
}