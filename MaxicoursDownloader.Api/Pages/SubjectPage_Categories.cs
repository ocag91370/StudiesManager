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

        private IWebElement CategoryElement(string categoryId) => CategoriesContainerElement.FindElement(By.XPath($"//a[@rel = '{categoryId}']"));

        public List<CategoryEntity> GetAllCategories()
        {
            return CategoryElementList.Select(o => GetCategory(o)).ToList();
        }

        public void SelectCategory(string categoryId)
        {
            CategoryElement(categoryId)?.Click();
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
            var id = categoryElement.GetAttribute("rel");
            var name = categoryElement.FindElement(By.TagName("span")).Text;

            return new CategoryEntity {
                Id = id,
                Tag = name.CleanName(),
                Name = name
            };
        }

        private CategoryEntity GetCategory(string categoryId)
        {
            var element = CategoryElement(categoryId);

            return GetCategory(element);
        }
    }
}