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
        private List<SubjectPanelPom> GetSubjects() => GetSubjectElements().Select(o => new SubjectPanelPom(Driver, o)).ToList();

        public string GetWorkToDo(WebDriverSettingsModel webDriverSettings)
        {
            GetWorkToDoElement().Click();

            ExtractFiles(webDriverSettings);
            ExtractContent(webDriverSettings);

            return GetDataElement().Text;
        }

        public string GetSessionsContent(WebDriverSettingsModel webDriverSettings)
        {
            GetSessionsContentElement().Click();

            ExtractFiles(webDriverSettings);
            var content = ExtractContent(webDriverSettings);

            return GetDataElement().Text;
        }

        public string ExtractContent(WebDriverSettingsModel webDriverSettings)
        {
            string head = string.Join("", Driver.FindElements(By.XPath("//*[@rel='stylesheet']")).AsEnumerable().Select(o => o.GetOuterHtml()));
            string body = GetDataElement().GetOuterHtml();
            var html = @$"<html><head>{head}</head><body>{body}</body></html>";

            var filename = Path.Combine(webDriverSettings.DownloadDefaultDirectory, "test.pdf");
            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(html);
            pdf.SaveAs(filename);

            return html;
        }

        public void ExtractFiles(WebDriverSettingsModel webDriverSettings)
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

            var nbRetry = 5;
            while ((nbRetry > 0) && Directory.EnumerateFiles(path, "*.crdownload").Any())
            {
                nbRetry--;
                Thread.Sleep(2000);
            }
        }

        public void SendMail()
        {
            var mail = new MailMessage("olive91370@gmail.com", "ocagliuli@hotmail.com");
/*
            var str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Schedule a Meeting");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", DateTime.Now.AddMinutes(+330)));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", DateTime.Now.AddMinutes(+660)));
            str.AppendLine("LOCATION: " + "abcd");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", msg.Subject));
            str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            var byteArray = Encoding.ASCII.GetBytes(str.ToString());
            var stream = new MemoryStream(byteArray);

            var attach = new Attachment(stream, "test.ics");

            mail.Attachments.Add(attach);

            var contype = new ContentType("text/calendar");
            contype.Parameters.Add("method", "REQUEST");
            //  contype.Parameters.Add("name", "Meeting.ics");
            var avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
            mail.AlternateViews.Add(avCal);
*/
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
    }

    public partial class HomeworkPanelPom
    {
        private readonly CultureInfo _ci = new CultureInfo("fr-FR");

        private IWebElement GetCurrentElement() => Driver.FindElement(By.Id("tab-devoirs-jour"), 1, 5);

        private IWebElement GetWorkToDoElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'afaire')]"));

        private IWebElement GetSessionsContentElement() => GetCurrentElement().FindElement(By.XPath("//*[contains(@ng-click, 'crseance')]"));

        private IWebElement GetDataElement() => Driver.FindElement(By.XPath($"//*[(@class = 'encartJour') and (@id != '')]"), 1, 5);

        private List<IWebElement> GetFileElements() => GetDataElement().FindElements(By.XPath($"//a[@file-type = 'FICHIER_CDT']")).ToList();

        private List<IWebElement> GetSubjectElements() => GetDataElement().FindElements(By.XPath($"//*[@class = '//*[@class = 'ed-card width-epingle']")).ToList();

        public HomeworkPanelPom(IWebDriver driver) : base(driver)
        {
        }
    }
}
