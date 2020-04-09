using AutoMapper;
using IronPdf;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StudiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Services
{
    public class MaxicoursService : IMaxicoursService, IDisposable
    {
        private readonly IMapper _mapper;
        private IWebDriver Driver;

        public MaxicoursService(IMapper mapper)
        {
            _mapper = mapper;

            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            var homePage = new MaxicoursHomePage(Driver);

            var result = homePage.GetAllSchoolLevels();

            return result;
        }

        public SchoolLevelModel GetSchoolLevel(string levelTag)
        {
            var schoolLevelList = GetAllSchoolLevels();

            var result = schoolLevelList.FirstOrDefault(o => o.Tag.IsSameAs(levelTag));

            return result;
        }

        public List<SubjectSummaryModel> GetAllSubjects(string levelTag)
        {
            var schoolLevel = GetSchoolLevel(levelTag);

            var schoolLevelPage = new SchoolLevelPage(Driver, schoolLevel.Url);
            var result = _mapper.Map<List<SubjectSummaryModel>>(schoolLevelPage.GetAllSubjects());

            return result;
        }

        public HeaderModel GetHeader(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<HeaderModel>(subjectPage.GetHeader());

            return result;
        }

        public SubjectModel GetSubject(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var subject = _mapper.Map<SubjectModel>(subjectPage.GetSubject());

            return subject;
        }

        public List<ThemeModel> GetAllThemes(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ThemeModel>>(subjectPage.GetAllThemes());

            return result;
        }

        public List<CategoryModel> GetAllCategories(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<CategoryModel>>(subjectPage.GetAllCategories());

            return result;
        }

        public List<ItemModel> GetAllItems(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ItemModel>>(subjectPage.GetAllItems());

            return result;
        }

        public List<ItemModel> GetItemsOfCategory(string levelTag, int subjectId, string categoryId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var result = _mapper.Map<List<ItemModel>>(subjectPage.GetItemsOfCategory(categoryId));

            return result;
        }

        public bool SaveLesson(string levelTag, int subjectId, string categoryId, int lessonId)
        {
            try
            {
                var schoolLevel = GetSchoolLevel(levelTag);
                var themeList = GetAllThemes(levelTag, subjectId);
                var categoryList = GetAllCategories(levelTag, subjectId);

                var itemList = GetItemsOfCategory(levelTag, subjectId, categoryId);
                var lesson = itemList.Where(o => o.ItemId == lessonId).FirstOrDefault();

                var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(lesson));
                var url = lessonPage.GetPrintUrl();
                SaveUrlAsPdf(url, $"{lessonId}.pdf");

                //var html = lessonPage.GetPrintFormat();
                //SaveHtmlAsPdf(html, $"{lessonId}.pdf");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SaveLessonsOfTheme(string levelTag, int subjectId, string categoryId, int themeId)
        {
            try
            {
                var schoolLevel = GetSchoolLevel(levelTag);
                var themeList = GetAllThemes(levelTag, subjectId);
                var categoryList = GetAllCategories(levelTag, subjectId);

                var itemList = GetItemsOfCategory(levelTag, subjectId, categoryId);
                var lessons = itemList.Where(o => o.ThemeId == themeId);

                foreach (var lesson in lessons)
                {
                    var lessonPage = new LessonPage(Driver, _mapper.Map<ItemEntity>(lesson));
                    var url = lessonPage.GetPrintUrl();
                    SaveUrlAsPdf(url, $"{levelTag}_{subjectId}_{categoryId}_{lesson.ThemeId}_{lesson.ItemId}.pdf");
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveUrlAsPdf(string url, string filename)
        {
            var Renderer = new IronPdf.HtmlToPdf();

            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.PrintHtmlBackgrounds = true;
            Renderer.PrintOptions.Title = "My PDF Document Name";
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 50;
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            Renderer.PrintOptions.DPI = 300;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.JpegQuality = 80;
            Renderer.PrintOptions.GrayScale = false;
            Renderer.PrintOptions.FitToPaperWidth = true;
            //Renderer.PrintOptions.InputEncoding = Encoding.UTF8;
            Renderer.PrintOptions.Zoom = 100;
            //Renderer.PrintOptions.CreatePdfFormsFromHtml = true;
            Renderer.PrintOptions.MarginTop = 10;
            Renderer.PrintOptions.MarginLeft = 10;
            Renderer.PrintOptions.MarginRight = 10;
            Renderer.PrintOptions.MarginBottom = 10;
            Renderer.PrintOptions.FirstPageNumber = 1;

            var pdf = Renderer.RenderUrlAsPdf(url);

            pdf.SaveAs(filename);
        }

        public void SaveHtmlAsPdf(string html, string filename)
        {
            var Renderer = new IronPdf.HtmlToPdf();

            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.PrintHtmlBackgrounds = true;
            Renderer.PrintOptions.Title = "My PDF Document Name";
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 50;
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            Renderer.PrintOptions.DPI = 300;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.JpegQuality = 80;
            Renderer.PrintOptions.GrayScale = false;
            Renderer.PrintOptions.FitToPaperWidth = true;
            //Renderer.PrintOptions.InputEncoding = Encoding.UTF8;
            Renderer.PrintOptions.Zoom = 100;
            //Renderer.PrintOptions.CreatePdfFormsFromHtml = true;
            Renderer.PrintOptions.MarginTop = 10;
            Renderer.PrintOptions.MarginLeft = 10;
            Renderer.PrintOptions.MarginRight = 10;
            Renderer.PrintOptions.MarginBottom = 10;
            Renderer.PrintOptions.FirstPageNumber = 1;

            var pdf = Renderer.RenderHtmlAsPdf(html);

            pdf.SaveAs(filename);
        }

        public void SaveClosedPageAsPdf()
        {
            var closedPage = new MaxicoursClosedPage(Driver);
            var html = closedPage.GetHtml();

            Driver.Url = "https://entraide-covid19.maxicours.com/W/cours/fiche/postit.php?oid=544567";
            var navigation = Driver.Navigate();
            html = Driver.PageSource;

            var Renderer = new IronPdf.HtmlToPdf();

            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            Renderer.PrintOptions.PrintHtmlBackgrounds = true;
            Renderer.PrintOptions.Title = "My PDF Document Name";
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 50;
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
            Renderer.PrintOptions.DPI = 300;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.JpegQuality = 80;
            Renderer.PrintOptions.GrayScale = false;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.InputEncoding = Encoding.UTF8;
            Renderer.PrintOptions.Zoom = 100;
            Renderer.PrintOptions.CreatePdfFormsFromHtml = true;
            Renderer.PrintOptions.MarginTop = 10;
            Renderer.PrintOptions.MarginLeft = 10;
            Renderer.PrintOptions.MarginRight = 10;
            Renderer.PrintOptions.MarginBottom = 10;
            Renderer.PrintOptions.FirstPageNumber = 1;

            var pdf = Renderer.RenderHtmlAsPdf(html);
            pdf.SaveAs("ClosedPage.pdf");
        }

        public void Dispose()
        {
            //Driver.FinishHim();
            Driver.Quit();
            Driver.Dispose();
            Driver = null;
        }
    }
}
