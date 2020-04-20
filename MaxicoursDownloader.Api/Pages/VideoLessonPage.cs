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
            var videoElement = Driver.FindElement(By.XPath("//*[@class = 'mxc-jp-jplayer']//video"), 1000);
            var videoUrl = videoElement.GetAttribute("src");

            return new VideoLessonEntity
            {
                Item = _item,
                VideoUrl = videoUrl
            };
        }
    }
}
