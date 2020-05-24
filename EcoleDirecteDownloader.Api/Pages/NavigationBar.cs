using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class NavigationBar : BasePage
    {
        public HomeworkBookPage GoToHomeworkBook()
        {
            GetHomeworkBookElement().Click();

            return new HomeworkBookPage(Driver);
        }
    }

    public partial class NavigationBar : BasePage
    {
        public NavigationBar(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement GetHomeworkBookElement() => Driver.FindElement(By.XPath("//*[*[@class = 'icon-ed_cahierdetexte']]"), 1, 5);
    }
}
