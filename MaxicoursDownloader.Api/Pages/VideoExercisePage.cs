using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using StudiesManager.Services.Extensions;
using MaxicoursDownloader.Models;
using StudiesManager.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var videoElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video[@src]"), 1, 5);
            var videoUrl = videoElement.GetAttribute("src");
            Debug.Assert(!string.IsNullOrWhiteSpace(videoUrl));

            if (videoUrl.Contains("423453"))
            {
                videoElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video[@src and (contains(@src, '423453') = false)]"), 1, 10);
                videoUrl = videoElement.GetAttribute("src");
            }

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
