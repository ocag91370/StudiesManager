using System.Collections.Generic;

namespace MaxicoursDownloader.Api.Models.Audit
{
    public class AuditCategoryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<AuditItemModel> Items { get; set; }
    }
}