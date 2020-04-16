using MaxicoursDownloader.Api.Models;

namespace MaxicoursDownloader.Api.Repositories
{
    public static class ExportResultFactory
    {
        public static ExportResultModel Create (int nbItems, int nbFiles, int nbDuplicates)
        {
            return new ExportResultModel { NbItems = nbItems, NbFiles = nbFiles, NbDuplicates = nbDuplicates };
        }
    }
}
