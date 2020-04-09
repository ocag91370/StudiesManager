using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class SubjectEntity
    {
        public HeaderEntity Header { get; set; }

        public List<ThemeEntity> Themes { get; set; }

        public List<CategoryEntity> Categories { get; set; }

        public List<ItemEntity> Items { get; set; }
    }
}
