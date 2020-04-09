using AutoMapper;
using IronPdf;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
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
    public class PdfConverterService : IPdfConverterService
    {

        public void SaveUrlAsPdf(string url, string filename)
        {
            var renderer = new HtmlToPdf();
            renderer.SetPdfPrintOptions();
            var pdf = renderer.RenderUrlAsPdf(url);

            pdf.SaveAs(filename);
        }

        public void SaveHtmlAsPdf(string html, string filename)
        {
            var renderer = new IronPdf.HtmlToPdf();
            renderer.SetPdfPrintOptions();
            var pdf = renderer.RenderHtmlAsPdf(html);

            pdf.SaveAs(filename);
        }
    }
}
