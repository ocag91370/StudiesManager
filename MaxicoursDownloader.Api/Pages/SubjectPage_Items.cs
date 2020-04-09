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
        private IWebElement ItemsContainerElement => ContainerElement.FindElement(By.XPath("//*[@class = 'lsi-crn-container']//*[@class = 'panes']"));

        private IEnumerable<IWebElement> ItemElementList => ItemsContainerElement.FindElements(By.XPath("//*[contains(@class, '  overable')]"));

        public List<ItemEntity> GetAllItems()
        {
            var result = ItemElementList.Select(o => GetItem(o)).ToList();

            return result;
        }

        public List<ItemEntity> GetItemsOfCategory(string categoryId)
        {
            var itemElementList = ItemsContainerElement.FindElements(By.XPath($"//*[contains(@class, '{categoryId}  overable')]"));
            var result = itemElementList.Select(o => GetItem(o)).ToList();

            return result;
        }

        private ItemEntity GetItem(IWebElement element)
        {
            var categoryId = GetCategoryId(element);

            var itemElement = element.FindElement(By.XPath("td[@class = 'label']/a"));

            var url = itemElement.GetAttribute("href");
            var themeId = GetThemeId(url);
            var itemId = GetItemId(url);

            var name = itemElement.GetAttribute("title");

            var entity = new ItemEntity
            {
                ThemeId = themeId,
                CategoryId = categoryId,
                ItemId = itemId,
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
