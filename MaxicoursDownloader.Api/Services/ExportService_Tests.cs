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
        private ExportResultModel ExportTest(TestModel test)
        {
            try
            {
                var item = test.Item;
                var index = item.Index.ToString().PadLeft(3, '0');

                var workFilename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag} - sujet.pdf");
                using (WebClient client = new WebClient())
                {
                    byte[] arr = client.DownloadData(test.WorkUrl);

                    File.WriteAllBytes(workFilename, arr);

                }

                var correctionFilename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag} - correction.pdf");
                using (WebClient client = new WebClient())
                {
                    byte[] arr = client.DownloadData(test.CorrectionUrl);

                    File.WriteAllBytes(correctionFilename, arr);

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
                string categoryId = _maxicoursSettings.Categories["test"];

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
                string categoryId = _maxicoursSettings.Categories["test"];

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
    }
}