using AutoMapper;
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
        private readonly string _testsCategoryKey = "tests";

        private ExportResultModel ExportTest(TestModel test)
        {
            try
            {
                var item = test.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var filename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}");
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(test.WorkUrl), $"{filename} - sujet.pdf");
                    client.DownloadFile(new Uri(test.CorrectionUrl), $"{filename} - correction.pdf");
                }

                return new ExportResultModel(1, 0, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportTest(string levelTag, int subjectId, int testId)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_testsCategoryKey];

                var test = _maxicoursService.GetTest(levelTag, subjectId, testId);

                return ExportTest(test);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportTests(string levelTag)
        {
            try
            {
                string categoryId = _maxicoursSettings.Categories[_testsCategoryKey];

                var subjectList = _maxicoursService.GetSummarySubjects(levelTag);

                var resultList = new List<ExportResultModel>();
                subjectList.ForEach((subject) =>
                {
                    var itemList = _maxicoursService.GetItemsOfCategory(levelTag, subject.Id, categoryId);
                    resultList.Add(ExportTests(itemList));
                });

                return new ExportResultModel(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ExportResultModel ExportTests(List<ItemModel> itemList)
        {
            try
            {
                var testList = itemList.Select(item => _maxicoursService.GetTest(item)).ToList();
                var fileList = testList.Select(test => ExportTest(test)).ToList();

                return new ExportResultModel(fileList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportResultModel ExportTests(string levelTag, int subjectId, List<ItemKeyModel> itemKeyList)
        {
            try
            {
                var resultList = new List<ExportResultModel>();
                itemKeyList.ForEach((itemKey) =>
                {
                    var item = _maxicoursService.GetTest(levelTag, subjectId, itemKey);
                    resultList.Add(ExportTest(item));
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