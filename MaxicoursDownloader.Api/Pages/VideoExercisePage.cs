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

        private IWebElement SolutionElement => ContainerElement.FindElement(By.XPath("//*[@id = 'solutionTexte']"));

        private IWebElement SubjectElement => ContainerElement.FindElement(By.XPath("//*[@class = 'enonce']"));

        private IWebElement VideoElement => ContainerElement.FindElement(By.XPath("//*[@class = 'enonce']"));

        public VideoExercisePage(MaxicoursSettingsModel settings, IWebDriver driver, ItemEntity item) : base(settings, driver, item.Url)
        {
            _item = item;
        }

        public VideoExerciseEntity GetVideoExercise()
        {
            var subject = SubjectElement.Text;
            var solution = SolutionElement.Text;

            //var videoElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video[@src]"), 1000, 5);
            var videoUrl = VideoElement.GetAttribute("src");

            return new VideoExerciseEntity
            {
                Item = _item,
                Subject = subject,
                Solution = solution,
                VideoUrl = videoUrl
            };
        }
    }
}
