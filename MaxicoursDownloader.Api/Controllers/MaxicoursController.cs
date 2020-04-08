using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class MaxicoursController : ControllerBase
    {
        private readonly ILogger<MaxicoursController> _logger;
        private readonly IMaxicoursService _maxicoursService;

        public MaxicoursController(IMaxicoursService maxicoursService, ILogger<MaxicoursController> logger)
        {
            _logger = logger;
            _maxicoursService = maxicoursService;
        }

        [HttpGet]
        [Route("schoollevels")]
        public IActionResult GetAllSchoolLevels()
        {
            var schoolLevelList = _maxicoursService.GetAllSchoolLevels();

            return Ok(schoolLevelList);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects")]
        public IActionResult GetAllSubjects(string levelTag)
        {
            var subjectList = _maxicoursService.GetAllSubjects(levelTag);

            return Ok(subjectList);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}")]
        public IActionResult GetSubject(string levelTag, int subjectId)
        {
            var subject = _maxicoursService.GetSubject(levelTag, subjectId);

            return Ok(subject);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/themes")]
        public IActionResult GetAllThemes(string levelTag, int subjectId)
        {
            var subject = _maxicoursService.GetSubject(levelTag, subjectId);

            return Ok(subject.Themes);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/categories")]
        public IActionResult GetAllCategories(string levelTag, int subjectId)
        {
            var subject = _maxicoursService.GetSubject(levelTag, subjectId);

            return Ok(subject.Categories);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/paths")]
        public IActionResult GetAllPaths(string levelTag, int subjectId)
        {
            //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/lessons")]
        public IActionResult GetAllLessons(string levelTag, int subjectId)
        {
            //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/lessons/videos")]
        public IActionResult GetAllVideosLessons(string levelTag, int subjectId)
        {
            //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/summarysheets")]
        public IActionResult GetAllSummarySheets(string levelTag, int subjectId)
        {
            //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/exercices")]
        public IActionResult GetAllVideosExercices(string levelTag, int subjectId)
        {
            //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId}/tests")]
        public IActionResult GetAllTests(string levelTag, int subjectId)
        {
            //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

            return Ok();
        }
    }
}
