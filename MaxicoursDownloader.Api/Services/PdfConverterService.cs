using IronPdf;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
using System.IO;

namespace MaxicoursDownloader.Api.Services
{
    public class PdfConverterService : IPdfConverterService
    {
        private readonly MaxicoursSettingsModel _maxicoursSettings;

        public PdfConverterService(IOptions<MaxicoursSettingsModel> configuration)
        {
            _maxicoursSettings = configuration.Value;

        }

        public void SaveAsPdf(LessonModel lesson)
        {
            var filename = GetFilename(lesson.Item);

            var renderer = new HtmlToPdf();

            SetPrintOptions(renderer);
            SetHeader(lesson.Item, renderer);
            SetFooter(lesson.Item, renderer);

            var pdf = renderer.RenderUrlAsPdf(lesson.PrintUrl);

            pdf.SaveAs(filename);
        }

        public void SaveAsPdf(VideoExerciseModel videoExercise)
        {
            var item = videoExercise.Item;
            var index = item.Index.ToString().PadLeft(3, '0');

            var filename = Path.Combine(_maxicoursSettings.ExportPath, $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}");

            try
            {
                var renderer = new HtmlToPdf();

                SetPrintOptions2(renderer);
                SetHeader(videoExercise, renderer);
                SetFooter(videoExercise, renderer);

                var pdf = renderer.RenderHtmlAsPdf(videoExercise.Subject);

                pdf.SaveAs($"{filename} - subject.pdf");
            }
            catch (System.Exception)
            {
            }

            try
            {
                var renderer = new HtmlToPdf();

                SetPrintOptions2(renderer);
                SetHeader(videoExercise, renderer);
                SetFooter(videoExercise, renderer);

                var pdf = renderer.RenderHtmlAsPdf(videoExercise.Solution);

                pdf.SaveAs($"{filename} - solution.pdf");
            }
            catch (System.Exception)
            {
            }
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
            @this.PrintOptions.MarginTop = 20;
            @this.PrintOptions.MarginBottom = 20;
            @this.PrintOptions.MarginLeft = 20;
            @this.PrintOptions.MarginRight = 20;
            @this.PrintOptions.FirstPageNumber = 1;
        }

        private void SetPrintOptions2(HtmlToPdf @this)
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
            @this.PrintOptions.MarginTop = 20;
            @this.PrintOptions.MarginBottom = 20;
            @this.PrintOptions.MarginLeft = 20;
            @this.PrintOptions.MarginRight = 20;
            @this.PrintOptions.FirstPageNumber = 1;
        }

        private void SetHeader(ItemModel item, HtmlToPdf @this)
        {
            var html = $"<div style='font-family: Verdana,Arial,Helvetica,sans-serif; font-weight: bold; font-size: 13pt; color: #712958; text-align: center;'>{item.Name}</div>";

            @this.PrintOptions.Header = new HtmlHeaderFooter()
            {
                Height = 15,
                FontFamily = "Comic Sans MS, cursive",
                HtmlFragment = html,
                DrawDividerLine = false,
            };
        }

        private void SetFooter(ItemModel item, HtmlToPdf @this)
        {
            var title = $"{item.SummarySubject.SchoolLevel.Name} - {item.SummarySubject.Name} - {item.Category.Name} - {item.Name}";

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

        private void SetHeader(VideoExerciseModel videoExercise, HtmlToPdf @this)
        {
            var html = $"<div style='font-family: Verdana,Arial,Helvetica,sans-serif; font-weight: bold; font-size: 13pt; color: #36c;'>{videoExercise.Title}</div>";

            @this.PrintOptions.Header = new HtmlHeaderFooter()
            {
                Height = 15,
                FontFamily = "Comic Sans MS, cursive",
                HtmlFragment = html,
                DrawDividerLine = false,
            };
        }

        private void SetFooter(VideoExerciseModel videoExercise, HtmlToPdf @this)
        {
            var item = videoExercise.Item;
            var title = $"{item.SummarySubject.SchoolLevel.Name} - {item.SummarySubject.Name} - {item.Category.Name} - {item.Name}";

            @this.PrintOptions.Footer = new HtmlHeaderFooter()
            {
                Height = 20,
                //HtmlFragment = "<div style='margin-left: 20px; margin-right: 20px; border-top: 1px solid lightgrey; text-align: right;'><span style='padding-top: 5px;'><i style='padding: 5px; background-color: lightgrey'>{page} / {total-pages}<i></span></div>",
                HtmlFragment = @$"
                    <div style='font-family: cursive; font-size: 12px; margin-bottom: 5px; padding-top: 5px; border-top: 1px solid darkgray;'>
                        <div style='display: inline'><i style='color: black;'>{title}</i>" + @"</div>
	                    <div style='display: inline; float: right;'>
    	                    <i style='padding: 5px; background-color: darkgray; color: white;'>{page} / {total-pages}</i>
                        </div>
                    </div>",
                DrawDividerLine = false,
            };
        }

        private void SetFooter(string title, HtmlToPdf @this)
        {
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

            var filename = $"{item.SummarySubject.SchoolLevel.Tag} - {item.SummarySubject.Tag} - {item.Category.Tag} - {index} - {item?.Theme?.Tag ?? item.SummarySubject.Tag} - {item.Id} - {item.Tag}.pdf";
            var result = Path.Combine(_maxicoursSettings.ExportPath, filename);

            return result;
        }
    }
}
