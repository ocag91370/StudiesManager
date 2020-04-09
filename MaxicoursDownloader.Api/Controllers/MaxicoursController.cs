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
            var result = _maxicoursService.GetAllSchoolLevels();

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects")]
        public IActionResult GetAllSubjects(string levelTag)
        {
            var result = _maxicoursService.GetAllSubjects(levelTag);

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}")]
        public IActionResult GetSubject(string levelTag, int subjectId)
        {
            var result = _maxicoursService.GetSubject(levelTag, subjectId);

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/header")]
        public IActionResult GetHeader(string levelTag, int subjectId)
        {
            var result = _maxicoursService.GetHeader(levelTag, subjectId);

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes")]
        public IActionResult GetAllThemes(string levelTag, int subjectId)
        {
            var result = _maxicoursService.GetAllThemes(levelTag, subjectId);

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/categories")]
        public IActionResult GetAllCategories(string levelTag, int subjectId)
        {
            var subject = _maxicoursService.GetSubject(levelTag, subjectId);

            return Ok(subject.Categories);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/items")]
        public IActionResult GetAllItems(string levelTag, int subjectId)
        {
            var result = _maxicoursService.GetAllItems(levelTag, subjectId);

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons")]
        public IActionResult GetAllLessons(string levelTag, int subjectId)
        {
            var result = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, "fiche");

            return Ok(result);
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/{lessonId:int}/save")]
        public IActionResult SaveLesson(string levelTag, int subjectId, int lessonId)
        {
            _maxicoursService.SaveLesson(levelTag, subjectId, "fiche", lessonId);

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes/{themeId}/save")]
        public IActionResult SaveLessons(string levelTag, int subjectId, int themeId)
        {
            _maxicoursService.SaveLessonsOfTheme(levelTag, subjectId, "fiche", themeId);

            return Ok();
        }

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/paths")]
        //public IActionResult GetAllPaths(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/videos")]
        //public IActionResult GetAllVideosLessons(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/{lessonId}")]
        //public IActionResult GetLesson(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/summarysheets")]
        //public IActionResult GetAllSummarySheets(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/exercices")]
        //public IActionResult GetAllVideosExercices(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests")]
        //public IActionResult GetAllTests(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}


        [HttpGet]
        [Route("closed")]
        public IActionResult Closed()
        {
            _maxicoursService.SaveClosedPageAsPdf();

            return Ok();
        }
    }
}
