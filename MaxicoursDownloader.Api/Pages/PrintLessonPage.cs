using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using OpenQA.Selenium;
using StudiesManager.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class PrintLessonPage : BasePage
    {
        private IWebElement LessonElement => Driver.FindElement(By.XPath("//*[@id = 'cours']"));

        public PrintLessonPage(IWebDriver driver, string url) : base(driver)
        {
            Url = url;
            GoTo();
        }

        public string GetHtmlLesson()
        {
            var htmlLesson = GetHtmlPage(LessonElement.GetOuterHtml());

            return htmlLesson;
        }
    }
}
