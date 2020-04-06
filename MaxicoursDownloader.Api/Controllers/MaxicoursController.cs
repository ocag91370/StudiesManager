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
        private readonly ILogger<CursusController> _logger;
        private readonly MaxicoursService _maxicoursService;

        public MaxicoursController(ILogger<CursusController> logger)
        {
            _logger = logger;
            _maxicoursService = new MaxicoursService();
        }

        [HttpGet]
        [Route("schoollevels")]
        public IActionResult GetAllSchoolLevels()
        {
            var schoolLevelList = _maxicoursService.GetAllSchoolLevels();

            return Ok(schoolLevelList);
        }

        [HttpGet]
        [Route("schoollevels/{levelName}")]
        public IActionResult GetSchoolLevel(string levelName)
        {
            var schoolLevelList = _maxicoursService.GetSchoolLevel(levelName);

            return Ok(schoolLevelList);
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects")]
        public IActionResult GetAllSubjects(string levelName)
        {
            var subjectList = _maxicoursService.GetAllSubjects(levelName);

            return Ok(subjectList);
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}")]
        public IActionResult GetSubject(string levelName, string subjectName)
        {
            var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok(subjectList);
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/themes")]
        public IActionResult GetAllThemes(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/paths")]
        public IActionResult GetAllPaths(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/lessons")]
        public IActionResult GetAllLessons(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/lessons/videos")]
        public IActionResult GetAllVideosLessons(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/summarysheets")]
        public IActionResult GetAllSummarySheets(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/exercices")]
        public IActionResult GetAllVideosExercices(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelName}/subjects/{subjectName}/tests")]
        public IActionResult GetAllTests(string levelName, string subjectName)
        {
            //var subjectList = _maxicoursService.GetSubject(levelName, subjectName);

            return Ok();
        }
    }
}
