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
    public class VideoExercisesController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;
        private readonly IExportService _exportService;

        public VideoExercisesController(IMaxicoursService maxicoursService, IExportService exportService)
        {
            _maxicoursService = maxicoursService;
            _exportService = exportService;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/videoexercises")]
        public IActionResult GetVideoExercises(string levelTag)
        {
            try
            {
                var summarySubjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!summarySubjectList.Any())
                    return NotFound();

                var itemList = summarySubjectList.SelectMany(summarySubject => _maxicoursService.GetVideoExercises(summarySubject)).ToList();

                if (!itemList.Any())
                    return NotFound();

                var schoolLevel = summarySubjectList.First().SchoolLevel;
                var result = new
                {
                    SchoolLevel = schoolLevel,
                    Count = itemList.Count(),
                    VideoExercises = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.Index })
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/videoexercises")]
        public IActionResult GetVideoExercises(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetVideoExercises(levelTag, subjectId);

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
                    VideoExercises = new
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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/videoexercises/ids")]
        public IActionResult GetIdsOfVideoExercises(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetVideoExercises(levelTag, subjectId);

                if (!itemList.Any())
                    return NotFound();

                var firstItem = itemList?.FirstOrDefault();
                if (firstItem.IsNull())
                    return NotFound();

                var result = new
                {
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    VideoExercises = new
                    {
                        Count = itemList.Count(),
                        Ids = itemList.Select(o => new { o.Id, o.Index })
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
        [Route("schoollevels/{levelTag}/videoexercises/ids")]
        public IActionResult GetIdsOfVideoExercises(string levelTag)
        {
            try
            {
                var summarySubjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!summarySubjectList.Any())
                    return NotFound();

                var itemList = summarySubjectList.SelectMany(summarySubject => _maxicoursService.GetVideoExercises(summarySubject)).ToList();

                if (!itemList.Any())
                    return NotFound();

                var schoolLevel = summarySubjectList.First().SchoolLevel;

                var result = new
                {
                    SchoolLevel = new { schoolLevel.Id, schoolLevel.Name },
                    NbSubjects = summarySubjectList.Count(),
                    NbVideoExercises = itemList.Count(),
                    Subjects = itemList.GroupBy(
                        o => o.SummarySubject.Id,
                        o => o,
                        (subjectId, subjectItemList) =>
                        {
                            var subject = summarySubjectList.FirstOrDefault(o => o.Id == subjectId);
                            return new
                            {
                                Subject = new
                                {
                                    subject.Id,
                                    subject.Name,
                                    NbVideoExercises = subjectItemList.Count(),
                                    Ids = subjectItemList.Select(o => new { o.Id, o.Index })
                                }
                            };
                        })
                        .ToList()
                };


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
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
                    Tests = $"{exportResult.NbItems} video exercises(s) identified.",
                    Duplicates = $"{exportResult.NbDuplicates} video exercises(s) identified.",
                    Files = $"{exportResult.NbFiles} video exercises(s) successfully exported."
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
