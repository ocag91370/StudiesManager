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
            var name = element.GetAttribute("title");

            var reference = FromUrl(url);

            var id = reference.Arbo.Last();

            return new BreadcrumbEntity
            {
                Id = id,
                Name = name,
                Url = url
            };
        }
    }
}
