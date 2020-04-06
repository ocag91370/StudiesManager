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
    [Route("cursus")]
    public class CursusController : ControllerBase, IDisposable
    {
        private readonly ILogger<CursusController> _logger;

        private readonly IWebDriver _driver;

        public CursusController(ILogger<CursusController> logger)
        {
            _logger = logger;
            _driver = new ChromeDriver();
        }

        [HttpGet]
        public void GetCursus()
        {
            var uri = @"https://entraide-covid19.maxicours.com/W/cours/fiche/visualiser.php?oid=240713&fromNid=25732&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25732%2Fopd%2F240713&_eid=8a2h90iqc1beguudv1covmqau0";

            var page = new CursusPage(_driver, uri);
            page.Navigate();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
