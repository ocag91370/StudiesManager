using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Models
{
    public class ExportResultModel
    {
        public int NbItems { get; set; }

        public int NbDuplicates { get; set; }

        public int NbFiles { get; set; }

        public ExportResultModel(int nbItems, int nbDuplicates, int nbFiles)
        {
            NbItems = nbItems;
            NbDuplicates = nbDuplicates;
            NbFiles = nbFiles;
        }

        public ExportResultModel(List<ExportResultModel> exportResultList)
        {
            NbItems = exportResultList.Sum(o => o.NbItems);
            NbDuplicates = exportResultList.Sum(o => o.NbDuplicates);
            NbFiles = exportResultList.Sum(o => o.NbFiles);
        }
    }
}
