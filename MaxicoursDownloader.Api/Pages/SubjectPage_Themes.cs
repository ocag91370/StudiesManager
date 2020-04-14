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
        private IWebElement ThemeElement(int themeId) => ThemesContainerElement.FindElement(By.XPath($"//a[contains(@href, '/{themeId}')]"));

        public List<ThemeEntity> GetAllThemes()
        {
            return ThemeElementList.Select(o => GetTheme(o)).ToList();
        }

        private ThemeEntity GetTheme(IWebElement themeElement)
        {
            var url = themeElement.GetAttribute("href");
            var name = themeElement.GetAttribute("title");

            var reference = FromUrl(url);

            var model = new ThemeEntity
            {
                Id = reference.ThemeId,
                Tag = name.CleanName(),
                Name = name,
                Url = url
            };

            return model;
        }

        private ThemeEntity GetTheme(ReferenceEntity reference)
        {
            var skipNb = Current.Arbo.Count();

            if (!reference.Arbo.Skip(skipNb).Any())
                return null;

            int themeId =  reference.Arbo.Skip(skipNb).FirstOrDefault();

            var element = ThemeElement(themeId);

            return GetTheme(element);
        }
    }
}
