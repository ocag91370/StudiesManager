﻿using MaxicoursDownloader.Api.Entities;
using StudiesManager.Common.Extensions;
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

        public List<ItemEntity> GetAllItems()
        {
            var categoryList = GetAllCategories();
            var themeList = GetAllThemes();

            var elementList = ContainerElement.FindElements(By.XPath($"//*[contains(@class,'  overable')]//*[@class = 'label']/a"));

            var result = elementList.Select((element, index) => GetItem(categoryList, themeList, element, index)).ToList();

            return result;
        }

        public List<ItemEntity> GetItemsOfCategory(string categoryId)
        {
            var categoryList = GetAllCategories();
            var themeList = GetAllThemes();

            var elementList = ContainerElement.FindElements(By.XPath($"//*[@class = '{categoryId}  overable']//*[@class = 'label']/a"));

            var result = elementList.Select((element, index) => GetItem(categoryList, themeList, element, index)).ToList();

            return result;
        }

        public ItemEntity GetItemOfCategory(string categoryId, int itemId)
        {
            var result = GetItemsOfCategory(categoryId).FirstOrDefault(o => o.Id == itemId);

            return result;
        }

        public ItemEntity GetItemOfCategory(string categoryId, ItemKeyModel itemKey)
        {
            var result = GetItemsOfCategory(categoryId).FirstOrDefault(o => o.Id == itemKey.Id && o.Index == itemKey.Index);

            return result;
        }

        private ItemEntity GetItem(List<CategoryEntity> categoryList, List<ThemeEntity> themeList, IWebElement element, int index)
        {
            var url = element.GetAttribute("href");
            var reference = FromUrl(url);

            var name = element.GetAttribute("title");

            var category = GetCategory(categoryList, reference);
            var theme = GetTheme(themeList, reference);

            var entity = new ItemEntity
            {
                SummarySubject = _summarySubject,
                Theme = theme,
                Category = category,
                Id = reference.ItemId,
                Tag = name.CleanName(),
                Name = name,
                Url = url,
                Index = index + 1
            };

            return entity;
        }
    }
}
