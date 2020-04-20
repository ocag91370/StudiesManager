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
        private readonly string _testsCategoryKey = "tests";

        public TestModel GetTest(string levelTag, int subjectId, int testId)
        {
            var item = GetItem(levelTag, subjectId, _maxicoursSettings.Categories[_testsCategoryKey], testId);
            Debug.Assert(item.IsNotNull());

            var result = GetTest(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public TestModel GetTest(ItemModel item)
        {
            var testPage = GetTestPage(_mapper.Map<ItemEntity>(item));
            Debug.Assert(testPage.IsNotNull());

            var test = testPage.GetTest();
            Debug.Assert(test.IsNotNull());

            var result = _mapper.Map<TestModel>(test);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetTests(string levelTag, int subjectId)
        {
            var result = GetItemsOfCategory(levelTag, subjectId, _maxicoursSettings.Categories[_testsCategoryKey]);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
