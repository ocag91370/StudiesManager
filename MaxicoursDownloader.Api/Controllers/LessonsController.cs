using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class LessonsController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;
        private readonly IExportService _exportService;

        public LessonsController(IMaxicoursService maxicoursService, IExportService exportService)
        {
            _maxicoursService = maxicoursService;
        }

        #region Export

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/ids")]
        public IActionResult GetIdsOfLessons(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetLessons(levelTag, subjectId);

                if (!itemList.Any())
                    return NotFound();

                var firstItem = itemList?.FirstOrDefault();
                if (firstItem.IsNull())
                    return NotFound();

                var result = new
                {
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    Lessons = new
                    {
                        Count = itemList.Count(),
                        Ids = itemList.Select(o => o.Id)
                    }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}")]
        public IActionResult GetLessons(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetLessons(levelTag, subjectId);

                if (!itemList.Any())
                    return NotFound();

                var firstItem = itemList?.FirstOrDefault();
                if (firstItem.IsNull())
                    return NotFound();

                var result = new
                {
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    firstItem.Theme,
                    firstItem.Category,
                    Lesson = new
                    {
                        Count = itemList.Count(),
                        Data = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.Index })
                    }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/{lessonId:int}")]
        public IActionResult GetLesson(string levelTag, int subjectId, int lessonId)
        {
            try
            {
                var item = _maxicoursService.GetLessons(levelTag, subjectId).FirstOrDefault(o => o.Id == lessonId);

                if (item.IsNull())
                    return NotFound();

                var result = new
                {
                    item.SummarySubject.SchoolLevel,
                    item.SummarySubject,
                    item.Theme,
                    item.Category,
                    Lesson = new { item.Id, item.Tag, item.Name, item.Url, item.Index }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion

        #region Export

        [HttpGet]
        [Route("schoollevels/{levelTag}/lessons/export")]
        public IActionResult ExportSchoolLevelLessons(string levelTag)
        {
            try
            {
                var exportResult = _exportService.ExportLessons(levelTag);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                var result = new
                {
                    Lessons = $"{exportResult.NbItems} lesson(s) identified.",
                    Duplicates = $"{exportResult.NbDuplicates} lesson(s) identified.",
                    Files = $"{exportResult.NbFiles} lesson(s) successfully exported."
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/export")]
        public IActionResult ExportSubjectLessons(string levelTag, int subjectId)
        {
            try
            {
                var exportResult = _exportService.ExportLessons(levelTag, subjectId);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                var result = new
                {
                    Lessons = $"{exportResult.NbItems} lesson(s) identified.",
                    Duplicates = $"{exportResult.NbDuplicates} lesson(s) identified.",
                    Files = $"{exportResult.NbFiles} lesson(s) successfully exported."
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes/{themeId}/lessons/export")]
        public IActionResult ExportThemeLessons(string levelTag, int subjectId, int themeId)
        {
            try
            {
                var exportResult = _exportService.ExportLessons(levelTag, subjectId, themeId);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                var result = new
                {
                    Lessons = $"{exportResult.NbItems} lesson(s) identified.",
                    Duplicates = $"{exportResult.NbDuplicates} lesson(s) identified.",
                    Files = $"{exportResult.NbFiles} lesson(s) successfully exported."
                };

                return Ok(result);
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
                var exportResult = _exportService.ExportLesson(levelTag, subjectId, lessonId);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                return Ok("Lesson successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
