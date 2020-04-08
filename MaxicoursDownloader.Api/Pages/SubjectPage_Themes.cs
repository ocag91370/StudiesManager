using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SubjectPage : BasePage
    {
        private IWebElement ThemesContainerElement => ContainerElement.FindElement(By.XPath("//*[@class = 'lsi-cartouche-milieu']"));

        private IEnumerable<IWebElement> ThemeElementList => ThemesContainerElement.FindElements(By.TagName("a"));

        public List<ThemeEntity> GetAllThemes()
        {
            return ThemeElementList.Select(o => GetTheme(o)).ToList();
        }

        private ThemeEntity GetTheme(IWebElement themeElement)
        {
            var url = themeElement.GetAttribute("href");
            var ids = url.SplitUrl();

            var model = new ThemeEntity
            {
                ThemeId = ids[2],
                Name = themeElement.FindElement(By.TagName("span")).Text,
                Url = url
            };

            return model;
        }
    }
}
