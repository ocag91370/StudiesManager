using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaxicoursDownloader.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSameAs(this string @this, string value)
        {
            return @this.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static int[] SplitUrl(this string @this)
        {
            var url = @this;

            url = url.Split('?').First();
            //if (url.Contains("?"))
            //{
            //    var index = url.IndexOf('?');
            //    url = url.Substring(0, index);
            //}

            //url = url
            //    .Replace("https://entraide-covid19.maxicours.com", "")
            //    .Replace("/LSI/prod/Arbo/home/bo/", "")
            //    .Replace("/LSI/prod/Accueil/", "");

            //var values = new List<int>();
            //url.Split('/').ToList().ForEach(o => {
            //    if (int.TryParse(o, out var value))
            //        values.Add(value);
            //});

            var values = url.Split('/')
                .Where(o => int.TryParse(o, out var value))
                .Select(o => int.Parse(o))
                .ToArray();

            return values;
        }

        public static string GetUrlParameter(this string @this, string key)
        {
            var url = @this.Substring(@this.IndexOf("?"));
            return HttpUtility.ParseQueryString(url)?[key];
        }

        public static List<int> GetBreadcrumbFromUrl(this string @this)
        {
            var uri = new Uri(@this);

            var path = uri.LocalPath.ToLower();

            switch (path)
            {
                case var arbo when path.Contains("/home/bo/"):
                    return path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

                case var parcours when path.Contains("parcours"):
                case var cours when path.Contains("cours"):
                case var exercices when path.Contains("exercices"):
                    //var url = $"{uri.Scheme}://{uri.Host}{HttpUtility.ParseQueryString(uri.Query)["_vp"]}";
                    //return url.GetBreadcrumbFromUrl();
                    return HttpUtility.ParseQueryString(uri.Query)["_vp"].Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();
            }

            return new List<int>();
        }

        public static int? GetUrlParameterAsInt(this string @this, string key)
        {
            if (int.TryParse(@this.GetUrlParameter(key), out var value))
                return value;

            return new int?();
        }

        public static string DecodeUrl(this string @this)
        {
            return HttpUtility.UrlDecode(@this);
        }

        public static string CleanName(this string @this)
        {
            var invalidCharacters = new List<char>();
            var substituteValue = string.Empty;

            invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidPathChars());
            invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidFileNameChars());
            invalidCharacters.AddRange((IEnumerable<char>)new char[] { ':', '?', '"', '\\', '/' });

            var result = new StringBuilder(@this);

            foreach (var character in invalidCharacters)
            {
                result = result.Replace(character.ToString(), substituteValue);
            }

            return result.ToString();
        }
    }
}