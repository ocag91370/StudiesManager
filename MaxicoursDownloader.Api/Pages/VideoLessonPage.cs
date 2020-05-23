using MaxicoursDownloader.Api.Entities;
using StudiesManager.Services.Extensions;
using MaxicoursDownloader.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class VideoLessonPage : BasePage
    {
        private readonly ItemEntity _item;

        //private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video"));

        public VideoLessonPage(MaxicoursSettingsModel settings, IWebDriver driver, ItemEntity item) : base(settings, driver, item.Url)
        {
            _item = item;
        }

        public VideoLessonEntity GetVideoLesson()
        {
            var videoElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video[@src]"), 1, 5);
            var videoUrl = videoElement.GetAttribute("src");
            Debug.Assert(!string.IsNullOrWhiteSpace(videoUrl));

            if (videoUrl.Contains("423453"))
            {
                var videoPlayerElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']"), 1, 5);
                videoPlayerElement.Click();
                videoElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video[@src and (contains(@src, '423453') = false)]"), 1, 5);
                videoUrl = videoElement.GetAttribute("src");
            }

            return new VideoLessonEntity
            {
                Item = _item,
                VideoUrl = videoUrl
            };
        }
    }
}
