using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Entities
{
    public class HeaderEntity
    {
        public BreadcrumbEntity Current { get; set; }

        public List<BreadcrumbEntity> Breadcrumb { get; set; }
    }
}
