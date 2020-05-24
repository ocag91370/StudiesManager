using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Globalization;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomeworkPanel : BasePage
    {
        public string GetWorkToDo()
        {
            GetWorkToDoElement().Click();
            DownloadFiles();

            return GetDataElement().GetOuterHtml();
        }

        public string GetSessionsContent()
        {
            GetSessionsContentElement().Click();
            DownloadFiles();

            return GetDataElement().GetOuterHtml();
        }

        public void DownloadFiles()
        {
            GetFileElements().ForEach(o => {
                o.Click();
                System.Threading.Thread.Sleep(5000);
            });
        }
    }

    public partial class HomeworkPanel : BasePage
    {
        private readonly CultureInfo _ci = new CultureInfo("fr-FR");

        private IWebElement GetCurrentElement() => Driver.FindElement(By.Id("tab-devoirs-jour"), 1, 5);

        private IWebElement GetWorkToDoElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'afaire')]"));

        private IWebElement GetSessionsContentElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'crseance')]"));

        private IWebElement GetDataElement() => Driver.FindElement(By.XPath($"//*[(@class = 'encartJour') and (@id != '')]"), 1, 5);

        private List<IWebElement> GetFileElements() => GetDataElement().FindElements(By.XPath($"//a[@file-type = 'FICHIER_CDT']")).ToList();

        public HomeworkPanel(IWebDriver driver) : base(driver)
        {
        }
    }
}
