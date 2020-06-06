using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Models.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class ExportLessonsController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportLessonsController(IExportService exportService)
        {
            _exportService = exportService;
        }

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
                    Items = $"{exportResult.NbItems} item(s) identified.",
                    Files = $"{exportResult.NbFiles} file(s) successfully exported."
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
                    Items = $"{exportResult.NbItems} item(s) identified.",
                    Files = $"{exportResult.NbFiles} file(s) successfully exported."
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
                    Items = $"{exportResult.NbItems} item(s) identified.",
                    Files = $"{exportResult.NbFiles} file(s) successfully exported."
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

        [HttpPost]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/export")]
        public IActionResult ExportLessons(string levelTag, int subjectId, [FromBody]List<ItemKeyModel> itemKeyList)
        {
            try
            {
                var exportResult = _exportService.ExportLessons(levelTag, subjectId, itemKeyList);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                var result = new
                {
                    Items = $"{exportResult.NbItems} item(s) identified.",
                    Files = $"{exportResult.NbFiles} file(s) successfully exported."
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("schoollevels/{levelTag}/lessons/export")]
        public IActionResult ExportLessons(string levelTag, [FromBody]SchoolLevelKeyModel schoolLevelModel)
        {
            try
            {
                var exportResultList = new List<ExportResultModel>();
                schoolLevelModel.Subjects.ForEach(subject =>
                {
                    exportResultList.Add(_exportService.ExportLessons(levelTag, subject.Id, subject.Items));
                });

                var nbItems = exportResultList.Sum(o => o.NbItems);
                var nbFiles = exportResultList.Sum(o => o.NbFiles);

                if (nbFiles <= 0)
                    return NotFound();

                var result = new
                {
                    Items = $"{nbItems} item(s) identified.",
                    Files = $"{nbFiles} file(s) successfully exported."
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
