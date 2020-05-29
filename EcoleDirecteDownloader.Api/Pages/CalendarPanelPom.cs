using StudiesManager.Services.Extensions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Globalization;

namespace EcoleDirecteDownloader.Api.Pages
{
    public partial class CalendarPanelPom : BasePom
    {
        public void GoToDate(DateTime date)
        {
            var dateAsInt = int.Parse(date.ToString("yyyyMM"));

            while (dateAsInt > int.Parse(GetCurrentMonth().ToString("yyyyMM")))
            {
                GetNextMonthElement().Click();
            }

            while (dateAsInt < int.Parse(GetCurrentMonth().ToString("yyyyMM")))
            {
                GetPreviousMonthElement().Click();
            }

            GetDayElement(date.Day).Click();
        }

        public DateTime GetCurrentMonth()
        {
            return DateTime.Parse(GetCurrentMonthElement().Text, _ci);
        }
    }

    public partial class CalendarPanelPom
    {
        private readonly CultureInfo _ci = new CultureInfo("fr-FR");

        public CalendarPanelPom(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement GetCalendarElement() => Driver.FindElement(By.Id("cdtnavigation-calendar"), 1, 5);

        private IWebElement GetMonthHeader() => GetCalendarElement().FindElement(By.XPath("//*[@class = 'picker-top-row']"));

        private IWebElement GetDayElement(int day) => GetCalendarElement().FindElement(By.XPath($"//*[contains(@class, ' picker-day')][text() = {day}]"));

        private List<IWebElement> GetDayElements() => GetCalendarElement().FindElements(By.XPath("//*[contains(@class, ' picker-day') and not(contains(@class, 'picker-empty'))]")).ToList();

        IWebElement GetCurrentDayElement() => GetCalendarElement().FindElement(By.Id("cdtnavigation-calendar"));

        private IWebElement GetCurrentMonthElement() => GetMonthHeader().FindElement(By.XPath("//*[contains(@class, 'picker-month')]"));

        private IWebElement GetPreviousMonthElement() => GetMonthHeader().FindElement(By.XPath("//*[contains(@class, 'picker-navigate-left-arrow')]"));

        private IWebElement GetNextMonthElement() => GetMonthHeader().FindElement(By.XPath("//*[contains(@class, 'picker-navigate-right-arrow')]"));
    }
}
