using MaxicoursDownloader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private MaxicoursSettingsModel _maxicoursSettings { get; set; }

        public ConfigurationController(IOptions<MaxicoursSettingsModel> configuration)
        {
            _maxicoursSettings = configuration.Value;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetConfiguration()
        {
            return Ok(_maxicoursSettings);
        }
    }
}