using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using StudiesManager.Common;
using MaxicoursDownloader.Api.Models.Result;
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
        private readonly IMapper _mapper;

        public VideoLessonsController(IMaxicoursService maxicoursService, IMapper mapper)
        {
            _maxicoursService = maxicoursService;
            _mapper = mapper;
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
        [Route("schoollevels/{levelTag}/videolessons/ids")]
        public IActionResult GetIdsOfVideoLessons(string levelTag)
        {
            try
            {
                var summarySubjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!summarySubjectList.Any())
                    return NotFound();

                var itemList = summarySubjectList.SelectMany(summarySubject => _maxicoursService.GetVideoLessons(summarySubject)).ToList();

                if (!itemList.Any())
                    return NotFound();

                var result = new
                {
                    Count = _mapper.Map<SchoolLevelCountResultModel>(itemList),
                    Result = _mapper.Map<SchoolLevelKeyResultModel>(itemList)
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

                var result = new
                {
                    Count = _mapper.Map<SubjectCountResultModel>(itemList),
                    Result = _mapper.Map<SubjectKeyResultModel>(itemList)
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
