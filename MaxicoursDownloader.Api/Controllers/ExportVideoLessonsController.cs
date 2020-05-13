﻿using MaxicoursDownloader.Api.Contracts;
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
    public class ExportVideoLessonsController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportVideoLessonsController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpPost]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/videolessons/export")]
        public IActionResult ExportVideoLessons(string levelTag, int subjectId, [FromBody]List<ItemKeyModel> itemKeyList)
        {
            try
            {
                var exportResult = _exportService.ExportVideoLessons(levelTag, subjectId, itemKeyList);

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