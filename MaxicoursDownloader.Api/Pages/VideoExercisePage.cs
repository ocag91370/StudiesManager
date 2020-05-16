using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class VideoExercisePage : BasePage
    {
        private readonly ItemEntity _item;

        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[@id = 'ECV']"));

        private IWebElement TitleElement => ContainerElement.FindElement(By.Id("titre"));

        private IWebElement TextSolutionButtonElement => ContainerElement.FindElement(By.Id("voir-solution"));

        private IWebElement VideoSolutionButtonElement => ContainerElement.FindElement(By.Id("voir-video"));

        private IWebElement SubjectElement => Driver.FindElement(By.ClassName("enonce"));

        private IWebElement SolutionElement => Driver.FindElement(By.ClassName("solutionContenu"));

        public VideoExercisePage(MaxicoursSettingsModel settings, IWebDriver driver, ItemEntity item) : base(settings, driver, item.Url)
        {
            _item = item;
        }

        public VideoExerciseEntity GetVideoExercise()
        {
            var title = TitleElement.Text;

            var subject = GetHtmlPage(GetSeparator("Enoncé") + SubjectElement.GetOuterHtml());

            var solution = GetHtmlPage(subject + GetSeparator("Solution") + SolutionElement.GetOuterHtml());

            VideoSolutionButtonElement.Click();
            var videoUrl = string.Empty;
            do
            {
                var videoSolutionElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video[@src]"), 1, 5);
                videoUrl = videoSolutionElement?.GetAttribute("src") ?? string.Empty;
            }
            while (videoUrl.Contains("423453-high.mp4"));

            return new VideoExerciseEntity
            {
                Item = _item,
                Title = title,
                Subject = subject,
                Solution = solution,
                VideoUrl = videoUrl
            };
        }

        public string GetSeparator(string title)
        {
            var html = $"<div style='border-top: 2px dotted #cacaca; border-bottom: 2px dotted #cacaca; margin-top: 20px; margin-bottom: 20px; padding-top: 5px; padding-bottom: 5px;'><span style='font-family: Verdana,Arial,Helvetica,sans-serif; font-size: 13pt; font-weight: bold; color: #555;'>{title}</span></div>";

            return html;
        }
    }
}
