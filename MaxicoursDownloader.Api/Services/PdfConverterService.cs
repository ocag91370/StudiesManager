using IronPdf;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using System.IO;

namespace MaxicoursDownloader.Api.Services
{
    public class PdfConverterService : IPdfConverterService
    {
        private readonly string _basePath = @"C:\Travail\Maxicours\Export\Temp";

        public void SaveAsPdf(LessonModel lesson)
        {
            var filename = GetFilename(lesson.Item);

            var renderer = new HtmlToPdf();

            SetPrintOptions(renderer);
            SetHeader(lesson, renderer);
            SetFooter(lesson, renderer);

            var pdf = renderer.RenderUrlAsPdf(lesson.PrintUrl);

            pdf.SaveAs(filename);
        }

        public void SaveUrlAsPdf(string url, string filename)
        {
            var renderer = new HtmlToPdf();
            SetPrintOptions(renderer);
            var pdf = renderer.RenderUrlAsPdf(url);

            pdf.SaveAs(filename);
        }

        public void SaveHtmlAsPdf(string html, string filename)
        {
            var renderer = new HtmlToPdf();
            SetPrintOptions(renderer);
            var pdf = renderer.RenderHtmlAsPdf(html);

            pdf.SaveAs(filename);
        }

        private void SetPrintOptions(HtmlToPdf @this)
        {
            @this.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            @this.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            @this.PrintOptions.PrintHtmlBackgrounds = true;
            @this.PrintOptions.Title = string.Empty;
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
            @this.PrintOptions.CreatePdfFormsFromHtml = true;
            @this.PrintOptions.MarginTop = 10;
            @this.PrintOptions.MarginBottom = 20;
            @this.PrintOptions.MarginLeft = 0;
            @this.PrintOptions.MarginRight = 0;
            @this.PrintOptions.FirstPageNumber = 1;
        }

        private void SetHeader(LessonModel lesson, HtmlToPdf @this)
        {
            //var title = $"{lesson.Item.SubjectSummary.SchoolLevel.Name} - {lesson.Item.SubjectSummary.Name} - {lesson.Item.Category.Name} - {lesson.Item.Name}";

            //@this.PrintOptions.Header = new HtmlHeaderFooter()
            //{
            //    Height = 15,
            //    FontFamily = "Comic Sans MS, cursive",
            //    //HtmlFragment = $"<div style='margin-left: 20px; margin-right: 20px; border-top: 1px solid lightgrey; border-bottom: 1px solid lightgrey'><center style='padding-top: 5px; padding-bottom: 5px'><i>{lesson.Item.SubjectSummary.SchoolLevel.Name} - {lesson.Item.SubjectSummary.Name} - {lesson.Item.Category.Name} - {lesson.Item.Name}<i></center></div>",
            //    HtmlFragment = @$"<div style='margin-left: 50px; margin-right: 50px; border-top: 1px solid lightgrey; border-bottom: 1px solid lightgrey; color: gray'>
	        //        <center style='padding-top: 5px; padding-bottom: 5px'>
            //            <i>{title}" + @"<i>
            //        </center>
            //        </div>",
            //    DrawDividerLine = false,
            //};
        }

        private void SetFooter(LessonModel lesson, HtmlToPdf @this)
        {
            var title = $"{lesson.Item.SubjectSummary.SchoolLevel.Name} - {lesson.Item.SubjectSummary.Name} - {lesson.Item.Category.Name} - {lesson.Item.Name}";

            @this.PrintOptions.Footer = new HtmlHeaderFooter()
            {
                Height = 20,
                //HtmlFragment = "<div style='margin-left: 20px; margin-right: 20px; border-top: 1px solid lightgrey; text-align: right;'><span style='padding-top: 5px;'><i style='padding: 5px; background-color: lightgrey'>{page} / {total-pages}<i></span></div>",
                HtmlFragment = @$"
                    <div style='font-family: cursive; font-size: 12px; margin-bottom: 5px; margin-left: 50px; margin-right: 50px; padding-top: 5px; border-top: 1px solid darkgray;'>
                        <div style='display: inline'><i style='color: black;'>{title}</i>" + @"</div>
	                    <div style='display: inline; float: right;'>
    	                    <i style='padding: 5px; background-color: darkgray; color: white;'>{page} / {total-pages}</i>
                        </div>
                    </div>",
                DrawDividerLine = false,
            };
        }

        private string GetFilename(ItemModel item)
        {
            var index = item.Index.ToString().PadLeft(3, '0');

            var filename = $"{item.SubjectSummary.SchoolLevel.Tag} - {item.SubjectSummary.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SubjectSummary.Tag} - {item.Id} - {item.Tag}.pdf";
            var result = Path.Combine(_basePath, filename);

            return result;
        }
    }
}
