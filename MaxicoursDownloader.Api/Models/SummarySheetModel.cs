using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class SummarySheetModel
    {
        public ItemModel Item { get; set; }

        public string PrintUrl { get; set; }

        public string AudioUrl { get; set; }
    }
}
