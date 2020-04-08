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
        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[@class = 'lsi-arbo']"));

        private IWebElement BreadcrumbElement => Driver.FindElement(By.XPath("//*[contains(@class, 'breadcrumb-side-current')]"));

        public SubjectPage(IWebDriver driver, string url) : base(driver, url)
        {
        }

        public string Title => BreadcrumbElement.FindElement(By.XPath("a/span")).Text;

        public string Link => BreadcrumbElement.FindElement(By.TagName("a")).GetAttribute("href");
    }
}
