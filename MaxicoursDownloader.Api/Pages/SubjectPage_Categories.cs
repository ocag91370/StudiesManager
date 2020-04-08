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
        private IWebElement CategoriesContainerElement => ContainerElement.FindElement(By.XPath("//*[@class = 'lsi-crn-container']//*[@class = 'onglets']"));

        private IEnumerable<IWebElement> CategoryElementList => CategoriesContainerElement.FindElements(By.TagName("a"));

        private IWebElement GetCategoryElement(string categoryId) => CategoriesContainerElement.FindElement(By.XPath($"a[@rel = '{categoryId}']"));


        public List<CategoryEntity> GetAllCategories()
        {
            return CategoryElementList.Select(o => GetCategory(o)).ToList();
        }

        public void SelectCategory(string categoryId)
        {
            GetCategoryElement(categoryId)?.Click();
        }

        public void SelectLessonCategory()
        {
            SelectCategory("fiche");
        }

        public void SelectVideoCategory()
        {
            SelectCategory("video");
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