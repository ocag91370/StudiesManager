﻿using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Services
{
    public partial class ExportService : IExportService
    {
        private readonly string _summarySheetsCategoryKey = "summary_sheets";

        private ExportResultModel ExportSummarySheet(SummarySheetModel summarySheet)
        {
            try
            {
                var item = summarySheet.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var filename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}");
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(summarySheet.PrintUrl), $"{filename}.pdf");
                    client.DownloadFile(new Uri(summarySheet.AudioUrl), $"{filename}.mp3");
                }

                return new ExportResultModel(1, 0, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportSummarySheet(string levelTag, int subjectId, int summarySheetId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_summarySheetsCategoryKey];

                var summarySheet = _maxicoursService.GetSummarySheet(levelTag, subjectId, summarySheetId);

                return ExportSummarySheet(summarySheet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportSummarySheets(List<ItemModel> itemList)
        {
            try
            {
                var summarySheetList = itemList.Select(item => _maxicoursService.GetSummarySheet(item)).ToList();
                var fileList = summarySheetList.Select(summarySheet => ExportSummarySheet(summarySheet)).ToList();

                return new ExportResultModel(fileList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportSummarySheets(string levelTag)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_summarySheetsCategoryKey];

                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportSummarySheets(itemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportSummarySheets(string levelTag, int subjectId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_summarySheetsCategoryKey];

                var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subjectId, categoryId);
                var groupByThemeIdItemList = itemList.GroupBy(
                    o => o.Theme?.Id,
                    o => o,
                    (themeId, itemList) => new { ThemeId = themeId, ItemList = itemList.ToList() }
                    ).ToList();

                var resultList = new List<ExportResultModel>();
                groupByThemeIdItemList.ForEach((group) =>
                {
                    resultList.Add(ExportSummarySheets(group.ItemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}