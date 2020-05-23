using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using StudiesManager.Common.Extensions;
using MaxicoursDownloader.Api.Models.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class TestsController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;
        private readonly IMapper _mapper;

        public TestsController(IMaxicoursService maxicoursService, IMapper mapper)
        {
            _maxicoursService = maxicoursService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests")]
        public IActionResult GetTests(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetTests(levelTag, subjectId);

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
                    Test = new
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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests/{testId:int}")]
        public IActionResult GetTest(string levelTag, int subjectId, int testId)
        {
            try
            {
                var item = _maxicoursService.GetTests(levelTag, subjectId).FirstOrDefault(o => o.Id == testId);

                if (item.IsNull())
                    return NotFound();

                var result = new
                {
                    item.SummarySubject.SchoolLevel,
                    item.SummarySubject,
                    item.Theme,
                    item.Category,
                    Test = new { item.Id, item.Tag, item.Name, item.Url, item.Index }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/tests/ids")]
        public IActionResult GetIdsOfTests(string levelTag)
        {
            try
            {
                var summarySubjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!summarySubjectList.Any())
                    return NotFound();

                var itemList = summarySubjectList.SelectMany(summarySubject => _maxicoursService.GetTests(summarySubject)).ToList();

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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/tests/ids")]
        public IActionResult GetIdsOfTests(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetTests(levelTag, subjectId);

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
