using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomePom : BasePom
    {
        public NavigationBarPom GetNavigationBar() => new NavigationBarPom(Driver);
    }

    public partial class HomePom
    {
        public HomePom(IWebDriver driver) : base(driver)
        {
        }
    }
}
