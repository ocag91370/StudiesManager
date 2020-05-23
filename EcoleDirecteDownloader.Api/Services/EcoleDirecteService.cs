using AutoMapper;
using EcoleDirecteDownloader.Api.Contracts;
using EcoleDirecteDownloader.Api.Pages;
using EcoleDirecteDownloader.Models;
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
        private IWebDriver Driver;

        public EcoleDirecteService(IOptions<EcoleDirecteSettingsModel> configuration, IMapper mapper)
        {
            _ecoleDirecteSettings = configuration.Value;
            _mapper = mapper;

            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        private LoginPage GetLoginPage()
        {
            var homePage = new LoginPage(_ecoleDirecteSettings, Driver, _ecoleDirecteSettings.StartUrl);
            Debug.Assert(homePage.IsNotNull());

            return homePage;
        }
    }
}
