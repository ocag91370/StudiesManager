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
        public SubjectModel GetSubject(string levelTag, int subjectId)
        {
            var subjectPage = GetSubjectPage(levelTag, subjectId);
            Debug.Assert(subjectPage.IsNotNull());

            var subject = subjectPage.GetSubject();
            Debug.Assert(subject.IsNotNull());

            var result = _mapper.Map<SubjectModel>(subject);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<CategoryModel> GetCategories(string levelTag, int subjectId)
        {
            var subjectPage = GetSubjectPage(levelTag, subjectId);
            Debug.Assert(subjectPage.IsNotNull());

            var categoryList = subjectPage.GetAllCategories();
            Debug.Assert(categoryList.IsNotNull());

            var result = _mapper.Map<List<CategoryModel>>(categoryList);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public ItemModel GetItem(string levelTag, int subjectId, string categoryId, int itemId)
        {
            var subjectPage = GetSubjectPage(levelTag, subjectId);
            Debug.Assert(subjectPage.IsNotNull());

            var item = subjectPage.GetItemOfCategory(categoryId, itemId);
            Debug.Assert(item.IsNotNull());

            var result = _mapper.Map<ItemModel>(item);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetItems(string levelTag, int subjectId)
        {
            var subjectPage = GetSubjectPage(levelTag, subjectId);
            Debug.Assert(subjectPage.IsNotNull());

            var itemList = subjectPage.GetAllItems();
            Debug.Assert(itemList.IsNotNull());

            var result = _mapper.Map<List<ItemModel>>(itemList);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId)
        {
            var subjectPage = GetSubjectPage(levelTag, subjectId);
            Debug.Assert(subjectPage.IsNotNull());

            var itemList = subjectPage.GetItemsOfCategory(categoryId);
            Debug.Assert(itemList.IsNotNull());

            var result = _mapper.Map<List<ItemModel>>(itemList);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ItemModel> GetItemsOfCategory(SummarySubjectModel summarySubject, string categoryId)
        {
            var summarySubjectEntity = _mapper.Map<SummarySubjectEntity>(summarySubject);

            var subjectPage = GetSubjectPage(summarySubjectEntity);
            Debug.Assert(subjectPage.IsNotNull());

            var itemList = subjectPage.GetItemsOfCategory(categoryId);
            Debug.Assert(itemList.IsNotNull());

            var result = _mapper.Map<List<ItemModel>>(itemList);
            Debug.Assert(result.IsNotNull());

            return result;
        }

        public List<ThemeModel> GetThemes(string levelTag, int subjectId)
        {
            var subjectPage = GetSubjectPage(levelTag, subjectId);
            Debug.Assert(subjectPage.IsNotNull());

            var themeList = subjectPage.GetAllThemes();
            Debug.Assert(themeList.IsNotNull());

            var result = _mapper.Map<List<ThemeModel>>(themeList);
            Debug.Assert(result.IsNotNull());

            return result;
        }
    }
}
