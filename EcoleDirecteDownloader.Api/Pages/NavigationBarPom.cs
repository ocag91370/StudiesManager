using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class NavigationBarPom : BasePom
    {
        public HomeworkBookPom GoToHomeworkBook()
        {
            GetHomeworkBookElement().Click();

            return new HomeworkBookPom(Driver);
        }
    }

    public partial class NavigationBarPom
    {
        public NavigationBarPom(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement GetHomeworkBookElement() => Driver.FindElement(By.XPath("//*[*[@class = 'icon-ed_cahierdetexte']]"), 1, 5);
    }
}
