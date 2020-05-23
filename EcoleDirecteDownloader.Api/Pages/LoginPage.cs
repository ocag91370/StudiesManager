using EcoleDirecteDownloader.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(EcoleDirecteSettingsModel settings, IWebDriver driver, string url) : base(settings, driver, url)
        {
        }
    }
}
