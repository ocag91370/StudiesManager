namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IPdfConverterService
    {
        void SaveUrlAsPdf(string url, string filename);
    }
}