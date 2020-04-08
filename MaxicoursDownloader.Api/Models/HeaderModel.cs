using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models
{
    public class HeaderModel
    {
        public BreadcrumbModel Current { get; set; }

        public List<BreadcrumbModel> Breadcrumb { get; set; }
    }
}
