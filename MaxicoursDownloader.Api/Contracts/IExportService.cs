﻿using MaxicoursDownloader.Api.Models;
using System;

namespace MaxicoursDownloader.Api.Interfaces
{
    public interface IExportService : IDisposable
    {
        ExportResultModel ExportLesson(string levelTag, int subjectId, int lessonId);

        ExportResultModel ExportLessons(string levelTag);

        ExportResultModel ExportLessons(string levelTag, int subjectId);

        ExportResultModel ExportLessons(string levelTag, int subjectId, int themeId);

        ExportResultModel ExportSummarySheet(string levelTag, int subjectId, int summarySheetId);

        ExportResultModel ExportSummarySheets(string levelTag);

        ExportResultModel ExportSummarySheets(string levelTag, int subjectId);
    }
}