using MaxicoursDownloader.Api.Models;
using System;
using System.Linq;
using System.Web;

namespace MaxicoursDownloader.Api.Repositories
{
    public static class ReferenceFactory
    {
        public static ReferenceModel FromUrl(string url)
        {
            var uri = new Uri(url);
            var path = uri.LocalPath.ToLower();

            switch (path)
            {
                case var arbo when path.Contains("/arbo/home/bo/"):
                case var home when path.Contains("/home/bo/"):
                    return FromArboUrl(path);

                case var quizz when path.Contains("quizz"):
                    return FromQuizzUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"]);

                case var parcours when path.Contains("parcours"):
                case var cours when path.Contains("cours"):
                case var exercices when path.Contains("exercices"):
                    switch (path)
                    {
                        case var parcours_pivot when path.Contains("/prod/parcours/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "parcours_pivot");
                        case var parcours_pivot when path.Contains("/cours/fiche/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "fiche");
                        case var parcours_pivot when path.Contains("/cours/video/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "video");
                        case var parcours_pivot when path.Contains("/cours/fiche-synthese/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "fiche_synthese");
                        case var parcours_pivot when path.Contains("/exercices/enonce_corrige_video/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "enonce_corrige_video");
                        case var parcours_pivot when path.Contains("/exercices/pazapa/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "pazapa");
                        case var parcours_pivot when path.Contains("/exercices/controle_pdf/"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "controle_pdf");
                    }
                    break;
            }

            return null;
        }

        public static ReferenceModel FromArboUrl(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            int themeId = idList.Skip(2).Any() ? idList.Last() : int.MinValue;

            return new ReferenceModel(idList, idList.First(), idList.Skip(1).First(), themeId);
        }

        public static ReferenceModel FromQuizzUrl(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            int themeId = idList.Skip(2).Any() ? idList.Last() : int.MinValue;

            return new ReferenceModel(idList, idList.First(), idList.Skip(1).Last(), themeId, "serie-qcm");
        }

        public static ReferenceModel FromCategoryUrl(string path, string categoryId)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            return new ReferenceModel(idList, idList.First(), idList.Skip(1).First(), idList.Skip(2).SkipLast(1).Last(), categoryId, idList.Last());
        }
    }
}
