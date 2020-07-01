using EcoleDirecteController.Api.Models;
using EcoleDirecteDownloader.Api.Contracts;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace EcoleDirecteController.Api.Controllers
{
    [ApiController]
    [Route("ecoledirecte")]
    public class MaxicoursController : ControllerBase
    {
        private readonly IEcoleDirecteService _ecoleDirecteService;

        public MaxicoursController(IEcoleDirecteService ecoleDirecteService)
        {
            _ecoleDirecteService = ecoleDirecteService;
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            try
            {
                var result = _ecoleDirecteService.Home();

                if (!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homeworkbook")]
        public IActionResult HomeworkBook()
        {
            try
            {
                var result = _ecoleDirecteService.HomeworkBook();

                if (!result)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homework/{date}")]
        public IActionResult GetHomework(DateTime date)
        {
            try
            {
                _ecoleDirecteService.GetWorkToDo(date);
                _ecoleDirecteService.GetSessionsContent(date);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homework/fromdate/{fromDate}/todate/{toDate}")]
        public IActionResult GetHomeworkPeriod(DateTime fromDate, DateTime toDate)
        {
            _ecoleDirecteService.GoToHomeworkBookPage();

            var date = fromDate;
            do
            {
                try
                {
                    _ecoleDirecteService.GetHomework(date);
                    date = date.AddDays(1);
                }
                catch (Exception ex)
                {
                }
            } while (date <= toDate);

            return Ok();
        }

        [HttpGet]
        [Route("homework/worktodo/{date}")]
        public IActionResult GetWorkToDo(DateTime date)
        {
            try
            {
                var result = _ecoleDirecteService.GetWorkToDo(date);

                if (string.IsNullOrWhiteSpace(result))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homework/sessionscontent/{date}")]
        public IActionResult GetSessionsContent(DateTime date)
        {
            try
            {
                var result = _ecoleDirecteService.GetSessionsContent(date);

                if (string.IsNullOrWhiteSpace(result))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homework/sendmail")]
        public IActionResult SendMail()
        {
            try
            {
                var buffer = System.IO.File.ReadAllBytes(@"c:\perso\export\test.pdf");

                var attachment = new Ical.Net.DataTypes.Attachment(buffer);
                attachment.Parameters.Add("X-FILENAME", "test.pdf");
                var calendar = new Calendar();
                var vEvent = new CalendarEvent
                {
                    Start = new CalDateTime(DateTime.Parse("2020-05-24T07:00:00")),
                    End = new CalDateTime(DateTime.Parse("2020-05-24T08:00:00")),
                    Attachments = new List<Ical.Net.DataTypes.Attachment> { attachment }
                };

                calendar.Events.Add(vEvent);

                var serializer = new CalendarSerializer(new SerializationContext());
                string serializedCalendar = serializer.SerializeToString(calendar);

                using (StreamWriter outputFile = new StreamWriter(@"c:\perso\ocag.ics"))
                {
                    outputFile.Write(serializedCalendar);
                }

                /*
                                var fromAddress = new MailAddress("olive91370@gmail.com", "From Name");
                                var toAddress = new MailAddress("ocagliuli@hotmail.com", "To Name");
                                const string fromPassword = "ocag_2312";
                                const string subject = "Subject";
                                const string body = "Body";

                                var smtp = new SmtpClient
                                {
                                    Host = "smtp.gmail.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = false,
                                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                                };
                                using (var message = new MailMessage(fromAddress, toAddress)
                                {
                                    Subject = subject,
                                    Body = body
                                })
                                {
                                    var ct = new ContentType("text/calendar");
                                    ct.Parameters.Add("method", "REQUEST");
                                    var avCal = AlternateView.CreateAlternateViewFromString(serializer.SerializeToString(calendar), ct);
                                    message.AlternateViews.Add(avCal);

                                    smtp.Send(message);
                                }
                */
                //_ecoleDirecteService.SendMail();

                //var mail = new MailMessage("olive91370@gmail.com", "ocagliuli@hotmail.com");
                //mail.Subject = "Test Mail";
                //mail.Body = "This is for testing SMTP mail from GMAIL";
                //var smtpclient = new SmtpClient();
                //smtpclient.Host = "smtp.gmail.com"; //-------this has to given the Mailserver IP
                //smtpclient.Port = 465;
                //smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpclient.UseDefaultCredentials = false;
                //smtpclient.Credentials = new NetworkCredential("olive91370@gmail.com", "ocag_2312");
                //smtpclient.EnableSsl = true;
                //smtpclient.Send(mail);

                /*
                var fromAddress = new MailAddress("olive91370@gmail.com", "From Name");
                var toAddress = new MailAddress("ocagliuli@hotmail.com", "To Name");
                const string fromPassword = "ocag_2312";
                const string subject = "Subject";
                const string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
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
                    str.AppendLine(string.Format("DESCRIPTION:{0}", message.Body));
                    str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", message.Body));
                    str.AppendLine(string.Format("SUMMARY:{0}", message.Subject));
                    str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", message.From.Address));

                    str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", message.To[0].DisplayName, message.To[0].Address));

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

                    message.Attachments.Add(attach);

                    var contype = new ContentType("text/calendar");
                    contype.Parameters.Add("method", "REQUEST");
                    //  contype.Parameters.Add("name", "Meeting.ics");
                    var avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
                    message.AlternateViews.Add(avCal);

                    smtp.Send(message);
                }
                    */
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("homeworkbook/mail/reminder")]
        public IActionResult CreateMailReminder([FromBody]MailReminderModel model)
        {
            try
            {
                var buffer = System.IO.File.ReadAllBytes(@"c:\perso\export\test.pdf");

                var attachment = new Ical.Net.DataTypes.Attachment(buffer);
                attachment.Parameters.Add("X-FILENAME", "test.pdf");
                var calendar = new Calendar();
                var vEvent = new CalendarEvent
                {
                    Start = new CalDateTime(DateTime.Parse("2020-05-24T07:00:00")),
                    End = new CalDateTime(DateTime.Parse("2020-05-24T08:00:00")),
                    Attachments = new List<Ical.Net.DataTypes.Attachment> { attachment }
                };

                calendar.Events.Add(vEvent);

                var serializer = new CalendarSerializer(new SerializationContext());
                string serializedCalendar = serializer.SerializeToString(calendar);

                using (StreamWriter outputFile = new StreamWriter(@"c:\perso\ocag.ics"))
                {
                    outputFile.Write(serializedCalendar);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
