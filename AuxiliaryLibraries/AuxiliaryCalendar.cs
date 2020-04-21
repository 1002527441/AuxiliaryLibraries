using AuxiliaryLibraries.Enums;
using AuxiliaryLibraries.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Persian calendar for convertin datetime
    /// </summary>
    public static class AuxiliaryCalendar
    {
        public const long secondTicks = 10000000;
        public const long minuteTicks = 600000000;
        public const long hourTicks = 36000000000;
        public const long dayTicks = 864000000000;
        public const long weekTicks = 6048000000000;
        public const long monthTicks = 25920000000000;
        /// <summary>
        /// Year Pattern
        /// </summary>
        public const string yearPattern = "yyyy";
        /// <summary>
        /// Month Pattern
        /// </summary>
        public const string monthPattern = "MM";
        /// <summary>
        /// Day Pattern
        /// </summary>
        public const string dayPattern = "dd";
        /// <summary>
        /// Hour:Minute
        /// </summary>
        public const string hourPattern = "HH:mm";
        /// <summary>
        /// Day Of The Week Pattern
        /// </summary>
        public const string dayOfTheWeekPattern = "EEEE";
        /// <summary>
        /// Year-Month-Day Pattern
        /// </summary>
        public const string datePattern = "yyyy/MM/dd";
        /// <summary>
        /// Datetime pattern
        /// </summary>
        public const string datetimePattern = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// Utc datetime pattern
        /// </summary>
        public const string utcDatetimePattern = "yyyy-MM-dd'T'hh:mm:ss.SSS";
        /// <summary>
        /// Valid Mobile Datetime Pattern
        /// </summary>
        public const string mobileDatetimePattern = "yyyy-MM-dd'T'HH:mm:sszzz";
        /// <summary>
        /// Iranian Time Zone
        /// </summary>
        public const string IranianTimeZone = "Iran Standard Time";
        /// <summary>
        /// Persian Date Pattern
        /// </summary>
        public const string PersianDatePattern = @"[1-9][0-9]{3}[-/, ][0-9]{2}[-/, ][0-9]{2}";
        /// <summary>
        /// Persian Date Pattern
        /// </summary>
        public const string PersianDatePattern1 = @"[1-9][0-9]{3}[-/, ][0-9]{1}[-/, ][0-9]{2}";
        /// <summary>
        /// Persian Date Pattern
        /// </summary>
        public const string PersianDatePattern2 = @"[1-9][0-9]{3}[-/, ][0-9]{2}[-/, ][0-9]{1}";
        /// <summary>
        /// Persian Date Pattern
        /// </summary>
        public const string PersianDatePattern3 = @"[1-9][0-9]{3}[-/, ][0-9]{1}[-/, ][0-9]{1}";
        /// <summary>
        /// Different Valid Separators
        /// </summary>
        public static char[] Seperators = new char[] { '-', '/', ' ', ',' };

        /// <summary>
        /// Convert date time to persian (shamsi) date time
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="delimiter">Separate Year,Month,Day with delimiter, the default valur is '/'</param>
        /// <returns>Persian date as string</returns>
        public static string ToPersianDateTime(this DateTime dateTime, bool isUtc = false, string delimiter = "/")
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;
            string strdate = null;

            strdate = date.ToString(datePattern);

            var Date = DateTime.Parse(strdate);

            int year = shamsi.GetYear(Date);

            int month = shamsi.GetMonth(Date);

            int day = shamsi.GetDayOfMonth(Date);

            int hour = date.Hour;

            int minute = date.Minute;

            return $"{year}{delimiter}{month.ToTowDigits()}{delimiter}{day.ToTowDigits()} ساعت {hour.ToTowDigits()}:{minute.ToTowDigits()}";
        }

        /// <summary>
        /// Convert date time to persian (shamsi) date without time
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="delimiter">Separate Year,Month,Day with delimiter, the default valur is '/'</param>
        /// <returns>Persian date as string</returns>
        public static string ToPersianDate(this DateTime dateTime, bool isUtc = false, string delimiter = "/")
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;
            string strdate = null;

            strdate = date.ToString(datePattern);

            var Date = DateTime.Parse(strdate);

            int year = shamsi.GetYear(Date);

            int month = shamsi.GetMonth(Date);

            int day = shamsi.GetDayOfMonth(Date);

            return $"{year}{delimiter}{month.ToTowDigits()}{delimiter}{day.ToTowDigits()}";
        }

        /// <summary>
        /// Convert date time to persian (shamsi) date time with Second and Millisecond
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="delimiter">Separate Year,Month,Day with delimiter, the default valur is '/'</param>
        /// <returns>Persian date as string</returns>
        public static string ToPersianFullDateTime(this DateTime dateTime, bool isUtc = false, string delimiter = "/")
        {
            try
            {
                System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;

                string strdate = null;

                strdate = date.ToString(datePattern);

                var Date = DateTime.Parse(strdate);

                int year = shamsi.GetYear(Date);

                int month = shamsi.GetMonth(Date);

                int day = shamsi.GetDayOfMonth(Date);

                int hour = date.Hour;

                int minute = date.Minute;

                int second = date.Second;

                int millisecond = date.Millisecond;

                return $"{year}{delimiter}{month.ToTowDigits()}{delimiter}{day.ToTowDigits()} {hour.ToTowDigits()}:{minute.ToTowDigits()}:{second.ToTowDigits()}:{millisecond}";
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert Utc date time to Milidai date time and vise versa.
        /// </summary>
        /// <param name="dateTime">Utc date time</param>
        /// <param name="destinationTimeZone">Time zone</param>
        /// <returns>Miladi date time or UTC date time</returns>
        public static DateTime ConvertFromUTC(DateTime dateTime, TimeZoneInfo destinationTimeZone)
        {
            DateTime cstTime = DateTime.Now;
            try
            {
                if (dateTime.Kind == DateTimeKind.Utc)
                {
                    cstTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
                }
                else if (dateTime.Kind == DateTimeKind.Unspecified)
                {
                    DateTime utc = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    cstTime = TimeZoneInfo.ConvertTimeFromUtc(utc, destinationTimeZone);
                }
                else
                {
                    DateTime date = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
                    cstTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, destinationTimeZone);
                }
                //Console.WriteLine("The date and time are {0} {1}.", cstTime, destinationTimeZone.IsDaylightSavingTime(cstTime) ? destinationTimeZone.DaylightName : destinationTimeZone.StandardName);
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
            }
            catch { }
            return cstTime;
        }

        /// <summary>
        /// Return a datetime as midnight time
        /// </summary>
        /// <param name="date">Milidai datetime</param>
        /// <param name="hour">Default value is 0</param>
        /// <param name="min">Default value is 0</param>
        /// <param name="sec">Default value is 0</param>
        /// <param name="mil">Default value is 0</param>
        /// <returns>DateTime as midnight time</returns>
        public static DateTime BeginDate(this DateTime date, int hour = 0, int min = 0, int sec = 0, int mil = 0)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, min, sec, mil);
        }

        /// <summary>
        /// Return a datetime as the end time of day (Tomorrow midnight, somehow)
        /// </summary>
        /// <param name="date">Milidai datetime</param>
        /// <param name="hour">Default value is 23</param>
        /// <param name="min">Default value is 59</param>
        /// <param name="sec">Default value is 59</param>
        /// <param name="mil">Default value is 999</param>
        /// <returns>DateTime as end time of day</returns>
        public static DateTime EndDate(this DateTime date, int hour = 23, int min = 59, int sec = 59, int mil = 999)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, min, 59, 999);
        }

        /// <summary>
        /// Convert datetime to date as pretty format.
        /// Samples:
        /// Persian format = هجدهم آذر ماه 1395
        /// Miladi format = Ninth of December 2016
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="toPersian">If you want to convert to persian date send it true, and for miladi date send false. Default value is true</param>
        /// <returns>string</returns>
        public static string ToPrettyDate(this DateTime dateTime, bool isUtc = false, bool toPersian = true)
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;

            #region OldVersion
            /*var result = string.Empty;
            var time = date.Ticks;
            var midNightDate = DateTime.Now.BeginDate();

            //int today = GetDayOfWeek();
            //int passedDayOdWeek = today + 1 <= 7 ? today + 1 : 1;

            long midNightTime = midNightDate.Ticks;

            if (time >= midNightTime)
            {
                result = toPersian ? $"{DisplayNames.TodayPersian} ساعت {date.ToString(hourPattern)}" : $"{DisplayNames.Today} at {date.ToString(hourPattern)}";
            }
            else if (time >= midNightTime - dayTicks)
            {
                result = toPersian ? $"{DisplayNames.YesterdayPersian} ساعت {date.ToString(hourPattern)}" : $"{DisplayNames.Yesterday} at {date.ToString(hourPattern)}";
            }
            //else if (time >= midNightTime - (oneDay * passedDayOdWeek))
            //{
            //    result = GetDayOfWeek(GetDayOfWeek(date.DayOfWeek.ToString()), isPersian);
            //}
            else
            {
                int year = date.Year, month = date.Month, day = date.Day, hour = date.Hour, minute = date.Minute, second = date.Second, millisecond = date.Millisecond;
                if (toPersian)
                {


                    string strdate = null;

                    strdate = dateTime.ToString(datePattern);

                    var Date = DateTime.Parse(strdate);

                    year = shamsi.GetYear(dateTime);

                    month = shamsi.GetMonth(dateTime);

                    day = shamsi.GetDayOfMonth(dateTime);

                    hour = dateTime.Hour;

                    minute = dateTime.Minute;
                }
                var dayOfWeek = GetDayOfWeek(GetDayOfWeek(date.DayOfWeek.ToString()), toPersian);
                var dayOfMonth = GetDayOfMonth(day, toPersian);
                var monthOfYear = GetMonth(month, toPersian);
                monthOfYear = toPersian ? $"{monthOfYear} ماه" : $"of {monthOfYear}";
                result = $"{dayOfWeek} {dayOfMonth} {monthOfYear} {year}";
            }*/
            #endregion

            #region NewVerion
            var result = string.Empty;

            long diff = 0;
            long absoluteDiff = 0;

            var resultDay = General(date, toPersian, out diff, out absoluteDiff);

            var listOfDay = new List<long>() {
                0, //Today
                1, //Tomorrow
                -1 //Yesterday
            };

            if (listOfDay.Contains(diff))
            {
                result = resultDay;
            }
            else
            {
                int year = date.Year, month = date.Month, day = date.Day, hour = date.Hour, minute = date.Minute, second = date.Second, millisecond = date.Millisecond;
                if (toPersian)
                {
                    string strdate = null;
                    strdate = dateTime.ToString(datePattern);
                    var Date = DateTime.Parse(strdate);
                    year = shamsi.GetYear(dateTime);
                    month = shamsi.GetMonth(dateTime);
                    day = shamsi.GetDayOfMonth(dateTime);
                    hour = dateTime.Hour;
                    minute = dateTime.Minute;
                }
                var dayOfWeek = DayOfWeek(DayOfWeek(date.DayOfWeek.ToString()), toPersian);
                var dayOfMonth = DayOfMonth(day, toPersian);
                var monthOfYear = Month(month, toPersian);
                monthOfYear = toPersian ? $"{monthOfYear} ماه" : $"of {monthOfYear}";
                result = $"{dayOfWeek} {dayOfMonth} {monthOfYear} {year}";
            }
            #endregion

            return result;
        }

        /// <summary>
        /// Convert datetime to time as pretty format.
        /// Samples:
        /// Persian format = 18 دقیقه قبل
        /// Miladi format = 18 minutes ago
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="toPersian">If you want to convert to persian date send it true, and for miladi date send false. Default value is true</param>
        /// <returns>string</returns>
        public static string ToPrettyTime(this DateTime dateTime, bool isUtc = false, bool toPersian = true)
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;

            var result = string.Empty;
            var diff = DateTime.Now.Ticks - date.Ticks;
            var diffDate = DateTime.Now - date;

            if (diff == 0)
            {
                return toPersian ? DisplayNames.JustRightNowPersian : DisplayNames.JustRightNow;
            }
            else if (diff < minuteTicks)
            {
                int second = Convert.ToInt32(diffDate.TotalSeconds);
                second = second <= 0 ? 1 : second;
                result = toPersian ? string.Format(DisplayNames.SecondsAgoPersian, second) : string.Format(DisplayNames.SecondsAgo, second);
            }
            else if (diff < hourTicks)
            {
                int min = Convert.ToInt32(diffDate.TotalMinutes);
                min = min <= 0 ? 1 : min;
                result = toPersian ? string.Format(DisplayNames.MinutesAgoPersian, min) : string.Format(DisplayNames.MinutesAgo, min);
            }
            else if (diff < dayTicks)
            {
                int hour = Convert.ToInt32(diffDate.TotalHours);
                hour = hour <= 0 ? 1 : hour;
                result = toPersian ? string.Format(DisplayNames.HourAgoPersian, hour) : string.Format(DisplayNames.HourAgo, hour);
            }
            else if (diff < dayTicks * 2)
            {
                result = toPersian ? DisplayNames.YesterdayPersian : DisplayNames.Yesterday;
            }
            else if (diff < weekTicks)
            {
                int day = Convert.ToInt32(diffDate.TotalDays);
                day = day <= 0 ? 1 : day;
                result = toPersian ? string.Format(DisplayNames.DayAgoPersian, day) : string.Format(DisplayNames.DayAgo, day);
            }
            else if (diff < monthTicks)
            {
                int week = Convert.ToInt32(diffDate.TotalDays / 7);
                week = week <= 0 ? 1 : week;
                result = toPersian ? string.Format(DisplayNames.WeekAgoPersian, week) : string.Format(DisplayNames.WeekAgo, week);
            }
            else
            {
                int month = Convert.ToInt32(diffDate.TotalDays / 30);
                month = month <= 0 ? 1 : month;
                result = toPersian ? string.Format(DisplayNames.MonthAgoPersian, month) : string.Format(DisplayNames.MonthAgo, month);
            }

            return result;
        }

        /// <summary>
        /// Convert datetime to date as pretty format.
        /// Samples:
        /// Persian format = هجدهم آذر ماه 1395 ساعت 14:30
        /// Miladi format = Ninth of December 2016 at 14:30
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="toPersian">If you want to convert to persian date send it true, and for miladi date send false. Default value is true</param>
        /// <returns>string</returns>
        public static string ToPrettyDateTime(this DateTime dateTime, bool isUtc = false, bool toPersian = true)
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;

            var result = string.Empty;
            //var time = date.Ticks;
            //var midNightDate = DateTime.Now.BeginDate();
            //long midNightTime = midNightDate.Ticks;

            long diff = 0;
            long absoluteDiff = 0;

            var resultDay = General(date, toPersian, out diff, out absoluteDiff);

            var timeOfDayPersian = $"ساعت {date.ToString(hourPattern)}";
            var timeOfDayEnglish = $"at {date.ToString(hourPattern)}";

            var listOfDay = new List<long>() {
                0, //Today
                1, //Tomorrow
                -1 //Yesterday
            };

            //if (time >= midNightTime)
            //{
            //    result = toPersian ? $"{resultDay} {timeOfDay}" : $"{resultDay} at {date.ToString(hourPattern)}";
            //}
            //else if (time >= midNightTime - dayTicks)
            //{
            //    result = toPersian ? $"{resultDay} {timeOfDay}" : $"{resultDay} at {date.ToString(hourPattern)}";
            //}
            if (listOfDay.Contains(diff))
            {
                result = toPersian ? $"{resultDay} {timeOfDayPersian}" : $"{resultDay} {timeOfDayEnglish}";
            }
            else
            {
                int year = date.Year, month = date.Month, day = date.Day, hour = date.Hour, minute = date.Minute, second = date.Second, millisecond = date.Millisecond;
                if (toPersian)
                {
                    string strdate = null;
                    strdate = dateTime.ToString(datePattern);
                    var Date = DateTime.Parse(strdate);
                    year = shamsi.GetYear(dateTime);
                    month = shamsi.GetMonth(dateTime);
                    day = shamsi.GetDayOfMonth(dateTime);
                    hour = dateTime.Hour;
                    minute = dateTime.Minute;
                }
                var dayOfWeek = DayOfWeek(DayOfWeek(date.DayOfWeek.ToString()), toPersian);
                var dayOfMonth = DayOfMonth(day, toPersian);
                var monthOfYear = Month(month, toPersian);
                monthOfYear = toPersian ? $"{monthOfYear} ماه" : $"of {monthOfYear}";
                result = toPersian ? $"{dayOfWeek} {dayOfMonth} {monthOfYear} {year} {timeOfDayPersian}" :
                                     $"{dayOfWeek} {dayOfMonth} {monthOfYear} {year} {timeOfDayEnglish}";
            }

            return result;
        }

        /// <summary>
        /// Convert datetime to time as pretty format.
        /// Samples:
        /// Persian format = امروز ، دیروز، فردا، 2 روز پیش، 5 هفته بعد
        /// Miladi format = Today , Yesterday, Tomorrow, 2 days ago, 5 weeks later
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="toPersian">If you want to convert to persian date send it true, and for miladi date send false. Default value is true</param>
        /// <returns>string</returns>
        public static string ToPrettyDay(this DateTime dateTime, bool isUtc = false, bool toPersian = true)
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;

            long diff = 0;
            long absoluteDiff = 0;

            var result = General(date, toPersian, out diff, out absoluteDiff);

            if (diff == 0)
            {
                return toPersian ? DisplayNames.TodayPersian : DisplayNames.Today;
            }
            #region Ago
            else if (diff < 0)
            {
                if (diff == -1)
                {
                    result = toPersian ? DisplayNames.YesterdayPersian : DisplayNames.Yesterday;
                }
                else if (diff % 7 == 0)
                {
                    int week = Convert.ToInt32(absoluteDiff / 7);
                    week = week <= 0 ? 1 : week;
                    result = toPersian ? string.Format(DisplayNames.WeekAgoPersian, week) : string.Format(DisplayNames.WeekAgo, week);
                }
                else if (diff % 30 == 0)
                {
                    int month = Convert.ToInt32(absoluteDiff / 30);
                    month = month <= 0 ? 1 : month;
                    result = toPersian ? string.Format(DisplayNames.MonthAgoPersian, month) : string.Format(DisplayNames.MonthAgo, month);
                }
                else
                {
                    result = toPersian ? string.Format(DisplayNames.DayAgoPersian, absoluteDiff) : string.Format(DisplayNames.DayAgo, absoluteDiff);
                }
            }
            #endregion
            #region Later
            else if (diff > 0)
            {
                if (diff == 1)
                {
                    result = toPersian ? DisplayNames.TomorrowPersian : DisplayNames.Tomorrow;
                }
                else if (diff % 7 == 0)
                {
                    int week = Convert.ToInt32(absoluteDiff / 7);
                    week = week <= 0 ? 1 : week;
                    result = toPersian ? string.Format(DisplayNames.WeekLaterPersian, week) : string.Format(DisplayNames.WeekLater, week);
                }
                else if (diff % 30 == 0)
                {
                    int month = Convert.ToInt32(absoluteDiff / 30);
                    month = month <= 0 ? 1 : month;
                    result = toPersian ? string.Format(DisplayNames.MonthLaterPersian, month) : string.Format(DisplayNames.MonthLater, month);
                }
                else
                {
                    result = toPersian ? string.Format(DisplayNames.DayLaterPersian, absoluteDiff) : string.Format(DisplayNames.DayLater, absoluteDiff);
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// Convert datetime to day of the week as pretty format.
        /// Samples:
        /// Persian format is like = امروز ، دیروز ، فردا ، شنبه ، جمعه ، ...
        /// Miladi format is like = Today , Yesterday , Tomorrow , Saturday , Friday, ...
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="toPersian">If you want to convert to persian date send it true, and for miladi date send false. Default value is true</param>
        /// <param name="dayNumber">If you chose persian date, it may need to return 1 شنبه, instead of یکشنبه. So set it true</param>
        /// <returns>string</returns>
        public static string ToPrettyDayOfWeek(this DateTime dateTime, bool isUtc = false, bool toPersian = true, bool dayNumber = false)
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertFromUTC(dateTime, TimeZoneInfo.FindSystemTimeZoneById(IranianTimeZone)) : dateTime;

            long diff = 0;
            long absoluteDiff = 0;

            var result = General(date, toPersian, out diff, out absoluteDiff);

            if (diff == 0)
            {
                return toPersian ? DisplayNames.TodayPersian : DisplayNames.Today;
            }
            else if (diff == -1)
            {
                result = toPersian ? DisplayNames.YesterdayPersian : DisplayNames.Yesterday;
            }
            else if (diff == 1)
            {
                result = toPersian ? DisplayNames.TomorrowPersian : DisplayNames.Tomorrow;
            }
            else
            {
                result = DayOfWeek(dateTime, toPersian, dayNumber);
            }
            return result;
        }

        private static string General(DateTime date, bool toPersian, out long diff, out long absoluteDiff)
        {
            var result = string.Empty;
            diff = ((DateTime.Today.Ticks - date.BeginDate().Ticks) / dayTicks) * -1;
            absoluteDiff = diff < 0 ? diff * -1 : diff;
            if (diff == 0)
            {
                return toPersian ? DisplayNames.TodayPersian : DisplayNames.Today;
            }
            else if (diff == -1)
            {
                result = toPersian ? DisplayNames.YesterdayPersian : DisplayNames.Yesterday;
            }
            else if (diff == 1)
            {
                result = toPersian ? DisplayNames.TomorrowPersian : DisplayNames.Tomorrow;
            }
            return result;
        }

        /// <summary>
        /// Convert datetime to day of the week as pretty format.
        /// Samples:
        /// Persian format is like = شنبه ، یکشنبه ، دوشنبه ، سه شنبه، چهارشنبه ، پنجشنبه ، جمعه
        /// Miladi format is like = Saturday , Sunday , Monday , Tuesday , Wednesday, Thursday, Friday
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="toPersian">If you want to convert to persian date send it true, and for miladi date send false. Default value is true</param>
        /// <param name="dayNumber">If you chose persian date, it may need to return 1 شنبه, instead of یکشنبه. So set it true</param>
        /// <returns>string</returns>
        public static string DayOfWeek(this DateTime dateTime, bool toPersian = true, bool dayNumber = false)
        {
            switch (dateTime.DayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    return toPersian ? dayNumber ? $"1 {DisplayNames.SaturdayPersian}" : DisplayNames.SundayPersian : DisplayNames.Sunday;
                case System.DayOfWeek.Monday:
                    return toPersian ? dayNumber ? $"2 {DisplayNames.SaturdayPersian}" : DisplayNames.MondayPersian : DisplayNames.Monday;
                case System.DayOfWeek.Tuesday:
                    return toPersian ? dayNumber ? $"3 {DisplayNames.SaturdayPersian}" : DisplayNames.TuesdayPersian : DisplayNames.Tuesday;
                case System.DayOfWeek.Wednesday:
                    return toPersian ? dayNumber ? $"4 {DisplayNames.SaturdayPersian}" : DisplayNames.WednesdayPersian : DisplayNames.Wednesday;
                case System.DayOfWeek.Thursday:
                    return toPersian ? dayNumber ? $"5 {DisplayNames.SaturdayPersian}" : DisplayNames.ThursdayPersian : DisplayNames.Thursday;
                case System.DayOfWeek.Friday:
                    return toPersian ? DisplayNames.FridayPersian : DisplayNames.Friday;
                case System.DayOfWeek.Saturday:
                    return toPersian ? DisplayNames.SaturdayPersian : DisplayNames.Saturday;
            }
            return string.Empty;
        }

        /// <summary>
        /// Get name of days in week by their number
        /// </summary>
        /// <param name="day"></param>
        /// <param name="toPersian"></param>
        /// <returns>string</returns>
        public static string DayOfWeek(this int day, bool toPersian = true)
        {
            switch (day)
            {
                case 1:
                    {
                        return toPersian ? DisplayNames.SaturdayPersian : DisplayNames.Saturday;
                    }
                case 2:
                    {
                        return toPersian ? DisplayNames.SundayPersian : DisplayNames.Sunday;
                    }
                case 3:
                    {
                        return toPersian ? DisplayNames.MondayPersian : DisplayNames.Monday;
                    }
                case 4:
                    {
                        return toPersian ? DisplayNames.TuesdayPersian : DisplayNames.Tuesday;
                    }
                case 5:
                    {
                        return toPersian ? DisplayNames.WednesdayPersian : DisplayNames.Wednesday;
                    }
                case 6:
                    {
                        return toPersian ? DisplayNames.ThursdayPersian : DisplayNames.Thursday;
                    }
                case 7:
                    {
                        return toPersian ? DisplayNames.FridayPersian : DisplayNames.Friday;
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// Return the index of day of week, you should pass one of saturday, sunday, monday, tuesday, wednesday, thursday, friday
        /// </summary>
        /// <param name="day"></param>
        /// <returns>int</returns>
        public static int DayOfWeek(string day = null)
        {
            day = string.IsNullOrEmpty(day) ? DateTime.Now.DayOfWeek.ToString() : day;
            switch (day.ToLower())
            {
                case "saturday":
                    {
                        return 1;
                    }
                case "sunday":
                    {
                        return 2;
                    }
                case "monday":
                    {
                        return 3;
                    }
                case "tuesday":
                    {
                        return 4;
                    }
                case "wednesday":
                    {
                        return 5;
                    }
                case "thursday":
                    {
                        return 6;
                    }
                case "friday":
                    {
                        return 7;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        /// <summary>
        /// Get name of days in month by their number
        /// </summary>
        /// <param name="day"></param>
        /// <param name="toPersian"></param>
        /// <returns>string</returns>
        public static string DayOfMonth(int day, bool toPersian = true)
        {
            switch (day)
            {
                case 1:
                    {
                        return toPersian ? DisplayNames.day1Persian : DisplayNames.day1;
                    }
                case 2:
                    {
                        return toPersian ? DisplayNames.day2Persian : DisplayNames.day2;
                    }
                case 3:
                    {
                        return toPersian ? DisplayNames.day3Persian : DisplayNames.day3;
                    }
                case 4:
                    {
                        return toPersian ? DisplayNames.day4Persian : DisplayNames.day4;
                    }
                case 5:
                    {
                        return toPersian ? DisplayNames.day5Persian : DisplayNames.day5;
                    }
                case 6:
                    {
                        return toPersian ? DisplayNames.day6Persian : DisplayNames.day6;
                    }
                case 7:
                    {
                        return toPersian ? DisplayNames.day7Persian : DisplayNames.day7;
                    }
                case 8:
                    {
                        return toPersian ? DisplayNames.day8Persian : DisplayNames.day8;
                    }
                case 9:
                    {
                        return toPersian ? DisplayNames.day9Persian : DisplayNames.day9;
                    }
                case 10:
                    {
                        return toPersian ? DisplayNames.day10Persian : DisplayNames.day10;
                    }
                case 11:
                    {
                        return toPersian ? DisplayNames.day11Persian : DisplayNames.day11;
                    }
                case 12:
                    {
                        return toPersian ? DisplayNames.day12Persian : DisplayNames.day12;
                    }
                case 13:
                    {
                        return toPersian ? DisplayNames.day13Persian : DisplayNames.day13;
                    }
                case 14:
                    {
                        return toPersian ? DisplayNames.day14Persian : DisplayNames.day14;
                    }
                case 15:
                    {
                        return toPersian ? DisplayNames.day15Persian : DisplayNames.day15;
                    }
                case 16:
                    {
                        return toPersian ? DisplayNames.day16Persian : DisplayNames.day16;
                    }
                case 17:
                    {
                        return toPersian ? DisplayNames.day17Persian : DisplayNames.day17;
                    }
                case 18:
                    {
                        return toPersian ? DisplayNames.day18Persian : DisplayNames.day18;
                    }
                case 19:
                    {
                        return toPersian ? DisplayNames.day19Persian : DisplayNames.day19;
                    }
                case 20:
                    {
                        return toPersian ? DisplayNames.day20Persian : DisplayNames.day20;
                    }
                case 21:
                    {
                        return toPersian ? DisplayNames.day21Persian : DisplayNames.day21;
                    }
                case 22:
                    {
                        return toPersian ? DisplayNames.day22Persian : DisplayNames.day22;
                    }
                case 23:
                    {
                        return toPersian ? DisplayNames.day23Persian : DisplayNames.day23;
                    }
                case 24:
                    {
                        return toPersian ? DisplayNames.day24Persian : DisplayNames.day24;
                    }
                case 25:
                    {
                        return toPersian ? DisplayNames.day25Persian : DisplayNames.day25;
                    }
                case 26:
                    {
                        return toPersian ? DisplayNames.day26Persian : DisplayNames.day26;
                    }
                case 27:
                    {
                        return toPersian ? DisplayNames.day27Persian : DisplayNames.day27;
                    }
                case 28:
                    {
                        return toPersian ? DisplayNames.day28Persian : DisplayNames.day28;
                    }
                case 29:
                    {
                        return toPersian ? DisplayNames.day29Persian : DisplayNames.day29;
                    }
                case 30:
                    {
                        return toPersian ? DisplayNames.day30Persian : DisplayNames.day30;
                    }
                case 31:
                    {
                        return toPersian ? DisplayNames.day31Persian : DisplayNames.day31;
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// Return name of month by its index, if set the isPersian to true it'll return persian names and if the isPersian set to false it'll miladi names
        /// </summary>
        /// <param name="month"></param>
        /// <param name="toPersian"></param>
        /// <returns>string</returns> 
        public static string Month(int month, bool toPersian = true)
        {
            switch (month)
            {
                case 1:
                    return toPersian ? DisplayNames.Month1Persian : DisplayNames.Month1;
                case 2:
                    return toPersian ? DisplayNames.Month2Persian : DisplayNames.Month2;
                case 3:
                    return toPersian ? DisplayNames.Month3Persian : DisplayNames.Month3;
                case 4:
                    return toPersian ? DisplayNames.Month4Persian : DisplayNames.Month4;
                case 5:
                    return toPersian ? DisplayNames.Month5Persian : DisplayNames.Month5;
                case 6:
                    return toPersian ? DisplayNames.Month6Persian : DisplayNames.Month6;
                case 7:
                    return toPersian ? DisplayNames.Month7Persian : DisplayNames.Month7;
                case 8:
                    return toPersian ? DisplayNames.Month8Persian : DisplayNames.Month8;
                case 9:
                    return toPersian ? DisplayNames.Month9Persian : DisplayNames.Month9;
                case 10:
                    return toPersian ? DisplayNames.Month10Persian : DisplayNames.Month10;
                case 11:
                    return toPersian ? DisplayNames.Month11Persian : DisplayNames.Month11;
                case 12:
                    return toPersian ? DisplayNames.Month12Persian : DisplayNames.Month12;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Return name of month by its index, if set the isPersian to true it'll return persian names and if the isPersian set to false it'll miladi names
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toPersian"></param>
        /// <returns>string</returns> 
        public static string Month(this DateTime dateTime, bool toPersian = true)
        {
            PersianCalendar shamsi = new PersianCalendar();

            switch (shamsi.GetMonth(dateTime))
            {
                case 1:
                    return toPersian ? DisplayNames.Month1Persian : DisplayNames.Month1;
                case 2:
                    return toPersian ? DisplayNames.Month2Persian : DisplayNames.Month2;
                case 3:
                    return toPersian ? DisplayNames.Month3Persian : DisplayNames.Month3;
                case 4:
                    return toPersian ? DisplayNames.Month4Persian : DisplayNames.Month4;
                case 5:
                    return toPersian ? DisplayNames.Month5Persian : DisplayNames.Month5;
                case 6:
                    return toPersian ? DisplayNames.Month6Persian : DisplayNames.Month6;
                case 7:
                    return toPersian ? DisplayNames.Month7Persian : DisplayNames.Month7;
                case 8:
                    return toPersian ? DisplayNames.Month8Persian : DisplayNames.Month8;
                case 9:
                    return toPersian ? DisplayNames.Month9Persian : DisplayNames.Month9;
                case 10:
                    return toPersian ? DisplayNames.Month10Persian : DisplayNames.Month10;
                case 11:
                    return toPersian ? DisplayNames.Month11Persian : DisplayNames.Month11;
                case 12:
                    return toPersian ? DisplayNames.Month12Persian : DisplayNames.Month12;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Convert persian date to miladi
        /// If the passed value is not valid, it will return Datetime.Now
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(int year, int month, int day)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                DateTime dt = new DateTime(year, month, day, new PersianCalendar());
                //dt.ToString(CultureInfo.InvariantCulture)
                return dt;
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Convert persian date to miladi, you should pass a string by this format 1357-12-22.
        /// Seperators sould be on if ('),(/),( ),(,)
        /// First part should be 4 digits, second and third must be 2 digits
        /// Smaples : 1300-01-01 , 1300/10/01 , 1300 08 23
        /// If the passed value is not valid, it will return new DateTime()
        /// </summary>
        /// <param name="persianDate"></param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this string persianDate)
        {
            if (new Regex(PersianDatePattern).IsMatch(persianDate)
                || new Regex(PersianDatePattern1).IsMatch(persianDate)
                || new Regex(PersianDatePattern2).IsMatch(persianDate)
                || new Regex(PersianDatePattern3).IsMatch(persianDate))
            {
                int year = 0, month = 0, day = 0;
                var numbers = persianDate.Split(Seperators);
                if (Int32.TryParse(numbers[0], out year) && Int32.TryParse(numbers[1], out month) && Int32.TryParse(numbers[2], out day))
                    return ToDateTime(year, month, day);
            }
            return new DateTime();
        }

        /// <summary>
        /// Return first day of month as DateTime object.
        /// By default toPersian is set to true, and it means it will convert input dateTime to persian date, calculate first day of month and it will convert the reslut to DateTime object again.
        /// If toPersian sets as false, it just calculate dateTime in miladi system.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toPersian"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime dateTime, bool toPersian = true)
        {
            if (toPersian)
            {
                PersianCalendar shamsi = new PersianCalendar();
                int year = shamsi.GetYear(DateTime.Now);
                int month = shamsi.GetMonth(DateTime.Now);
                int day = shamsi.GetDayOfMonth(DateTime.Now);

                var firstDayOfMonth = ToDateTime($"{year}-{month.ToTowDigits()}-01");
                return firstDayOfMonth;
            }
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Return first day of month as string and in persian.
        /// The input dateTime is string and it must be passed a variable as persian datetime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FirstDayOfMonth(this string dateTime)
        {
            var date = dateTime.ToDateTime();
            var miladi = date.FirstDayOfMonth();
            return miladi.ToPersianDate();
        }

        /// <summary>
        /// Return last day of month as DateTime object.
        /// By default toPersian is set to true, and it means it will convert input dateTime to persian date, calculate last day of month and it will convert the reslut to DateTime object again.
        /// If toPersian sets as false, it just calculate dateTime in miladi system.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toPersian"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime dateTime, bool toPersian = true)
        {
            if (toPersian)
            {
                PersianCalendar shamsi = new PersianCalendar();
                int month = shamsi.GetMonth(DateTime.Now);

                var firstDayOfMonth = FirstDayOfMonth(dateTime);
                var lastDayOfMonth = month <= 6 ? firstDayOfMonth.AddDays(31) : firstDayOfMonth.AddDays(30);
                return lastDayOfMonth;
            }
            return dateTime.FirstDayOfMonth(toPersian).AddMonths(1);
        }

        /// <summary>
        /// Return last day of month as string and in persian.
        /// The input dateTime is string and it must be passed a variable as persian datetime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string LastDayOfMonth(this string dateTime)
        {
            var date = dateTime.ToDateTime();
            var miladi = date.LastDayOfMonth();
            return miladi.ToPersianDate();
        }

        /// <summary>
        /// Return first day of current week
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(this DateTime dateTime)
        {
            //Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6
            var dayIndex = dateTime.DayOfWeek;
            if (dayIndex > 0)
            {
                var temp = (int)dayIndex * -1;
                var fistDayOfWeek = dateTime.AddDays(temp);
                return fistDayOfWeek;
            }
            else return dateTime;
        }

        /// <summary>
        /// Return first day of current week as Miladi Datetime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(this string dateTime)
        {
            //Saturday = 0, Sunday = 1, Monday = 2, Tuesday = 3, Wednesday = 4, Thursday = 5, Friday = 6
            var date = dateTime.ToDateTime();
            var dayIndex = date.PersianDayOfWeek();
            if (dayIndex > 0)
            {
                var temp = (int)dayIndex * -1;
                var fistDayOfWeek = date.AddDays(temp);
                return fistDayOfWeek;
            }
            else return date;
        }

        /// <summary>
        /// Return first day of current week as Persian Datetime in string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toPersian"></param>
        /// <returns></returns>
        public static string FirstDayOfWeek(this string dateTime, bool toPersian)
        {
            //Saturday = 0, Sunday = 1, Monday = 2, Tuesday = 3, Wednesday = 4, Thursday = 5, Friday = 6
            return dateTime.FirstDayOfWeek().ToPersianDate();
        }

        /// <summary>
        /// Return last day of current week
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfWeek(this DateTime dateTime)
        {
            //Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6
            var dayIndex = dateTime.DayOfWeek;
            if ((int)dayIndex < 6)
            {
                var temp = 6 - (int)dayIndex;
                var lastDayOfWeek = dateTime.AddDays(temp);
                return lastDayOfWeek;
            }
            else return dateTime;
        }

        /// <summary>
        /// Return last day of current week as Miladi DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfWeek(this string dateTime)
        {
            //Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6
            var date = dateTime.ToDateTime();
            var dayIndex = date.PersianDayOfWeek();
            if ((int)dayIndex < 6)
            {
                var temp = 6 - (int)dayIndex;
                var lastDayOfWeek = date.AddDays(temp);
                return lastDayOfWeek;
            }
            else return date;
        }

        /// <summary>
        /// Return last day of current week as Persian DateTime in string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toPersian"></param>
        /// <returns></returns>
        public static string LastDayOfWeek(this string dateTime, bool toPersian)
        {
            //Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6
            return dateTime.LastDayOfWeek().ToPersianDate();
        }

        /// <summary>
        /// Get Persian Day Of Week
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static PersianDayOfWeek PersianDayOfWeek(this DateTime dateTime)
        {
            //Saturday = 0, Sunday = 1, Monday = 2, Tuesday = 3, Wednesday = 4, Thursday = 5, Friday = 6
            var dayIndex = dateTime.DayOfWeek;
            switch (dayIndex)
            {
                case System.DayOfWeek.Sunday:
                    return Enums.PersianDayOfWeek.Shanbe;
                case System.DayOfWeek.Monday:
                    return Enums.PersianDayOfWeek.Yekshanbe;
                case System.DayOfWeek.Tuesday:
                    return Enums.PersianDayOfWeek.Doshanbe;
                case System.DayOfWeek.Wednesday:
                    return Enums.PersianDayOfWeek.Seshanbe;
                case System.DayOfWeek.Thursday:
                    return Enums.PersianDayOfWeek.Chaharshanbe;
                case System.DayOfWeek.Friday:
                    return Enums.PersianDayOfWeek.Panjshanbe;
                case System.DayOfWeek.Saturday:
                    return Enums.PersianDayOfWeek.Jomeh;
            }
            return Enums.PersianDayOfWeek.Shanbe;
        }

        /// <summary>
        /// It returns year, month and day of your dateTime in persian as out integer
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static void SplitToPersianYearMonthDay(this DateTime dateTime, out int year, out int month, out int day)
        {
            PersianCalendar shamsi = new PersianCalendar();
            year = shamsi.GetYear(dateTime);
            month = shamsi.GetMonth(dateTime);
            day = shamsi.GetDayOfMonth(dateTime);
        }
    }

    //private class SolarCalendar
    //{

    //    public String strWeekDay = "";
    //    public String strMonth = "";

    //    int date;
    //    int month;
    //    int year;

    //    public SolarCalendar(Context context)
    //    {
    //        Date GregorianDate = new Date();
    //        calculateSolarCalendar(GregorianDate, context);
    //    }

    //    public SolarCalendar(Date GregorianDate, Context context)
    //    {
    //        calculateSolarCalendar(GregorianDate, context);
    //    }

    //    private void calculateSolarCalendar(Date GregorianDate, Context context)
    //    {

    //        int ld;

    //        int gregorianYear = GregorianDate.getYear() + 1900;
    //        int gregorianMonth = GregorianDate.getMonth() + 1;
    //        int gregorianDate = GregorianDate.getDate();
    //        int WeekDay = GregorianDate.getDay();

    //        int[] buf1 = new int[12];
    //        int[] buf2 = new int[12];

    //        buf1[0] = 0;
    //        buf1[1] = 31;
    //        buf1[2] = 59;
    //        buf1[3] = 90;
    //        buf1[4] = 120;
    //        buf1[5] = 151;
    //        buf1[6] = 181;
    //        buf1[7] = 212;
    //        buf1[8] = 243;
    //        buf1[9] = 273;
    //        buf1[10] = 304;
    //        buf1[11] = 334;

    //        buf2[0] = 0;
    //        buf2[1] = 31;
    //        buf2[2] = 60;
    //        buf2[3] = 91;
    //        buf2[4] = 121;
    //        buf2[5] = 152;
    //        buf2[6] = 182;
    //        buf2[7] = 213;
    //        buf2[8] = 244;
    //        buf2[9] = 274;
    //        buf2[10] = 305;
    //        buf2[11] = 335;

    //        if ((gregorianYear % 4) != 0)
    //        {
    //            date = buf1[gregorianMonth - 1] + gregorianDate;

    //            if (date > 79)
    //            {
    //                date = date - 79;
    //                if (date <= 186)
    //                {
    //                    switch (date % 31)
    //                    {
    //                        case 0:
    //                            month = date / 31;
    //                            date = 31;
    //                            break;
    //                        default:
    //                            month = (date / 31) + 1;
    //                            date = (date % 31);
    //                            break;
    //                    }
    //                    year = gregorianYear - 621;
    //                }
    //                else {
    //                    date = date - 186;

    //                    switch (date % 30)
    //                    {
    //                        case 0:
    //                            month = (date / 30) + 6;
    //                            date = 30;
    //                            break;
    //                        default:
    //                            month = (date / 30) + 7;
    //                            date = (date % 30);
    //                            break;
    //                    }
    //                    year = gregorianYear - 621;
    //                }
    //            }
    //            else {
    //                if ((gregorianYear > 1996) && (gregorianYear % 4) == 1)
    //                {
    //                    ld = 11;
    //                }
    //                else {
    //                    ld = 10;
    //                }
    //                date = date + ld;

    //                switch (date % 30)
    //                {
    //                    case 0:
    //                        month = (date / 30) + 9;
    //                        date = 30;
    //                        break;
    //                    default:
    //                        month = (date / 30) + 10;
    //                        date = (date % 30);
    //                        break;
    //                }
    //                year = gregorianYear - 622;
    //            }
    //        }
    //        else {
    //            date = buf2[gregorianMonth - 1] + gregorianDate;

    //            if (gregorianYear >= 1996)
    //            {
    //                ld = 79;
    //            }
    //            else {
    //                ld = 80;
    //            }
    //            if (date > ld)
    //            {
    //                date = date - ld;

    //                if (date <= 186)
    //                {
    //                    switch (date % 31)
    //                    {
    //                        case 0:
    //                            month = (date / 31);
    //                            date = 31;
    //                            break;
    //                        default:
    //                            month = (date / 31) + 1;
    //                            date = (date % 31);
    //                            break;
    //                    }
    //                    year = gregorianYear - 621;
    //                }
    //                else {
    //                    date = date - 186;

    //                    switch (date % 30)
    //                    {
    //                        case 0:
    //                            month = (date / 30) + 6;
    //                            date = 30;
    //                            break;
    //                        default:
    //                            month = (date / 30) + 7;
    //                            date = (date % 30);
    //                            break;
    //                    }
    //                    year = gregorianYear - 621;
    //                }
    //            }

    //            else {
    //                date = date + 10;

    //                switch (date % 30)
    //                {
    //                    case 0:
    //                        month = (date / 30) + 9;
    //                        date = 30;
    //                        break;
    //                    default:
    //                        month = (date / 30) + 10;
    //                        date = (date % 30);
    //                        break;
    //                }
    //                year = gregorianYear - 622;
    //            }

    //        }

    //        strMonth = GetMonth(month, context);
    //        strWeekDay = GetDayOfWeek(WeekDay, context);
    //    }

    //}
}