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
        private readonly IWebDriver Driver;

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

            var schoolLevel = schoolLevelList.FirstOrDefault(o => o.Tag.IsSameAs(levelTag));

            return schoolLevel;
        }

        public List<SubjectSummaryModel> GetAllSubjects(string levelTag)
        {
            var schoolLevel = GetSchoolLevel(levelTag);

            var schoolLevelPage = new SchoolLevelPage(Driver, schoolLevel.Url);
            var subjectList = _mapper.Map<List<SubjectSummaryModel>>(schoolLevelPage.GetAllSubjects());

            return subjectList;
        }

        public SubjectModel GetSubject(string levelTag, int subjectId)
        {
            var subjectList = GetAllSubjects(levelTag);

            var subjectSummary = subjectList.FirstOrDefault(o => o.SubjectId == subjectId);
            var subjectPage = new SubjectPage(Driver, _mapper.Map<SubjectSummaryEntity>(subjectSummary));

            var subject = _mapper.Map<SubjectModel>(subjectPage.GetSubject());

            return subject;
        }

        public void SaveClosedPageAsPdf()
        {
            var closedPage = new MaxicoursClosedPage(Driver);
            var html = closedPage.GetHtml();

            var Renderer = new IronPdf.HtmlToPdf();

            Renderer.PrintOptions.SetCustomPaperSizeinMilimeters(210, 297);
            Renderer.PrintOptions.PrintHtmlBackgrounds = true;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
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
            Renderer.PrintOptions.MarginTop = 0;
            Renderer.PrintOptions.MarginLeft = 0;
            Renderer.PrintOptions.MarginRight = 0;
            Renderer.PrintOptions.MarginBottom = 0;
            Renderer.PrintOptions.FirstPageNumber = 1;

            var pdf = Renderer.RenderHtmlAsPdf(html);
            pdf.SaveAs("ClosedPage.pdf");
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
