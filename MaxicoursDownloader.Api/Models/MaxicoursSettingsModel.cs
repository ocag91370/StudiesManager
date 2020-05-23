using StudiesManager.Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Models
{
    public class MaxicoursSettingsModel
    {
        public string StartUpKey { get; set; }

        public string Token { get; set; }

        public string ExportPath { get; set; }

        private Dictionary<string, string> _urls;
        public Dictionary<string, string> Urls
        {
            get { return _urls; }
            set
            {
                _urls = value.Keys.ToDictionary(key => key, key => value[key].DecodeUrl());
            }
        }

        public Dictionary<string, string> Categories { get; set; }

        public string StartUpUrl => string.Format(Urls[StartUpKey], Token);
    }
}