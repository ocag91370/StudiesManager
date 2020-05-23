using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using StudiesManager.Common;
using MaxicoursDownloader.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MaxicoursDownloader.Api.Controllers
{
    [ApiController]
    [Route("maxicours/audit")]
    public class AuditController : ControllerBase
    {
        private readonly IMaxicoursService _maxicoursService;

        public AuditController(IMaxicoursService maxicoursService)
        {
            _maxicoursService = maxicoursService;
        }

        [HttpGet]
        [Route("schoollevels/{levelTag}/detail")]
        public IActionResult GetSchoolLevelDetail(string levelTag)
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
                    })
                    .ToList();

                var groupBySubject = itemList.GroupBy(
                    o => o.SummarySubject.Name,
                    o => o,
                    (subjectName, subjectItemList) => new
                    {
                        SubjectName = subjectName,
                        NbItems = subjectItemList.Count(),
                        Categories = subjectItemList.GroupBy(
                            o => o.Category.Name,
                            o => o,
                            (categoryName, categoryItemList) => new
                            {
                                CategoryName = categoryName,
                                NbItems = categoryItemList.Count(),
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
                    Subjects = groupBySubject
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
