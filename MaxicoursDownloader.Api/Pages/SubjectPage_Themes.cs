using MaxicoursDownloader.Api.Entities;
using StudiesManager.Common.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SubjectPage : BasePage
    {
        private IEnumerable<IWebElement> ThemeElementList => ContainerElement.FindElements(By.XPath("//*[contains(@class, '_2020-cartouche-matiere')]//*[@class = 'item-url']"));

        private IWebElement ThemeElement(int themeId) => ContainerElement.FindElement(By.XPath($"//*[contains(@class, '_2020-cartouche-matiere')]/a[contains(@href, '/{themeId}')]"));

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
            if (reference.ThemeId == 0)
                return null;

            var skipNb = Current.Arbo.Count();

            if (!reference.Arbo.Skip(skipNb).Any())
                return null;

            int themeId =  reference.Arbo.Skip(skipNb).FirstOrDefault();

            var element = ThemeElement(themeId);

            return GetTheme(element);
        }

        private ThemeEntity GetTheme(List<ThemeEntity> themeList, ReferenceEntity reference)
        {
            var skipNb = Current.Arbo.Count();

            if (!reference.Arbo.Skip(skipNb).Any())
                return null;

            int themeId = reference.Arbo.Skip(skipNb).FirstOrDefault();

            var theme = themeList.FirstOrDefault(o => o.Id == themeId);

            return theme;
        }
    }
}
