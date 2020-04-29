using MaxicoursDownloader.Api.Models;

namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IPdfConverterService
    {
        void SaveAsPdf(LessonModel lesson);

        void SaveUrlAsPdf(string url, string filename);

        void SaveHtmlAsPdf(string html, string filename);

        void SaveAsPdf(VideoExerciseModel videoExercise);
    }
}