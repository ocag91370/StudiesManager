using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours")]
    public class MaxicoursController : ControllerBase
    {
        private readonly ILogger<MaxicoursController> _logger;
        private readonly IMaxicoursService _maxicoursService;

        public MaxicoursController(IMaxicoursService maxicoursService, ILogger<MaxicoursController> logger)
        {
            _logger = logger;
            _maxicoursService = maxicoursService;
        }

        [HttpGet]
        [Route("schoollevels")]
        public IActionResult GetAllSchoolLevels()
        {
            try
            {
                var schoolLevelList = _maxicoursService.GetAllSchoolLevels();

                if (!schoolLevelList.Any())
                    return NotFound();

                var result = new
                {
                    Count = schoolLevelList.Count(),
                    Data = schoolLevelList
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects")]
        public IActionResult GetAllSubjects(string levelTag)
        {
            try
            {
                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (!subjectList.Any())
                    return NotFound();

                var result = new
                {
                    SchoolLevel = subjectList.First(),
                    Subjects = new
                    {
                        Count = subjectList.Count(),
                        Data = subjectList.Select(o => new { o.Id, o.Tag, o.Name, o.Url })
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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}")]
        public IActionResult GetSubject(string levelTag, int subjectId)
        {
            try
            {
                var subject = _maxicoursService.GetSubject(levelTag, subjectId);

                if (subject.IsNull())
                    return NotFound();

                var result = new
                {
                    subject.SubjectSummary.SchoolLevel,
                    subject.SubjectSummary,
                    Subject = new
                    {
                        Themes = new
                        {
                            Count = subject.Themes.Count(),
                            Data = subject.Themes.Select(o => new { o.Id, o.Tag, o.Name, o.Url })
                        },
                        Categories = new
                        {
                            Count = subject.Categories.Count(),
                            Data = subject.Categories.Select(o => new { o.Id, o.Tag, o.Name })
                        },
                        Items = new
                        {
                            Count = subject.Items.Count(),
                            Data = subject.Items.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.Index })
                        }
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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/themes")]
        public IActionResult GetThemes(string levelTag, int subjectId)
        {
            try
            {
                var themeList = _maxicoursService.GetThemes(levelTag, subjectId);

                if (!themeList.Any())
                    return NotFound();

                var result = new
                {
                    Count = themeList.Count(),
                    Themes = themeList.Select(o => new { o.Id, o.Tag, o.Name, o.Url })
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/categories")]
        public IActionResult GetCategories(string levelTag, int subjectId)
        {
            try
            {
                var categoryList = _maxicoursService.GetCategories(levelTag, subjectId);

                if (!categoryList.Any())
                    return NotFound();

                var result = new
                {
                    Count = categoryList.Count(),
                    Themes = categoryList.Select(o => new { o.Id, o.Tag, o.Name })
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/items")]
        public IActionResult GetItems(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetItems(levelTag, subjectId);

                if (!itemList.Any())
                    return NotFound();

                var firstItem = itemList?.FirstOrDefault();
                if (firstItem.IsNull())
                    return NotFound();

                var result = new
                {
                    firstItem.SubjectSummary.SchoolLevel,
                    firstItem.SubjectSummary,
                    firstItem.Theme,
                    firstItem.Category,
                    Items = new
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
                    firstItem.SubjectSummary.SchoolLevel,
                    firstItem.SubjectSummary,
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
                    item.SubjectSummary.SchoolLevel,
                    item.SubjectSummary,
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
        [Route("schoollevels/{levelTag}/subjects/{subjectId:int}/summarysheets")]
        public IActionResult GetSummarySheets(string levelTag, int subjectId)
        {
            try
            {
                var itemList = _maxicoursService.GetSummarySheets(levelTag, subjectId);

                if (!itemList.Any())
                    return NotFound();

                var firstItem = itemList?.FirstOrDefault();
                if (firstItem.IsNull())
                    return NotFound();

                var result = new
                {
                    firstItem.SubjectSummary.SchoolLevel,
                    firstItem.SubjectSummary,
                    firstItem.Theme,
                    firstItem.Category,
                    SummarySheets = new
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
                    firstItem.SubjectSummary.SchoolLevel,
                    firstItem.SubjectSummary,
                    firstItem.Theme,
                    firstItem.Category,
                    Tests = new
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

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/paths")]
        //public IActionResult GetAllPaths(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/lessons/videos")]
        //public IActionResult GetAllVideosLessons(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("schoollevels/{levelTag}/subjects/{subjectId:int}/exercices")]
        //public IActionResult GetAllVideosExercices(string levelTag, int subjectId)
        //{
        //    //var subjectList = _maxicoursService.GetSubject(levelTag, subjectName);

        //    return Ok();
        //}
    }
}
