using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class ExportTestsController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportTestsController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/tests/export")]
        public IActionResult ExportSchoolLevelTests(string levelTag)
        {
            try
            {
                var exportResult = _exportService.ExportTests(levelTag);

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

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests/export")]
        //public IActionResult ExportSubjectTests(string levelTag, int subjectId)
        //{
        //    try
        //    {
        //        var exportResult = _exportService.ExportTests(levelTag, subjectId);

        //        if (exportResult.NbFiles <= 0)
        //            return NotFound();

        //        var result = new
        //        {
        //            Items = $"{exportResult.NbItems} item(s) identified.",
        //            Files = $"{exportResult.NbFiles} file(s) successfully exported."
        //        };

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes/{themeId}/tests/export")]
        //public IActionResult ExportThemeTests(string levelTag, int subjectId, int themeId)
        //{
        //    try
        //    {
        //        var exportResult = _exportService.ExportTests(levelTag, subjectId, themeId);

        //        if (exportResult.NbFiles <= 0)
        //            return NotFound();

        //        var result = new
        //        {
        //            Items = $"{exportResult.NbItems} item(s) identified.",
        //            Files = $"{exportResult.NbFiles} file(s) successfully exported."
        //        };

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests/{testId:int}")]
        public IActionResult ExportTest(string levelTag, int subjectId, int testId)
        {
            try
            {
                var exportResult = _exportService.ExportTest(levelTag, subjectId, testId);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                return Ok("Test successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests/export")]
        public IActionResult ExportTests(string levelTag, int subjectId, [FromBody]List<ItemKeyModel> itemKeyList)
        {
            try
            {
                var exportResult = _exportService.ExportTests(levelTag, subjectId, itemKeyList);

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
    }
}
