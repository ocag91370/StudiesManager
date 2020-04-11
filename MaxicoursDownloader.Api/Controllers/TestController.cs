using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxicoursDownloader.Api.Extensions;
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
            var urls = new List<string>() {
                "https://entraide-covid19.maxicours.com/LSI/prod/Accueil/?_eid=fcafrfk6crdp0qmdnk3jllja16",
                "https://entraide-covid19.maxicours.com/LSI/prod/Arbo/home/bo/5006/5088?_eid=fcafrfk6crdp0qmdnk3jllja16",
                "https://entraide-covid19.maxicours.com/LSI/prod/Arbo/home/bo/5006/5088/25546?_eid=fcafrfk6crdp0qmdnk3jllja16",
                "https://entraide-covid19.maxicours.com/LSI/prod/Parcours/?act=init&from=fiche&oid=536571&nid=25546&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25732%2Fopd%2F536571&_eid=fcafrfk6crdp0qmdnk3jllja16",
                "https://entraide-covid19.maxicours.com/W/cours/fiche/visualiser.php?oid=536571&fromNid=25732&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25732%2Fopd%2F536571&_eid=fcafrfk6crdp0qmdnk3jllja16",
                "https://entraide-covid19.maxicours.com/W/exercices/enonce_corrige_video/index.php?oid=246384&fromNid=25404&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25404%2Fopd%2F246384&_eid=fcafrfk6crdp0qmdnk3jllja16"
            };

            var result = urls.Select(o => new { Url = o, Result = o.GetFromUrl() }).ToList();

            return Ok(result);
        }
    }
}
