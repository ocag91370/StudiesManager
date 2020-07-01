using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Globalization;
using StudiesManager.Services.Models;
using System.IO;
using System.Threading;
using IronPdf;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Net.Mime;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomeworkPanelPom : BasePom
    {
        public DateTime CurrentDate { get; set; }

        private List<SubjectPanelPom> GetSubjects() => GetSubjectElements().Select(o => new SubjectPanelPom(Driver, o)).ToList();

        public string GetWorkToDo(WebDriverSettingsModel webDriverSettings)
        {
            var body = GetWorkToDoPanel().GetWork("Travail à faire").Html;

            ExtractContent(body, webDriverSettings);
            ExtractFiles(webDriverSettings);
            MoveFilesToDirectory(webDriverSettings, "TravailaFaire");

            return body;
        }

        public string GetSessionsContent(WebDriverSettingsModel webDriverSettings)
        {
            var body = GetWorkSessionPanel().GetWork("Contenus de séances").Html;

            ExtractContent(body, webDriverSettings);
            ExtractFiles(webDriverSettings);
            MoveFilesToDirectory(webDriverSettings, "ContenusDeSeances");

            return body;
        }

        private string ExtractContent(string body, WebDriverSettingsModel webDriverSettings)
        {
            string head = string.Join("", Driver.FindElements(By.XPath("//*[@rel='stylesheet']")).AsEnumerable().Select(o => o.GetOuterHtml()));
            var html = @$"<html><head>{head}</head><body>{body}</body></html>";

            var filename = Path.Combine(webDriverSettings.DownloadDefaultDirectory, "Synthese.pdf");
            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(html);
            pdf.SaveAs(filename);

            return html;
        }

        private void ExtractFiles(WebDriverSettingsModel webDriverSettings)
        {
            DownloadFiles();
            CheckThatFilesAreDownloaded(webDriverSettings.DownloadDefaultDirectory);
        }

        public void DownloadFiles()
        {
            var fileElements = GetFileElements();
            if (!fileElements.Any())
                return;

            fileElements.ForEach(o =>
            {
                o.Click();
                Thread.Sleep(2000);
            });
        }

        private void CheckThatFilesAreDownloaded(string path)
        {
            var fileElements = GetFileElements();
            if (!fileElements.Any())
                return;

            var filenames = fileElements.Select(o => o.Text);

            var nbRetry = 100;
            while ((nbRetry > 0) && Directory.EnumerateFiles(path, "*.crdownload").Any())
            {
                nbRetry--;
                Thread.Sleep(5000);
            }
        }

        public void SendMail()
        {
            var mail = new MailMessage("olive91370@gmail.com", "ocagliuli@hotmail.com");
            //Now sending a mail with attachment ICS file.                     
            mail.Subject = "Test Mail";
            mail.Body = "This is for testing SMTP mail from GMAIL";
            var smtpclient = new SmtpClient();
            smtpclient.Host = "smtp.gmail.com"; //-------this has to given the Mailserver IP
            smtpclient.Port = 587;
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = new NetworkCredential("olive91370@gmail.com", "ocag_2312");
            smtpclient.EnableSsl = true;
            smtpclient.Send(mail);
        }

        private void MoveFilesToDirectory(WebDriverSettingsModel webDriverSettings, string tag)
        {
            var prefix = CurrentDate.ToLocalTime().ToString("yyyy-MM-dd");

            var newPath = Path.Combine(webDriverSettings.DownloadDefaultDirectory, prefix);
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            var files = Directory.GetFiles(webDriverSettings.DownloadDefaultDirectory);
            foreach (var file in files)
            {
                if (!File.Exists(file))
                    continue;

                var newFilename = Path.Combine(newPath, $"{prefix}_{tag}_{Path.GetFileName(file)}");

                File.Move(file, newFilename, true);
            }
        }
    }

    public partial class HomeworkPanelPom
    {
        private readonly CultureInfo _ci = new CultureInfo("fr-FR");

        public HomeworkPanelPom(IWebDriver driver, DateTime date) : base(driver)
        {
            CurrentDate = date;
        }

        private IWebElement GetCurrentElement() => Driver.FindElement(By.Id("tab-devoirs-jour"), 1, 5);

        private IWebElement GetWorkToDoElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'afaire')]"));

        private IWebElement GetWorkSessionElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'crseance')]"));

        //private IWebElement GetSessionsContentElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'crseance')]"));

        private IWebElement GetDataElement() => Driver.FindElement(By.XPath($"//*[(@class = 'encartJour') and (@id != '')]"), 1, 5);

        private List<IWebElement> GetFileElements() => GetDataElement().FindElements(By.XPath($"//a[@file-type = 'FICHIER_CDT']")).ToList();

        private List<IWebElement> GetSubjectElements() => GetDataElement().FindElements(By.XPath($"//*[@class = 'ed-card width-epingle']")).ToList();

        private WorkPanelPom GetWorkToDoPanel()
        {
            GetWorkToDoElement().Click();

            return new WorkPanelPom(Driver, GetDataElement());
        }

        private WorkPanelPom GetWorkSessionPanel()
        {
            GetWorkSessionElement().Click();

            return new WorkPanelPom(Driver, GetDataElement());
        }
    }
}
