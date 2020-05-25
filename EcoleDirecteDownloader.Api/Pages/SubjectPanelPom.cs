using EcoleDirecteDownloader.Api.Entities;
using OpenQA.Selenium;
using StudiesManager.Services.Extensions;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class SubjectPanelPom : BasePom
    {
        public SubjectEntity GetSubject ()
        {
            var title = GetTitleElement().Text;
            var content = GetContentElement().Text;
            var html = ContainerElement.GetOuterHtml();

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

        private IWebElement GetTitleElement() => ContainerElement.FindElement(By.XPath($"//*[contains(@class, 'panel-heading-devoir')]"));

        private IWebElement GetContentElement() => ContainerElement.FindElement(By.XPath($"//*[@class = 'row']"));

        private IWebElement GetFooterElement() => ContainerElement.FindElement(By.XPath($"//*[@class = 'footer']"));

        public SubjectPanelPom(IWebDriver driver, IWebElement containerElement) : base(driver)
        {
            ContainerElement = containerElement;
        }
    }
}
