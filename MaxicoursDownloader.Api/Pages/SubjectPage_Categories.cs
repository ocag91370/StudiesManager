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
        private IWebElement CategoriesContainerElement => ContainerElement.FindElement(By.XPath("//*[@class = 'lsi-crn-container']//*[@class = 'container-tabs']"));

        private IEnumerable<IWebElement> CategoryElementList => CategoriesContainerElement.FindElements(By.XPath("//*[@class = 'onglets']//a"));

        private IWebElement GetCategoryElement(string categoryId) => CategoriesContainerElement.FindElement(By.XPath($"//*[@class = 'onglets']//a[@rel = '{categoryId}']"));


        public List<CategoryEntity> GetAllCategories()
        {
            return CategoryElementList.Select(o => GetCategory(o)).ToList();
        }

        public void SelectCategory(string categoryId)
        {
            GetCategoryElement(categoryId)?.Click();
        }

        private CategoryEntity GetCategory(IWebElement categoryElement)
        {
            return new CategoryEntity {
                Id = categoryElement.GetAttribute("rel"),
                Name = categoryElement.FindElement(By.TagName("span")).Text
            };
        }
    }
}