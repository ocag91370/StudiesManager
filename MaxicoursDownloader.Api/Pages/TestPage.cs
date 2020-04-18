using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Pages
{
    public partial class TestPage : BasePage
    {
        private readonly ItemEntity _item;

        private IWebElement ContainerElement => Driver.FindElement(By.XPath("//*[contains(@class, 'lsi-structure-milieu')]"));

        private IWebElement WorkElement => ContainerElement.FindElement(By.XPath($"//*[@class = 'telechargement']/a[@title = 'LE SUJET']"));

        private IWebElement CorrectionElement => ContainerElement.FindElement(By.XPath($"//*[@class = 'telechargement']/a[@title = 'LE CORRIGÉ']"));

        public TestPage(IWebDriver driver, ItemEntity item) : base(driver, item.Url)
        {
            _item = item;
        }

        public TestEntity GetTest()
        {
            var workUrl = WorkElement.GetAttribute("href");
            var correctionUrl = CorrectionElement.GetAttribute("href");

            return new TestEntity
            {
                Item = _item,
                WorkUrl = workUrl,
                CorrectionUrl = correctionUrl
            };
        }
    }
}
