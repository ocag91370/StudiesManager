using MaxicoursDownloader.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours/covid")]
    public class MaxicoursCovidController : ControllerBase
    {
        private readonly ILogger<MaxicoursCovidController> _logger;
        private readonly MaxicoursService _maxicoursService;

        public MaxicoursCovidController(ILogger<MaxicoursCovidController> logger)
        {
            _logger = logger;
            _maxicoursService = new MaxicoursService();
        }

        [HttpGet]
        [Route("html")]
        public IActionResult GetHtml()
        {
            _maxicoursService.GetHtml();

            //var res = new HttpResponseMessage();
            //res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //res.Content = new ByteArrayContent(pdf);
            //return res;

            return Ok();
        }
    }
}
