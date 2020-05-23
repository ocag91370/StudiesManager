using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using StudiesManager.Common.Extensions;
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
    public class VideoExercisesController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;
        private readonly IMapper _mapper;

        public VideoExercisesController(IMaxicoursService maxicoursService, IMapper mapper)
        {
            _maxicoursService = maxicoursService;
            _mapper = mapper;
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
    }
}
