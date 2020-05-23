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
    public class LessonsController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;
        private readonly IMapper _mapper;

        public LessonsController(IMaxicoursService maxicoursService, IMapper mapper)
        {
            _maxicoursService = maxicoursService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons")]
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

        [HttpGet]
        [Route("schoollevels/{levelTag}/lessons/ids")]
        public IActionResult GetIdsOfLessons(string levelTag)
        {
            try
            {
                var summarySubjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!summarySubjectList.Any())
                    return NotFound();

                var itemList = summarySubjectList.SelectMany(summarySubject => _maxicoursService.GetLessons(summarySubject)).ToList();

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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/ids")]
        public IActionResult GetIdsOfLessons(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetLessons(levelTag, subjectId);

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
