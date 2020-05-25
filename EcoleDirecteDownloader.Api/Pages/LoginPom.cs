using EcoleDirecteDownloader.Api.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class LoginPom : BasePom
    {
        public HomePom Connect()
        {
            try
            {
                UserNameElement().SendKeys(_ecoleDirecteSettings.Login);
                PasswordElement().SendKeys(_ecoleDirecteSettings.Password);

                ConnectElement().Click();

                return new HomePom(Driver);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
 
    public partial class LoginPom
    {
        private IWebElement UserNameElement() => Driver.FindElement(By.Id("username"));

        private IWebElement PasswordElement() => Driver.FindElement(By.Id("password"));

        private IWebElement ConnectElement() => Driver.FindElement(By.Id("connexion"));

        public LoginPom(EcoleDirecteSettingsModel settings, IWebDriver driver, string url) : base(settings, driver, url)
        {
        }
    }
}