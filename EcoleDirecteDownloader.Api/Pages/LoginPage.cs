using EcoleDirecteDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class LoginPage : BasePage
    {
        public HomePage Connect()
        {
            try
            {
                UserNameElement().SendKeys(_ecoleDirecteSettings.Login);
                PasswordElement().SendKeys(_ecoleDirecteSettings.Password);

                ConnectElement().Click();

                return new HomePage(Driver);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
 
    public partial class LoginPage
    {
        private IWebElement UserNameElement() => Driver.FindElement(By.Id("username"));

        private IWebElement PasswordElement() => Driver.FindElement(By.Id("password"));

        private IWebElement ConnectElement() => Driver.FindElement(By.Id("connexion"));

        public LoginPage(EcoleDirecteSettingsModel settings, IWebDriver driver, string url) : base(settings, driver, url)
        {
        }
    }
}