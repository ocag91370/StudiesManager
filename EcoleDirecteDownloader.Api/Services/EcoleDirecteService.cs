using AutoMapper;
using EcoleDirecteDownloader.Api.Contracts;
using EcoleDirecteDownloader.Api.Models;
using EcoleDirecteDownloader.Api.Pages;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using StudiesManager.Common.Extensions;
using StudiesManager.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Services
{
    public class EcoleDirecteService : IEcoleDirecteService
    {
        private readonly EcoleDirecteSettingsModel _ecoleDirecteSettings;

        private readonly IMapper _mapper;

        public IWebDriver Driver { get; private set; }

        public EcoleDirecteService(IOptions<EcoleDirecteSettingsModel> configuration, IMapper mapper)
        {
            _ecoleDirecteSettings = configuration.Value;
            _mapper = mapper;

            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        public bool Home()
        {
            var page = Connect();
            if (page.IsNull())
                return false;

            return true;
        }

        public bool HomeworkBook()
        {
            var page = GoToHomeworkBookPage();

            if (page.IsNull())
                return false;

            return true;
        }

        public string GetWorkToDo(DateTime date)
        {
            return GoToHomeworkBookPage(date)?.GetWorkToDo();
        }

        public string GetSessionsContent(DateTime date)
        {
            return GoToHomeworkBookPage(date)?.GetSessionsContent();
        }

        private LoginPage GetLoginPage()
        {
            var loginPage = new LoginPage(_ecoleDirecteSettings, Driver, _ecoleDirecteSettings.StartUrl);
            Debug.Assert(loginPage.IsNotNull());

            return loginPage;
        }

        private NavigationBar Connect()
        {
            var loginPage = GetLoginPage();

            loginPage.Connect();

            var menuPage = new NavigationBar(Driver);
            Debug.Assert(menuPage.IsNotNull());

            return menuPage;
        }

        private HomeworkBookPage GoToHomeworkBookPage()
        {
            var menuPage = Connect();

            menuPage.GoToHomeworkBook();

            var homeworkBookPage = new HomeworkBookPage(Driver);
            Debug.Assert(homeworkBookPage.IsNotNull());

            return homeworkBookPage;
        }

        private HomeworkBookPage GoToHomeworkBookPage(DateTime date)
        {
            var menuPage = Connect();

            menuPage.GoToHomeworkBook();

            var homeworkBookPage = new HomeworkBookPage(Driver);
            Debug.Assert(homeworkBookPage.IsNotNull());

            homeworkBookPage.GoToDate(date);

            return homeworkBookPage;
        }

        public void Dispose()
        {
            if (Driver == null)
                return;

            Driver.Quit();
            Driver.Dispose();
            Driver = null;
        }
    }
}
