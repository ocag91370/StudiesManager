﻿using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxicoursDownloader.Api.Contracts
{
    public interface IMaxicoursService : IDisposable
    {
        #region School levels

        List<SchoolLevelModel> GetSchoolLevels();

        SchoolLevelModel GetSchoolLevel(string levelTag);

        #endregion

        #region Subjects

        List<SummarySubjectModel> GetSummarySubjects(string levelTag);

        SubjectModel GetSubject(string levelTag, int subjectId);

        List<ThemeModel> GetThemes(string levelTag, int subjectId);

        List<CategoryModel> GetCategories(string levelTag, int subjectId);

        List<ItemModel> GetItems(string levelTag, int subjectId);

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId);

        #endregion

        #region Lessons

        public List<ItemModel> GetLessons(string levelTag, int subjectId);

        LessonModel GetLesson(string levelTag, int subjectId, int lessonId);

        LessonModel GetLesson(ItemModel item);

        #endregion

        #region Summary sheets

        List<ItemModel> GetSummarySheets(string levelTag, int subjectId);

        SummarySheetModel GetSummarySheet(string levelTag, int subjectId, int summarySheetId);

        SummarySheetModel GetSummarySheet(ItemModel item);

        #endregion

        #region Tests

        TestModel GetTest(ItemModel item);

        TestModel GetTest(string levelTag, int subjectId, int testId);

        List<ItemModel> GetTests(string levelTag, int subjectId);

        #endregion

        #region Video lessons

        VideoLessonModel GetVideoLesson(ItemModel item);

        VideoLessonModel GetVideoLesson(string levelTag, int subjectId, int testId);

        List<ItemModel> GetVideoLessons(string levelTag, int subjectId);

        VideoLessonModel GetVideoLesson(string levelTag, int subjectId, ItemKeyModel itemKey);

        #endregion

        #region Video exercises

        VideoExerciseModel GetVideoExercise(ItemModel item);

        VideoExerciseModel GetVideoExercise(string levelTag, int subjectId, int itemId);

        List<ItemModel> GetVideoExercises(string levelTag, int subjectId);

        List<ItemModel> GetVideoExercises(SummarySubjectModel summarySubject);

        VideoExerciseModel GetVideoExercise(string levelTag, int subjectId, ItemKeyModel itemKey);

        #endregion
    }
}
