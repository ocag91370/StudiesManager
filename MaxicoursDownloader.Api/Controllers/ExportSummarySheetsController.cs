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
    public class ExportSummarySheetsController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportSummarySheetsController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/summarysheets/export")]
        public IActionResult ExportSchoolLevelSummarySheets(string levelTag)
        {
            try
            {
                var exportResult = _exportService.ExportSummarySheets(levelTag);

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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/summarysheets/export")]
        public IActionResult ExportSubjectSummarySheets(string levelTag, int subjectId)
        {
            try
            {
                var exportResult = _exportService.ExportSummarySheets(levelTag, subjectId);

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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes/{themeId}/summarysheets/export")]
        public IActionResult ExportSummarySheets(string levelTag, int subjectId, int themeId)
        {
            try
            {
                var exportResult = _exportService.ExportSummarySheets(levelTag, subjectId, themeId);

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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/summarysheets/{summarySheetId:int}")]
        public IActionResult ExportSummarySheet(string levelTag, int subjectId, int summarySheetId)
        {
            try
            {
                var exportResult = _exportService.ExportSummarySheet(levelTag, subjectId, summarySheetId);

                if (exportResult.NbFiles <= 0)
                    return NotFound();

                return Ok("Item successfully exported.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/summarysheets/export")]
        public IActionResult ExportSummarySheets(string levelTag, int subjectId, [FromBody]List<ItemKeyModel> itemKeyList)
        {
            try
            {
                var exportResult = _exportService.ExportSummarySheets(levelTag, subjectId, itemKeyList);

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
