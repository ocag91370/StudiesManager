using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours/export")]
    public class ExportController : ControllerBase
    {
        private readonly ILogger<ExportController> _logger;
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService, ILogger<ExportController> logger)
        {
            _logger = logger;
            _exportService = exportService;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}")]
        public IActionResult ExportLessons(string levelTag, int subjectId)
        {
            var isOk = _exportService.ExportLessons(levelTag, subjectId, "fiche");

            if (!isOk)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes/{themeId}")]
        public IActionResult ExportThemeLessons(string levelTag, int subjectId, int themeId)
        {
            var isOk = _exportService.ExportThemeLessons(levelTag, subjectId, "fiche", themeId);

            if (!isOk)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/{lessonId:int}")]
        public IActionResult ExportLesson(string levelTag, int subjectId, int lessonId)
        {
            var isOk = _exportService.ExportLesson(levelTag, subjectId, "fiche", lessonId);

            if (!isOk)
                return NotFound();

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
    }
}
