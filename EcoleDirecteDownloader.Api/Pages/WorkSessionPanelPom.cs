using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using EcoleDirecteDownloader.Api.Models;
using StudiesManager.Services.Models;
using System.Collections.Generic;
using System.Linq;
using EcoleDirecteDownloader.Api.Entities;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class WorkSessionPanelPom : BasePom
    {
        public WorkEntity GetWork()
        {
            var title = "Contenus de séances";
            var subjects = GetSubjectPanels().Select(o => o.GetSubject()).ToList();
            var html = $"<h3>{title}</h3>{string.Join("", subjects.Select(o => o.Html))}";

            return new WorkEntity
            {
                Title = title,
                Subjects = subjects,
                Html = html
            };
        }
    }

    public partial class WorkSessionPanelPom
    {
        public IWebElement ContainerElement { get; set; }

        public WorkSessionPanelPom(IWebDriver driver, IWebElement containerElement) : base(driver)
        {
            ContainerElement = containerElement;
        }

        private List<IWebElement> GetSubjectElements() => ContainerElement.FindElements(By.XPath($"//*[@class = 'ed-card width-epingle']")).ToList();

        private List<SubjectPanelPom> GetSubjectPanels() => GetSubjectElements().Select(o => new SubjectPanelPom(Driver, o)).ToList();
    }
}
