using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using EcoleDirecteDownloader.Api.Models;
using StudiesManager.Services.Models;
using OpenQA.Selenium.Interactions;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomeworkBookPom : BasePom
    {
        public DateTime CurrentDate { get; set; }

        public HomeworkBookPom GoToDate(DateTime date)
        {
            GetCalendarElement().GoToDate(date);

            CurrentDate = date;

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
            MoveToHomeworkBook();
        }

        public IWebElement GetHeaderElement() => Driver.FindElement(By.XPath("//*[@class = 'cdt']"), 1, 5);

        public IWebElement GetCurrentWorkToDoElement() => Driver.FindElement(By.XPath("//*[@class = 'btn btn-default active']"));

        private CalendarPanelPom GetCalendarElement() => new CalendarPanelPom(Driver);

        private HomeworkPanelPom GetHomeworkElement() => new HomeworkPanelPom(Driver, CurrentDate);

        private void MoveToHomeworkBook()
        {
            var headerElement = GetHeaderElement();
            var actions = new Actions(Driver);
            actions.MoveToElement(headerElement);
            actions.Perform();
        }
    }
}
