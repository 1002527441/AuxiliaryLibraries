using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuxiliaryLibraries;

namespace TestUnits
{
    [TestClass]
    public class AuxiliaryLibrariesUnitTest
    {
        [TestMethod]
        public void ToPrettyDate()
        {
            var persian_today = DateTime.Now.ToPrettyDate();
            var persian_yesterday = DateTime.Now.AddDays(-1).ToPrettyDate();
            var persian_date = DateTime.Now.AddDays(-10).ToPrettyDate();

            var todat = DateTime.Now.ToPrettyDate(toPersian: false);
            var yesterday = DateTime.Now.AddDays(-1).ToPrettyDate(toPersian: false);
            var date = DateTime.Now.AddDays(-10).ToPrettyDate(toPersian: false);

            var utc_persian_today = DateTime.UtcNow.ToPrettyDate(true);
            var utc_persian_yesterday = DateTime.UtcNow.AddDays(-1).ToPrettyDate(true);
            var utc_persian_date = DateTime.UtcNow.AddDays(-10).ToPrettyDate(true);

            var utc_today = DateTime.UtcNow.ToPrettyDate(true, false);
            var utc_yesterday = DateTime.UtcNow.AddDays(-1).ToPrettyDate(true, false);
            var utc_date = DateTime.UtcNow.AddDays(-10).ToPrettyDate(true, false);

            Assert.AreEqual(persian_today, utc_persian_today);
            Assert.AreEqual(persian_yesterday, utc_persian_yesterday);
            Assert.AreEqual(persian_date, utc_persian_date);

            Assert.AreEqual(todat, utc_today);
            Assert.AreEqual(yesterday, utc_yesterday);
            Assert.AreEqual(date, utc_date);
        }

        [TestMethod]
        public void ToPrettyTime()
        {
            var persian_today = DateTime.Now.ToPrettyTime();
            var persian_Seconds = DateTime.Now.AddSeconds(-30).ToPrettyTime();
            var persian_Minutes = DateTime.Now.AddMinutes(-10).ToPrettyTime();
            var persian_Hours = DateTime.Now.AddHours(-3).ToPrettyTime();
            var persian_Days = DateTime.Now.AddDays(-4).ToPrettyTime();
            var persian_Weeks = DateTime.Now.AddDays(-16).ToPrettyTime();
            var persian_Months = DateTime.Now.AddMonths(-3).ToPrettyTime();
            var persian_Years = DateTime.Now.AddMonths(-20).ToPrettyTime();

            var today = DateTime.Now.ToPrettyTime(toPersian: false);
            var Seconds = DateTime.Now.AddSeconds(-30).ToPrettyTime(toPersian: false);
            var Minutes = DateTime.Now.AddMinutes(-10).ToPrettyTime(toPersian: false);
            var Hours = DateTime.Now.AddHours(-3).ToPrettyTime(toPersian: false);
            var Days = DateTime.Now.AddDays(-4).ToPrettyTime(toPersian: false);
            var Weeks = DateTime.Now.AddDays(-16).ToPrettyTime(toPersian: false);
            var Months = DateTime.Now.AddMonths(-3).ToPrettyTime(toPersian: false);
            var Years = DateTime.Now.AddMonths(-20).ToPrettyTime(toPersian: false);

            var utc_persian_today = DateTime.UtcNow.ToPrettyTime(true);
            var utc_persian_Seconds = DateTime.UtcNow.AddSeconds(-30).ToPrettyTime(true);
            var utc_persian_Minutes = DateTime.UtcNow.AddMinutes(-10).ToPrettyTime(true);
            var utc_persian_Hours = DateTime.UtcNow.AddHours(-3).ToPrettyTime(true);
            var utc_persian_Days = DateTime.UtcNow.AddDays(-4).ToPrettyTime(true);
            var utc_persian_Weeks = DateTime.UtcNow.AddDays(-16).ToPrettyTime(true);
            var utc_persian_Months = DateTime.UtcNow.AddMonths(-3).ToPrettyTime(true);
            var utc_persian_Years = DateTime.UtcNow.AddMonths(-20).ToPrettyTime(true);

            var utc_today = DateTime.UtcNow.ToPrettyTime(true, false);
            var utc_Seconds = DateTime.UtcNow.AddSeconds(-30).ToPrettyTime(true, false);
            var utc_Minutes = DateTime.UtcNow.AddMinutes(-10).ToPrettyTime(true, false);
            var utc_Hours = DateTime.UtcNow.AddHours(-3).ToPrettyTime(true, false);
            var utc_Days = DateTime.UtcNow.AddDays(-4).ToPrettyTime(true, false);
            var utc_Weeks = DateTime.UtcNow.AddDays(-16).ToPrettyTime(true, false);
            var utc_Months = DateTime.UtcNow.AddMonths(-3).ToPrettyTime(true, false);
            var utc_Years = DateTime.UtcNow.AddMonths(-20).ToPrettyTime(true, false);

            Assert.AreEqual(persian_today, utc_persian_today);
            Assert.AreEqual(persian_Seconds, utc_persian_Seconds);
            Assert.AreEqual(persian_Minutes, utc_persian_Minutes);
            Assert.AreEqual(persian_Hours, utc_persian_Hours);
            Assert.AreEqual(persian_Days, utc_persian_Days);
            Assert.AreEqual(persian_Weeks, utc_persian_Weeks);
            Assert.AreEqual(persian_Months, utc_persian_Months);
            Assert.AreEqual(persian_Years, utc_persian_Years);

            Assert.AreEqual(today, utc_today);
            Assert.AreEqual(Seconds, utc_Seconds);
            Assert.AreEqual(Minutes, utc_Minutes);
            Assert.AreEqual(Hours, utc_Hours);
            Assert.AreEqual(Days, utc_Days);
            Assert.AreEqual(Weeks, utc_Weeks);
            Assert.AreEqual(Months, utc_Months);
            Assert.AreEqual(Years, utc_Years);
        }

    }
}
