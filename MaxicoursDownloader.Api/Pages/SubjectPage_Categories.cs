using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

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

        private CategoryEntity GetCategory(ReferenceEntity reference)
        {
            var categoryId = reference.CategoryId;

            var element = CategoryElement(categoryId);

            return GetCategory(element);
        }

        private CategoryEntity GetCategory(List<CategoryEntity> categoryList, ReferenceEntity reference)
        {
            var categoryId = reference.CategoryId;

            var category = categoryList.FirstOrDefault(o => o.Id == categoryId);

            return category;
        }
    }
}