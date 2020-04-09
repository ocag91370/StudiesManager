using IronPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Extensions
{
    public static class HtmlToPdfExtensions
    {
        public static void SetPdfPrintOptions(this HtmlToPdf @this)
        {
            @this.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            @this.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            @this.PrintOptions.EnableJavaScript = true;
            @this.PrintOptions.PrintHtmlBackgrounds = true;
            @this.PrintOptions.Title = "My PDF Document Name";
            @this.PrintOptions.EnableJavaScript = true;
            @this.PrintOptions.RenderDelay = 50;
            @this.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            @this.PrintOptions.DPI = 300;
            @this.PrintOptions.FitToPaperWidth = true;
            @this.PrintOptions.JpegQuality = 80;
            @this.PrintOptions.GrayScale = false;
            @this.PrintOptions.FitToPaperWidth = true;
            //@this.PrintOptions.InputEncoding = Encoding.UTF8;
            @this.PrintOptions.Zoom = 100;
            //@this.PrintOptions.CreatePdfFormsFromHtml = true;
            @this.PrintOptions.MarginTop = 10;
            @this.PrintOptions.MarginLeft = 10;
            @this.PrintOptions.MarginRight = 10;
            @this.PrintOptions.MarginBottom = 10;
            @this.PrintOptions.FirstPageNumber = 1;
        }
    }
}
