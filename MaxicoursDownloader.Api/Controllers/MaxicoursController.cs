﻿using MaxicoursDownloader.Api.Contracts;
using StudiesManager.Common.Extensions;
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
    public class MaxicoursController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;

        public MaxicoursController(IMaxicoursService maxicoursService)
        {
            _maxicoursService = maxicoursService;
        }

        [HttpGet]
        [Route("schoollevels")]
        public IActionResult GetSchoolLevels()
        {
            try
            {
                var schoolLevelList = _maxicoursService.GetSchoolLevels();

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
        public IActionResult GetSubjects(string levelTag)
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
                    subject.SummarySubject.SchoolLevel,
                    subject.SummarySubject,
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
                            Data = subject.Items.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index })
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
        [Route("schoollevels/{levelTag}/items")]
        public IActionResult GetSchoolLevelLessons(string levelTag)
        {
            try
            {
                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                if (subjectList.IsNull())
                    return NotFound();

                if (!subjectList.Any())
                    return NotFound();

                var itemList = new List<ItemModel>();
                subjectList.ForEach(subject => itemList.AddRange(_maxicoursService.GetItems(levelTag, subject.Id)));

                var groupByCategory = itemList.GroupBy(
                    o => o.Category.Name,
                    o => o,
                    (categoryName, categoryItemList) => new
                    {
                        CategoryName = categoryName,
                        NbItems = categoryItemList.Count(),
                        Items = categoryItemList.Select(o => new { o.Id, o.Name, o.Url, SubjectId = o.SummarySubject.Id, SubjectName = o.SummarySubject.Name })
                    })
                    .ToList();

                var groupBySubject = itemList.GroupBy(
                    o => o.SummarySubject.Name,
                    o => o,
                    (subjectName, subjectItemList) => new
                    {
                        SubjectName = subjectName,
                        NbItems = subjectItemList.Count(),
                        Items = subjectItemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index }),
                        Categories = subjectItemList.GroupBy(
                            o => o.Category.Name,
                            o => o,
                            (categoryName, categoryItemList) => new
                            {
                                CategoryName = categoryName,
                                NbItems = categoryItemList.Count(),
                                Items = categoryItemList.Select(o => new { o.Id, o.Name, o.Url })
                            })
                            .ToList()
                    })
                    .ToList();

                var schoolLevel = subjectList?.FirstOrDefault()?.SchoolLevel;
                if (schoolLevel.IsNull())
                    return NotFound();

                var result = new
                {
                    School = schoolLevel.Name,
                    NbItems = itemList.Count(),
                    Categories = groupByCategory,
                    Subjects = groupBySubject,
                    Items = itemList
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
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    firstItem.Theme,
                    firstItem.Category,
                    Items = new
                    {
                        Count = itemList.Count(),
                        Data = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index })
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
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    firstItem.Theme,
                    firstItem.Category,
                    SummarySheets = new
                    {
                        Count = itemList.Count(),
                        Data = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index })
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
                    firstItem.SummarySubject.SchoolLevel,
                    firstItem.SummarySubject,
                    firstItem.Theme,
                    firstItem.Category,
                    Tests = new
                    {
                        Count = itemList.Count(),
                        Data = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index })
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
                    VideoExercises = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index })
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
                        Data = itemList.Select(o => new { o.Id, o.Tag, o.Name, o.Url, o.HasSwf, o.Index })
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
