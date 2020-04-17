using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Repositories
{
    public class CategoryRepository
    {
        public static readonly Dictionary<string, string> Types = new Dictionary<string, string>
        {
            { "path", "parcours_pivot" },
            { "summary_sheet", "fiche_synthese" },
            { "lesson", "fiche" },
            { "video", "video" },
            { "video_correction", "enonce_corrige_video" },
            { "pazapa", "pazapa" },
            { "test", "controle_pdf" }
        };
    }
}
