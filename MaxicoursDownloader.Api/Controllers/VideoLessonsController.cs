using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class VideoLessonsController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;

        public VideoLessonsController(IMaxicoursService maxicoursService)
        {
            _maxicoursService = maxicoursService;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/videolessons")]
        public IActionResult GetVideoLessons(string levelTag)
        {
            try
            {
                var summarySubjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!summarySubjectList.Any())
                    return NotFound();

                var itemList = summarySubjectList.SelectMany(summarySubject => _maxicoursService.GetVideoLessons(levelTag, summarySubject.Id)).ToList();

                if (!itemList.Any())
                    return NotFound();

                var schoolLevel = summarySubjectList.First().SchoolLevel;
                var result = new
                {
                    SchoolLevel = schoolLevel,
                    Count = itemList.Count(),
                    VideoLessons = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.Index })
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/videolessons")]
        public IActionResult GetVideoLessons(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetVideoLessons(levelTag, subjectId);

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
                    VideoLessons = new
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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/videolessons/ids")]
        public IActionResult GetIdsOfVideoLessons(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetVideoLessons(levelTag, subjectId);

                if (!itemList.Any())
                    return NotFound();

                var firstItem = itemList?.FirstOrDefault();
                if (firstItem.IsNull())
                    return NotFound();

                var result = new
                {
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    VideoLessons = new
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
    }
}
