using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

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
        [Route("schoollevels/{levelTag}")]
        public IActionResult ExportSchoolLevelLessons(string levelTag)
        {
            try
            {
                var count = _exportService.ExportLessons(levelTag, "fiche");

                if (count <= 0)
                    return NotFound();

                return Ok($"{count} lesson(s) successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}")]
        public IActionResult ExportSubjectLessons(string levelTag, int subjectId)
        {
            try
            {
                var count = _exportService.ExportLessons(levelTag, subjectId, "fiche");

                if (count <= 0)
                    return NotFound();

                return Ok($"{count} lesson(s) successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes/{themeId}")]
        public IActionResult ExportThemeLessons(string levelTag, int subjectId, int themeId)
        {
            try
            {
                var count = _exportService.ExportLessons(levelTag, subjectId, "fiche", themeId);

                if (count <= 0)
                    return NotFound();

                return Ok($"{count} lesson(s) successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/{lessonId:int}")]
        public IActionResult ExportLesson(string levelTag, int subjectId, int lessonId)
        {
            try
            {
                var count = _exportService.ExportLesson(levelTag, subjectId, "fiche", lessonId);

                if (count <= 0)
                    return NotFound();

                return Ok("Lesson successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
