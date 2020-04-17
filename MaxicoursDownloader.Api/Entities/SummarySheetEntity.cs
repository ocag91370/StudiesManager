using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class SummarySheetEntity
    {
        public ItemEntity Item { get; set; }

        public string PrintUrl { get; set; }

        public string AudioUrl { get; set; }
    }
}
