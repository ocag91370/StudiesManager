using EcoleDirecteDownloader.Api.Entities;
using OpenQA.Selenium;
using StudiesManager.Services.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class SubjectPanelPom : BasePom
    {
        public SubjectEntity GetSubject ()
        {
            var sectionElements = GetSectionElements();

            var title = sectionElements[0].Text;
            var content = sectionElements[1].Text;
            var html = sectionElements[2].GetOuterHtml();

            return new SubjectEntity {
                Title = title,
                Content = content,
                Html = html
            };
        }
    }

    public partial class SubjectPanelPom
    {
        public IWebElement ContainerElement { get; set; }

        public SubjectPanelPom(IWebDriver driver, IWebElement containerElement) : base(driver)
        {
            ContainerElement = containerElement;
        }

        private List<IWebElement> GetSectionElements() => ContainerElement.FindElements(By.XPath("div")).ToList();
    }
}
