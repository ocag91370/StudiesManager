using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Models;
using OpenQA.Selenium;
using System;
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

        private readonly MaxicoursSettingsModel _maxicoursSettings;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Url = string.Empty;
            Current = null;
            _maxicoursSettings = null;
        }

        public BasePage(MaxicoursSettingsModel maxicoursSettings, IWebDriver driver, string url)
        {
            _maxicoursSettings = maxicoursSettings;
            Driver = driver;
            Url = url;
            Current = FromUrl(url);

            GoTo();
        }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(Url);
        }

#pragma warning disable
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
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["paths"]);
                        case var parcours_pivot when path.Contains("/cours/fiche-synthese"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["summary_sheets"]);
                        case var parcours_pivot when path.Contains("/cours/fiche"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["lessons"]);
                        case var parcours_pivot when path.Contains("/cours/video"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["video_lessons"]);
                        case var parcours_pivot when path.Contains("/exercices/enonce_corrige_video"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["video_exercices"]);
                        case var parcours_pivot when path.Contains("/exercices/pazapa"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["pazapa"]);
                        case var parcours_pivot when path.Contains("/exercices/controle_pdf"):
                            return FromCategoryUrl(HttpUtility.ParseQueryString(uri.Query)["_vp"], _maxicoursSettings.Categories["tests"]);
                    }
                    break;
            }

            return null;
        }
#pragma warning restore

        public ReferenceEntity FromArboUrl(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            var schoolLevelId = idList?.FirstOrDefault() ?? 0;
            var subjectId = idList?.Skip(1)?.FirstOrDefault() ?? 0;
            var themeId = idList?.Skip(2)?.FirstOrDefault() ?? 0;

            return new ReferenceEntity(idList, schoolLevelId, subjectId, themeId);
        }

        public ReferenceEntity FromQuizzUrl(string path)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            var schoolLevelId = idList?.FirstOrDefault() ?? 0;
            var subjectId = idList?.Skip(1)?.LastOrDefault() ?? 0;
            var themeId = idList?.Skip(2)?.LastOrDefault() ?? 0;

            return new ReferenceEntity(idList, schoolLevelId, subjectId, themeId, "serie-qcm");
        }

        public ReferenceEntity FromCategoryUrl(string path, string categoryId)
        {
            var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

            var schoolLevelId = idList?.FirstOrDefault() ?? 0;
            var subjectId = idList?.Skip(1)?.FirstOrDefault() ?? 0;
            var themeId = idList?.Skip(2)?.SkipLast(1)?.LastOrDefault() ?? 0;
            var ItemId = idList?.LastOrDefault() ?? 0;

            return new ReferenceEntity(idList, schoolLevelId, subjectId, themeId, categoryId, ItemId);
        }

        //private ReferenceEntity FromUrl(string path, string categoryId)
        //{
        //    var idList = path.Split('/').Where(o => int.TryParse(o, out var value)).Select(o => int.Parse(o)).ToList();

        //    var schoolLevelId = idList?.FirstOrDefault() ?? 0;
        //    var subjectId = idList?.Skip(1)?.FirstOrDefault() ?? 0;
        //    var themeId = idList?.Skip(2)?.SkipLast(1)?.LastOrDefault() ?? 0;
        //    var ItemId = idList?.LastOrDefault() ?? 0;

        //    return new ReferenceEntity(idList, schoolLevelId, subjectId, themeId, categoryId, ItemId);
        //}
    }
}