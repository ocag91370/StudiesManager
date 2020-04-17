using MaxicoursDownloader.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Repositories
{
    public static class ExportResultFactory
    {
        public static ExportResultModel Create (int nbItems, int nbDuplicates, int nbFiles)
        {
            return new ExportResultModel { NbItems = nbItems, NbDuplicates = nbDuplicates, NbFiles = nbFiles };
        }

        public static ExportResultModel Create(List<ExportResultModel> exportResultList)
        {
            return ExportResultFactory.Create(
                exportResultList.Sum(o => o.NbItems),
                exportResultList.Sum(o => o.NbDuplicates),
                exportResultList.Sum(o => o.NbFiles)
                );
        }
    }
}
