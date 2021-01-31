using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuxiliaryLibraries;

namespace TestUnits
{
    [TestClass]
    public class AuxiliaryLibrariesUnitTest
    {
        //nuget pack Package.nuspec
        [TestMethod]
        public void ToPrettyDate()
        {
            var persian_date1 = new DateTime(2019,01,07).ToPrettyDate();
            var persian_today = DateTime.Now.ToPrettyDate();
            var persian_yesterday = DateTime.Now.AddDays(-1).ToPrettyDate();
            var persian_date = DateTime.Now.AddDays(-10).ToPrettyDate();

            var persian_todayTime = DateTime.Now.ToPrettyDateTime();
            var persian_yesterdayTime = DateTime.Now.AddDays(-1).ToPrettyDateTime();
            var persian_dateTime = DateTime.Now.AddDays(-10).ToPrettyDateTime();

            var todat = DateTime.Now.ToPrettyDate(toPersian: false);
            var yesterday = DateTime.Now.AddDays(-1).ToPrettyDate(toPersian: false);
            var date = DateTime.Now.AddDays(-10).ToPrettyDate(toPersian: false);

            var utc_persian_today = DateTime.UtcNow.ToPrettyDate(true);
            var utc_persian_yesterday = DateTime.UtcNow.AddDays(-1).ToPrettyDate(true);
            var utc_persian_Tommorow = DateTime.UtcNow.AddDays(1).ToPrettyDate(true, false);
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

        [TestMethod]
        public void ToPrettyDay()
        {
            var _M8_day = DateTime.Now.AddDays(-8).ToPrettyDay();
            var _M7_day = DateTime.Now.AddDays(-7).ToPrettyDay();
            var _M6_day = DateTime.Now.AddDays(-6).ToPrettyDay();
            var _M5_day = DateTime.Now.AddDays(-5).ToPrettyDay();
            var _M4_day = DateTime.Now.AddDays(-4).ToPrettyDay();
            var _M3_day = DateTime.Now.AddDays(-3).ToPrettyDay();
            var _M2_day = DateTime.Now.AddDays(-2).ToPrettyDay();
            var _M1_day = DateTime.Now.AddDays(-1).ToPrettyDay();
            var persian_today = DateTime.Now.ToPrettyDay();
            var _1_day = DateTime.Now.AddDays(1).ToPrettyDay();
            var _2_day = DateTime.Now.AddDays(2).ToPrettyDay();
            var _3_day = DateTime.Now.AddDays(3).ToPrettyDay();
            var _4_day = DateTime.Now.AddDays(4).ToPrettyDay();
            var _5_day = DateTime.Now.AddDays(5).ToPrettyDay();
            var _6_day = DateTime.Now.AddDays(6).ToPrettyDay();
            var _7_day = DateTime.Now.AddDays(7).ToPrettyDay();
            var _8_day = DateTime.Now.AddDays(8).ToPrettyDay();
        }

        [TestMethod]
        public void ToPrettyWeekDay()
        {
            var _M8_day = DateTime.Now.AddDays(-8).ToPrettyDayOfWeek();
            var _M7_day = DateTime.Now.AddDays(-7).ToPrettyDayOfWeek();
            var _M6_day = DateTime.Now.AddDays(-6).ToPrettyDayOfWeek();
            var _M5_day = DateTime.Now.AddDays(-5).ToPrettyDayOfWeek();
            var _M4_day = DateTime.Now.AddDays(-4).ToPrettyDayOfWeek();
            var _M3_day = DateTime.Now.AddDays(-3).ToPrettyDayOfWeek();
            var _M2_day = DateTime.Now.AddDays(-2).ToPrettyDayOfWeek();
            var _M1_day = DateTime.Now.AddDays(-1).ToPrettyDayOfWeek();
            var persian_today = DateTime.Now.ToPrettyDayOfWeek();
            var _1_day = DateTime.Now.AddDays(1).ToPrettyDayOfWeek();
            var _2_day = DateTime.Now.AddDays(2).ToPrettyDayOfWeek();
            var _3_day = DateTime.Now.AddDays(3).ToPrettyDayOfWeek();
            var _4_day = DateTime.Now.AddDays(4).ToPrettyDayOfWeek();
            var _5_day = DateTime.Now.AddDays(5).ToPrettyDayOfWeek();
            var _6_day = DateTime.Now.AddDays(6).ToPrettyDayOfWeek();
            var _7_day = DateTime.Now.AddDays(7).ToPrettyDayOfWeek();
            var _8_day = DateTime.Now.AddDays(8).ToPrettyDayOfWeek();
        }

        [TestMethod]
        public void ToDateTime()
        {
            var enDate = new DateTime(2017, 11, 22);
            var first = AuxiliaryCalendar.ToDateTime(1396, 9, 1) == enDate;
            string date = "1396-09-01";
            var second = AuxiliaryCalendar.ToDateTime(date) == enDate;

            Assert.IsTrue(first && second);
        }

        [TestMethod]
        public void Encrypt()
        {
            var str1 = AuxiliaryEncryption.RC4.Encrypt("Amin", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var str2 = AuxiliaryEncryption.RC4.Encrypt("Mostafa", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var str3 = AuxiliaryEncryption.RC4.Encrypt("amin", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var str4 = AuxiliaryEncryption.RC4.Encrypt("mostafa", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var str5 = AuxiliaryEncryption.RC4.Encrypt("fahime", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var str6 = AuxiliaryEncryption.RC4.Encrypt("Fahime", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var str7 = AuxiliaryEncryption.RC4.Encrypt("AmiN", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
            var b1 = str1.Length;
            var b2 = str2.Length;
            var b3 = str3.Length;
            var b4 = str4.Length;
            var b5 = str5.Length;
            var b6 = str6.Length;
            var b7 = str7.Length;
        }

        [TestMethod]
        public void RSAEncrypttion()
        {
            var rsa = new AuxiliaryEncryption.RSA(2048);
            rsa.SavePrivateKey(@"X:\Fahime\Archive\Sadad\PrivateKey.xml");
            rsa.SavePublicKey(@"X:\Fahime\Archive\Sadad\PublicKey.xml");
        }

        [TestMethod]
        public void PriceModel()
        {
            //int _price = 1000;
            //var result1 = _price.ToToman(AuxiliaryPriceModel.Currency.Toman).PriceCommaDeLimited.ToPersianNumber();
            //var result2 = Convert.ToInt64(_price).ToToman(AuxiliaryPriceModel.Currency.Toman).PriceCommaDeLimited.ToPersianNumber();
            //var price1 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.IRR, AuxiliaryPriceModel.Currency.IRR);
            //var price2 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.IRR, AuxiliaryPriceModel.Currency.Toman);
            //var price3 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.Toman);
            //var price4 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.IRR);
            //var price5 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.BTC);
            //var price6 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.USD);
            //var price7 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.EUR);
            //var price8 = new AuxiliaryPriceModel(1000, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.GBP);
            var price9 = new AuxiliaryPriceModel(0.0012, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.BTC);
            var price10 = new AuxiliaryPriceModel(0.087, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.BTC);
            var price11 = new AuxiliaryPriceModel(0.97, AuxiliaryPriceModel.Currency.Toman, AuxiliaryPriceModel.Currency.BTC);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetFirstDayOfThisMonth()
        {
            var persianToday = AuxiliaryCalendar.GetFirstDayOfThisMonth();
            var today = AuxiliaryCalendar.GetFirstDayOfThisMonth(false);
        }

        [TestMethod]
        public void GetFirstDayOfThisWeek()
        {
            var persianToday = AuxiliaryCalendar.GetFirstDayOfThisWeek();
            var today = AuxiliaryCalendar.GetFirstDayOfThisWeek(false);
        }

        [TestMethod]
        public void GetFirstDayOfLastXDay()
        {
            var day0 = AuxiliaryCalendar.GetFirstDayOfLastXDay(1);
            var day1 = AuxiliaryCalendar.GetFirstDayOfLastXDay(2);
            var day2 = AuxiliaryCalendar.GetFirstDayOfLastXDay(8);
            var day3 = AuxiliaryCalendar.GetFirstDayOfLastXDay(10);
            var day4 = AuxiliaryCalendar.GetFirstDayOfLastXDay(15);
            var day5 = AuxiliaryCalendar.GetFirstDayOfLastXDay(23);
        }
    }
}
