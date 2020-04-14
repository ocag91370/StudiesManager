﻿using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SubjectPage : BasePage
    {
        private IWebElement ItemsContainerElement => ContainerElement.FindElement(By.XPath("//*[@class = 'lsi-crn-container']//*[@class = 'panes']"));

        private IEnumerable<IWebElement> ItemElementList(string categoryId) => ItemsContainerElement.FindElements(By.XPath($"//*[contains(@class, '{categoryId}  overable')]"));

        private IWebElement ItemElement(string categoryId, int itemId) => ItemsContainerElement.FindElement(By.XPath($"//*[contains(@class, '{categoryId}  overable')][./td[@class = 'label']/a[contains(@href, 'oid={itemId}')]]"));

        public List<ItemEntity> GetAllItems()
        {
            var result = GetItemsOfCategory(string.Empty);

            return result;
        }

        public List<ItemEntity> GetItemsOfCategory(string categoryId)
        {
            var categoryList = GetAllCategories();
            var themeList = GetAllThemes();

            var elementList = ContainerElement.FindElements(By.XPath($"//*[contains(@class, '{categoryId}  overable')]//*[@class = 'label']/a"));

            var itemList = new ConcurrentBag<ItemEntity>();
            Parallel.ForEach(elementList, (element) =>
            {
                var item = GetItem(categoryList, themeList, element);
                itemList.Add(item);
            });

            var result = itemList.ToList();

            return result;
        }

        public ItemEntity GetItem(string categoryId, int itemId)
        {
            return GetItem(ItemElement(categoryId, itemId));
        }

        private ItemEntity GetItem(IWebElement element)
        {
            var itemElement = element.FindElement(By.XPath("td[@class = 'label']/a"));

            var url = itemElement.GetAttribute("href");
            var reference = FromUrl(url);

            var name = itemElement.GetAttribute("title");

            var category = GetCategory(reference);
            var theme = GetTheme(reference);

            var entity = new ItemEntity
            {
                SubjectSummary = _subjectSummary,
                Theme = theme,
                Category = category,
                Id = reference.ItemId,
                Tag = name.CleanName(),
                Name = name,
                Url = url
            };

            return entity;
        }

        private ItemEntity GetItem(List<CategoryEntity> categoryList, List<ThemeEntity> themeList, IWebElement element)
        {
            var url = element.GetAttribute("href");
            var reference = FromUrl(url);

            var name = element.GetAttribute("title");

            var category = GetCategory(categoryList, reference);
            var theme = GetTheme(themeList, reference);

            var entity = new ItemEntity
            {
                SubjectSummary = _subjectSummary,
                Theme = theme,
                Category = category,
                Id = reference.ItemId,
                Tag = name.CleanName(),
                Name = name,
                Url = url
            };

            return entity;
        }

        private int GetThemeId(string url)
        {
            var parameter = url.GetUrlParameter("nid");
            if (string.IsNullOrWhiteSpace(parameter))
                parameter = url.GetUrlParameter("fromNid");

            int.TryParse(parameter, out var value);

            return value;
        }

        private int GetItemId(string url)
        {
            var parameter = url.GetUrlParameter("oid");
            int.TryParse(parameter, out var value);

            return value;
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
    }
}
