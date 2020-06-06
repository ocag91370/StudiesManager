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
    public partial class LessonPage : BasePage
    {
        private readonly ItemEntity _item;

        private IWebElement PrintElement => Driver.FindElement(By.XPath("//a[*[@class = 'picto-imprimer']]"));

        private List<IWebElement> SwfElementList => Driver.FindElements(By.XPath("//embed")).ToList();

        public LessonPage(MaxicoursSettingsModel settings, IWebDriver driver, ItemEntity item) : base(settings, driver, item.Url)
        {
            _item = item;
        }

        public LessonEntity GetLesson()
        {
            var printUrl = PrintElement?.GetAttribute("href")?.Replace("imprimer=1", "imprimer=0");
            var swfUrls = SwfElementList.Select(o => o?.GetAttribute("src")).ToList();
            var mindMapUrl = GetMindMapUrl();

            //var htmlLesson = GetHtmlLesson(printUrl);
            var htmlLesson = GetHtmlLesson();

            return new LessonEntity
            {
                Item = _item,
                HtmlLesson = htmlLesson,
                PrintUrl = printUrl,
                SwfUrls = swfUrls,
                MindMapUrl = mindMapUrl
            };
        }

        public string GetMindMapUrl()
        {
            try
            {
                var iframeElement = Driver.FindElement(By.XPath("//*[@class = 'fiche-au-programme']/iframe"));
                Driver.SwitchTo().Frame(iframeElement);
                var result = Driver.FindElement(By.XPath("//*[@id = 'goofy']/img"))?.GetAttribute("src");
                Driver.SwitchTo().DefaultContent();

                return result;
            }
            catch (Exception)
            {
                Driver.SwitchTo().DefaultContent();
                return string.Empty;
            }
        }

        public string GetHtmlLesson()
        {
            var TitleElement = Driver.FindElement(By.Id("titre"));
            var title = $"<div style='display: block; margin-bottom: 30px; font-family: rawline, arial, sans-serif; font-size: 32px; font-weight: bold; letter-spacing: 0.02em; line-height: 36px; color: #712958; text-align: left;'><span>{TitleElement.Text}</span></div>";

            if (Driver is IJavaScriptExecutor js)
            {
                try
                {
                    var script = $"document.getElementById('titre').remove();";
                    js.ExecuteScript(script);
                }
                catch (Exception)
                {
                }

                try
                {
                    var script = $"document.getElementById('fiche-exercices').remove();";
                    js.ExecuteScript(script);
                }
                catch (Exception)
                {
                }

                try
                {
                    var script = $"document.getElementsByClassName('lsi-annotation')[0].parentElement.remove();";
                    js.ExecuteScript(script);
                }
                catch (Exception)
                {
                }
            }

            var LessonElement = Driver.FindElement(By.XPath("//*[@id = 'cours']/div[2]"));

            var result = GetHtmlPage(title + LessonElement.GetOuterHtml());

            return result;
        }

        public string GetHtmlLesson(string url)
        {
            var printLessonPage = new PrintLessonPage(Driver, url);
            var result = printLessonPage.GetHtmlLesson();

            return result;
        }
    }
}
