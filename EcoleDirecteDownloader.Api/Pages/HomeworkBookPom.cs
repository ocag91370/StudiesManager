using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using EcoleDirecteDownloader.Api.Models;
using StudiesManager.Services.Models;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomeworkBookPom : BasePom
    {
        public HomeworkBookPom GoToDate(DateTime date)
        {
            GetCalendarElement().GoToDate(date);

            return this;
        }

        public string GetWorkToDo(WebDriverSettingsModel webDriverSettings)
        {
            return GetHomeworkElement().GetWorkToDo(webDriverSettings);
        }

        public string GetSessionsContent(WebDriverSettingsModel webDriverSettings)
        {
            return GetHomeworkElement().GetSessionsContent(webDriverSettings);
        }

        public void SendMail()
        {
            GetHomeworkElement().SendMail();
        }
    }

    public partial class HomeworkBookPom
    {
        public HomeworkBookPom(IWebDriver driver) : base(driver)
        {
        }

        private CalendarPanelPom GetCalendarElement() => new CalendarPanelPom(Driver);

        private HomeworkPanelPom GetHomeworkElement() => new HomeworkPanelPom(Driver);
    }
}
