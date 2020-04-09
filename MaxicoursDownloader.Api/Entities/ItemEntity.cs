using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Entities
{
    public class ItemEntity
    {
        public int ThemeId { get; set; }

        public string CategoryId { get; set; }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
