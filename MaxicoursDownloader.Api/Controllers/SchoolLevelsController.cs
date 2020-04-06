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
    [Route("schoollevels")]
    public class SchoolLevelsController : ControllerBase
    {
        private readonly ILogger<CursusController> _logger;

        private readonly IWebDriver _driver;

        public SchoolLevelsController(ILogger<CursusController> logger)
        {
            _logger = logger;
            _driver = new ChromeDriver();
        }

        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            var uri = @"https://entraide-covid19.maxicours.com/LSI/prod/Accueil/?_eid=8a2h90iqc1beguudv1covmqau0";

            var page = new SchoolLevelPage(_driver, uri);

            var subjectList = page.GetAllSubjects();

            return Ok(subjectList);
        }
    }
}
