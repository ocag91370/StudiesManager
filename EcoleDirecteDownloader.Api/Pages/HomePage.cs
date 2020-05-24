using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomePage : BasePage
    {
        public NavigationBar GetNavigationBar() => new NavigationBar(Driver);
    }

    public partial class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }
    }
}
