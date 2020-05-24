using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class HomeworkBookPage : BasePage
    {
        public HomeworkBookPage GoToDate(DateTime date)
        {
            GetCalendarElement().GoToDate(date);

            return this;
        }

        public string GetWorkToDo()
        {
            return GetHomeworkElement().GetWorkToDo();
        }

        public string GetSessionsContent()
        {
            return GetHomeworkElement().GetSessionsContent();
        }
    }

    public partial class HomeworkBookPage : BasePage
    {
        public HomeworkBookPage(IWebDriver driver) : base(driver)
        {
        }

        private CalendarPanel GetCalendarElement() => new CalendarPanel(Driver);

        private HomeworkPanel GetHomeworkElement() => new HomeworkPanel(Driver);
    }
}
