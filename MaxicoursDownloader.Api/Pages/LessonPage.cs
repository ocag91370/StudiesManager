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
        private readonly ItemEntity _item;

        public LessonPage(IWebDriver driver, ItemEntity item) : base(driver)
        {
            _item = item;
        }

        public LessonEntity GetLesson()
        {
            var printUrl = GetPrintUrl();

            return new LessonEntity
            {
                Item = _item,
                PrintUrl = printUrl
            };
        }

        private string GetPrintUrl()
        {
            var url = _item.Url.Replace("visualiser.php", "postit.php");

            return url;
        }
    }
}
