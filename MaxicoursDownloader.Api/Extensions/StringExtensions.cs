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
    }
}