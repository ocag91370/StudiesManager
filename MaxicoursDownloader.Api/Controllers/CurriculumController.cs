using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using MaxicoursDownloader.Api.Pages;
using OpenQA.Selenium.Chrome;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("curriculum")]
    public class CurriculumController : ControllerBase, IDisposable
    {
        private readonly ILogger<CursusController> _logger;

        private readonly IWebDriver _driver;

        public CurriculumController(ILogger<CursusController> logger)
        {
            _logger = logger;
            _driver = new ChromeDriver();
        }

        [HttpGet]
        public void GetCurricullum()
        {
            var uri = @"https://entraide-covid19.maxicours.com/LSI/prod/Accueil/?_eid=8a2h90iqc1beguudv1covmqau0";

            var page = new CurriculumPage(_driver, uri);
            page.Navigate();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
