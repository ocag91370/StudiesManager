using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class LessonPage : BasePage
    {
        private readonly SubjectSummaryEntity _subjectSummary;
        private readonly ItemEntity _item;

        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[@class = 'lsi-arbo']"));

        public LessonPage(IWebDriver driver, ItemEntity item) : base(driver)
        {
            _item = item;
        }

        public string GetPrintFormat()
        {
            var url = GetPrintUrl();
            Driver.Navigate().GoToUrl(url);
            var result = Driver.PageSource;

            return result;
        }

        public string GetPrintUrl()
        {
            var url = _item.Url.Replace("visualiser.php", "postit.php");

            return url;
        }
    }
}
