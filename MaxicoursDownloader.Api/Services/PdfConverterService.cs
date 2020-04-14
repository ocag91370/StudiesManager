using IronPdf;
using MaxicoursDownloader.Api.Extensions;
using MaxicoursDownloader.Api.Interfaces;

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
            var renderer = new HtmlToPdf();
            renderer.SetPdfPrintOptions();
            var pdf = renderer.RenderHtmlAsPdf(html);

            pdf.SaveAs(filename);
        }
    }
}
