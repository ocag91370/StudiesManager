using MaxicoursDownloader.Api.Entities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaxicoursDownloader.Api.Pages
{
    public class BasePage
    {
        public string UrlPrefix => @"https://entraide-covid19.maxicours.com/";

        public string Url { get; set; }

        public ReferenceEntity Current { get; private set; }

        public IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Url = string.Empty;
            Current = null;
        }

        public BasePage(IWebDriver driver, string url)
        {
            Driver = driver;
            Url = url;
            Current = FromUrl(url);

            GoTo();
        }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(Url);
        }

        public ReferenceEntity FromUrl(string url)
        {
            var uri = new Uri(url);
            var path = uri.LocalPath.ToLower();

            switch (path)
            {
                case var arbo when path.Contains("/arbo/home/bo/"):
                    return FromArboUrl(arbo);

                case var home when path.Contains("/home/bo/"):
                    return FromArboUrl(home);

                case var quizz when path.Contains("quizz"):
                    return FromQuizzUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"]);

                case var parcours when path.Contains("parcours"):
                case var cours when path.Contains("cours"):
                case var exercices when path.Contains("exercices"):
                    switch (path)
                    {
                        case var parcours_pivot when path.Contains("/prod/parcours"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "parcours_pivot");
                        case var parcours_pivot when path.Contains("/cours/fiche"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "fiche");
                        case var parcours_pivot when path.Contains("/cours/video"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "video");
                        case var parcours_pivot when path.Contains("/cours/fiche-synthese"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "fiche_synthese");
                        case var parcours_pivot when path.Contains("/exercices/enonce_corrige_video"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "enonce_corrige_video");
                        case var parcours_pivot when path.Contains("/exercices/pazapa"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "pazapa");
                        case var parcours_pivot when path.Contains("/exercices/controle_pdf"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], "controle_pdf");
                    }
                    break;
            }

            return null;
        }

        public ReferenceEntity FromArboUrl(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            var themeList = idList.Skip(2);
            if (themeList.Any())
                return new ReferenceEntity(idList, idList.First(), idList.Skip(1).First(), themeList.First());

            return new ReferenceEntity(idList, idList.First(), idList.Skip(1).First());
        }

        public ReferenceEntity FromQuizzUrl(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            int themeId = (idList.Skip(2).Any()) ? idList.Last() : int.MinValue;

            return new ReferenceEntity(idList, idList.First(), idList.Skip(1).Last(), themeId, "serie-qcm");
        }

        public ReferenceEntity FromCategoryUrl(string path, string categoryId)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            return new ReferenceEntity(idList, idList.First(), idList.Skip(1).First(), idList.Skip(2).SkipLast(1).Last(), categoryId, idList.Last());
        }

        public List<int> GetIdList(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            return idList;
        }

        public int GetSchoolLevelId(string path)
        {
            return GetIdList(path).First();
        }

        public int GetSubjectId(string path)
        {
            return GetIdList(path).Skip(1).First();
        }

        public List<int> GetThemeList(string path)
        {
            return GetIdList(path).Skip(2).ToList();
        }
    }
}