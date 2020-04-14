namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IPdfConverterService
    {
        void SaveUrlAsPdf(string url, string filename);

        void SaveHtmlAsPdf(string html, string filename);
    }
}