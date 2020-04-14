using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var urls = UrlRepository.Urls.Values;

            var result = urls.Select(o => new { Url = o, Result = ReferenceFactory.FromUrl(o) }).ToList();

            return Ok(result);
        }
    }
}
