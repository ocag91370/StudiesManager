using IronPdf;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        private readonly string Url = @"http://r.mail.cours.fr/tr/cl/uQVn67dGsu3VLVV8kZSVL5BVRagoDxwMejRp-6QpPslJOGZZ7IsABnHusm2CLvDHSJrTYgRE7wAa812MJ-111mkNI_j0KHFqVKHxQnkExhiPSZQdFn78Ac_F6SuRXAgM-ZMF9qGlkOesETjANgizI7i0qiuUKrzN6kPUdy8Rx4QZUB-SyZed_ULyMxB0jMBBddfQVxQqE7Jc099Q5RrkPK7Ue9xIuAzF4fGYkDcDiloiG1Uopfe3zzuzoZwXm88AR--Xo1KLKN733ejt6MufqY8nMVSwQjCiLy1Ub_CP1D7VNCKwwtKluWiRkmb0NaU8VXl5yQ";

        private IWebDriver Driver;

        public MaxicoursService()
        {
            Driver = new ChromeDriver();
        }

        public byte[] GetHtml()
        {
            var homePage = new MaxicoursCovidHomePage(Driver, Url);

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

            Dispose();

            var pdf = Renderer.RenderHtmlAsPdf(html);
            pdf.SaveAs("example.pdf");

            return pdf.BinaryData;

            //return html;
        }

        public List<SchoolLevelModel> GetAllSchoolLevels()
        {
            var homePage = new MaxicoursHomePage(Driver, Url);

            var result = homePage.GetAllSchoolLevels();

            return result;
        }


        public SchoolLevelModel GetSchoolLevel(string levelName)
        {
            var schoolLevelList = GetAllSchoolLevels();

            var schoolLevel = schoolLevelList.FirstOrDefault(o => o.Name == levelName);

            return schoolLevel;
        }

        public List<SubjectModel> GetAllSubjects(string levelName)
        {
            var schoolLevel = GetSchoolLevel(levelName);

            var schoolLevelPage = new SchoolLevelPage(Driver, schoolLevel.Url);
            var subjectList = schoolLevelPage.GetAllSubjects();

            return subjectList;
        }

        public SubjectModel GetSubject(string levelName, string subjectName)
        {
            var subjectList = GetAllSubjects(levelName);

            var subject = subjectList.FirstOrDefault(o => o.Name == subjectName);

            return subject;
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
