using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSameAs(this string @this, string value)
        {
            return @this.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static int[] SplitUrl (this string @this)
        {
            var url = @this.Replace("https://entraide-covid19.maxicours.com/LSI/prod/Arbo/home/bo/", "");

            if (url.Contains("?"))
            {
                var index = url.IndexOf('?');
                url = url.Substring(0, index);
            }

            var values = new List<int>();
            url.Split('/').ToList().ForEach(o => {
                if (int.TryParse(o, out var value))
                    values.Add(value);
            });

            return values.ToArray();
        }
    }
}
