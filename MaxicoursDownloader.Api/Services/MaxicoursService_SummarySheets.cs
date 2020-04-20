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
        private readonly string _summarySheetsCategoryKey = "summary_sheets";

        public SummarySheetModel GetSummarySheet(string levelTag, int subjectId, int summarySheetId)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_summarySheetsCategoryKey], summarySheetId);
            Debug.Assert(item.IsNotNull());

            var summarySheetPage = GetSummarySheetPage(_mapper.Map<ItemEntity>(item));
            Debug.Assert(summarySheetPage.IsNotNull());

            var summarySheet = summarySheetPage.GetSummarySheet();
            Debug.Assert(summarySheet.IsNotNull());

            var result = _mapper.Map<SummarySheetModel>(summarySheet);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public SummarySheetModel GetSummarySheet(ItemModel item)
        {
            var summarySheetPage = GetSummarySheetPage(_mapper.Map<ItemEntity>(item));
            Debug.Assert(summarySheetPage.IsNotNull());

            var result = _mapper.Map<SummarySheetModel>(summarySheetPage.GetSummarySheet());
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetSummarySheets(string levelTag, int subjectId)
        {
            var result = GetItemsOfCategory(levelTag, subjectId, _maxicoursSettings.Categories[_summarySheetsCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
