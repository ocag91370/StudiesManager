using IronPdf;
using MaxicoursDownloader.Api.Contracts;
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
        private readonly IWebDriver Driver;

        public MaxicoursService()
        {
            Driver = WebDriverFactory.CreateWebDriver(WebBrowserType.Chrome);
        }

        public void GetHtml()
        {
            var homePage = new MaxicoursCovidHomePage(Driver);

            var Renderer = new IronPdf.HtmlToPdf();

            Renderer.PrintOptions.SetCustomPaperSizeInInches(12.5, 20);
            Renderer.PrintOptions.PrintHtmlBackgrounds = true;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            Renderer.PrintOptions.Title = "My PDF Document Name";
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 50; //ms
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
            Renderer.PrintOptions.DPI = 300;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.JpegQuality = 80;
            Renderer.PrintOptions.GrayScale = false;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.InputEncoding = Encoding.UTF8;
            Renderer.PrintOptions.Zoom = 100;
            Renderer.PrintOptions.CreatePdfFormsFromHtml = true;
            Renderer.PrintOptions.MarginTop = 40;
            Renderer.PrintOptions.MarginLeft = 20;
            Renderer.PrintOptions.MarginRight = 20;
            Renderer.PrintOptions.MarginBottom = 40;
            Renderer.PrintOptions.FirstPageNumber = 1;

            var html = homePage.GetHtml();

            var pdf = Renderer.RenderHtmlAsPdf(html);
            pdf.SaveAs("example.pdf");

            //return html;
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

            var schoolLevel = schoolLevelList.FirstOrDefault(o => o.Tag.IsSameAs(levelTag));

            return schoolLevel;
        }

        public List<SubjectModel> GetAllSubjects(string levelTag)
        {
            var schoolLevel = GetSchoolLevel(levelTag);

            var schoolLevelPage = new SchoolLevelPage(Driver, schoolLevel.Url);
            var subjectList = schoolLevelPage.GetAllSubjects();

            return subjectList;
        }

        public SubjectModel GetSubject(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subject = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, subject.Url);

            subject.Themes = subjectPage.GetAllThemes().Select(o => new ThemeModel { ThemeId = o.ThemeId, Name = o.Name, Url = o.Url }).ToList();
            subject.Categories = subjectPage.GetAllCategories().Select(o => new CategoryModel { Id = o.Id, Name = o.Name }).ToList();

            return subject;
        }

        public List<ThemeModel> GetAllThemes(string levelTag, int subjectId)
        {
            var subject = GetSubject(levelTag, subjectId);

            var subjectPage = new SubjectPage(Driver, subject.Url);
            var themeList = subjectPage.GetAllThemes().Select(o => new ThemeModel { ThemeId = o.ThemeId, Name = o.Name, Url = o.Url }).ToList();

            return themeList;
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
