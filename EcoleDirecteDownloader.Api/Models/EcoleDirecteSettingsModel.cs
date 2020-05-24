using System.Collections.Generic;
using System.Linq;

namespace EcoleDirecteDownloader.Api.Models
{
    public class EcoleDirecteSettingsModel
    {
        public string StartUrl { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string ExportPath { get; set; }
    }
}