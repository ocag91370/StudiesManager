using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class SummarySheetPage : BasePage
    {
        private readonly ItemEntity _item;

        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[contains(@class, 'lsi-structure-milieu')]"));

        private IWebElement PrintElement => ContainerElement.FindElement(By.XPath($"//*[img[@class = 'picto-imprimer']]"));

        private IWebElement AudioElement => ContainerElement.FindElement(By.XPath($"//*[@class = 'audio-position']/a"));

        public SummarySheetPage(IWebDriver driver, ItemEntity item) : base(driver, item.Url + "&act=audio")
        {
            _item = item;
        }

        public SummarySheetEntity GetSummarySheet()
        {
            var printUrl = PrintElement.GetAttribute("href");
            var audioUrl = AudioElement.GetAttribute("href");

            return new SummarySheetEntity
            {
                Item = _item,
                PrintUrl = printUrl,
                AudioUrl = audioUrl
            };
        }
    }
}
