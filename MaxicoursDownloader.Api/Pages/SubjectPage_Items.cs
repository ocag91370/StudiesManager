using MaxicoursDownloader.Api.Entities;
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

            //var itemList = new ConcurrentBag<ItemEntity>();
            //Parallel.ForEach(elementList, (element, state, index) =>
            //{
            //    var item = GetItem(categoryList, themeList, element, index);
            //    itemList.Add(item);
            //});

            //var result = itemList.ToList();

            var result = elementList.Select((element, index) => GetItem(categoryList, themeList, element, index)).ToList();

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
                SummarySubject = _summarySubject,
                Theme = theme,
                Category = category,
                Id = reference.ItemId,
                Tag = name.CleanName(),
                Name = name,
                Url = url
            };

            return entity;
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
