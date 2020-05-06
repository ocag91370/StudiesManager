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
    public class ExportVideoExercisesController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportVideoExercisesController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpPost]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/videoexercises/export")]
        public IActionResult ExportVideoExercises(string levelTag, int subjectId, [FromBody]List<ItemKeyModel> itemKeyList)
        {
            try
            {
                var exportResult = _exportService.ExportVideoExercises(levelTag, subjectId, itemKeyList);

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
