using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using StudiesManager.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public partial class MaxicoursService : IMaxicoursService
    {
        private SummarySubjectModel GetSummarySubject(string levelTag, int subjectId)
        {
            var summarySubjectList = GetSummarySubjects(levelTag);
            Debug.Assert(summarySubjectList.IsNotNull());

            var summarySubject = summarySubjectList.FirstOrDefault(o => o.Id == subjectId);
            Debug.Assert(summarySubject.IsNotNull());

            var result = _mapper.Map<SummarySubjectModel>(summarySubject);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<SummarySubjectModel> GetSummarySubjects(string levelTag)
        {
            var page = GetSchoolLevelPage(levelTag);
            Debug.Assert(page.IsNotNull());

            var subjectSummaryList = page.GetAllSummarySubjects();
            Debug.Assert(subjectSummaryList.IsNotNull());

            var result = _mapper.Map<List<SummarySubjectModel>>(subjectSummaryList);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
