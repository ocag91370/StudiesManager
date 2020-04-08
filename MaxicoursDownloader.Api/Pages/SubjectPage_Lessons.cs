using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SubjectPage : BasePage
    {
        private IWebElement LessonsContainerElement => ContainerElement.FindElement(By.XPath("//*[@class = 'lsi-crn-container']//*[@class = 'panes']"));

        private IEnumerable<IWebElement> LessonElementList => LessonsContainerElement.FindElements(By.TagName("tr[contains(@class, '  overable')]"));

        public List<LessonEntity> GetAllLessons()
        {
            return LessonElementList.Select(o => GetLesson(o)).ToList();
        }

        private LessonEntity GetLesson(IWebElement element)
        {
            var categoryId = GetCategoryId(element);

            var lessonElement = element.FindElement(By.TagName("//*[@class = 'label']"));

            var url = lessonElement.GetAttribute("href");
            var parameters = url.GetUrlParameter("act").DecodeUrl();
            int.TryParse(parameters.GetUrlParameter("nid"), out var themeId);
            int.TryParse(parameters.GetUrlParameter("oid"), out var lessonId);
            var name = lessonElement.Text.Trim();

            var entity = new LessonEntity
            {
                ThemeId = themeId,
                CategoryId = categoryId,
                LessonId = lessonId,
                Name = name,
                Url = url
            };

            return entity;
        }

        private string GetCategoryId(IWebElement element)
        {
            try
            {
                return element.GetAttribute("class").Split("  ")[0];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //private ThemeEntity GetTheme(IWebElement themeElement)
        //{
        //    var url = themeElement.GetAttribute("href");
        //    var ids = url.SplitUrl();

        //    var model = new ThemeEntity
        //    {
        //        ThemeId = ids[2],
        //        Name = themeElement.FindElement(By.TagName("span")).Text,
        //        Url = url
        //    };

        //    return model;
        //}
    }
}
