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
    public partial class WorkPanelPom : BasePom
    {
        public List<SubjectEntity> GetSubjects() => GetSubjectPanels().Select(o => o.GetSubject()).ToList();

        private List<SubjectPanelPom> GetSubjectPanels() => GetSubjectElements().Select(o => new SubjectPanelPom(Driver, o)).ToList();
    }

    public partial class WorkPanelPom
    {
        public WorkPanelPom(IWebDriver driver, IWebElement containerElement) : base(driver)
        {
            ContainerElement = containerElement;
        }

        public IWebElement ContainerElement { get; set; }

        private List<IWebElement> GetSubjectElements() => ContainerElement.FindElements(By.XPath($"//*[@class = 'ed-card width-epingle']")).ToList();
    }
}
