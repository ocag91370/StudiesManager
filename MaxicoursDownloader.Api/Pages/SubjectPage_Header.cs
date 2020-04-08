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
        private IEnumerable<IWebElement> BreadcrumbElementList => Driver.FindElements(By.XPath("//*[contains(@class, 'breadcrumb-item-')]/a"));

        private IWebElement CurrentElement => Driver.FindElement(By.XPath("//*[contains(@class, 'breadcrumb-side-current')]/a"));

        public HeaderEntity GetHeader()
        {
            return new HeaderEntity {
                Current = GetCurrent(),
                Breadcrumb = GetBreadcrumb()
            };
        }

        private BreadcrumbEntity GetCurrent()
        {
            return GetBreadcrumItem(CurrentElement);
        }

        private List<BreadcrumbEntity> GetBreadcrumb()
        {
            return BreadcrumbElementList.Select(o => GetBreadcrumItem(o)).ToList();
        }

        private BreadcrumbEntity GetBreadcrumItem(IWebElement element)
        {
            var url = element.GetAttribute("href");
            var id = GetBreadcrumItemId(element);
            var name = element.FindElement(By.TagName("span")).Text;

            return new BreadcrumbEntity
            {
                Id = id,
                Name = name,
                Url = url
            };
        }

        private int GetBreadcrumItemId(IWebElement element)
        {
            var ids = element.GetAttribute("href").SplitUrl();

            if (ids.Any())
                return ids.Last();

            ids = CurrentElement.GetAttribute("href").SplitUrl();

            return ids.Any() ? ids.First() : -1;
        }
    }
}
