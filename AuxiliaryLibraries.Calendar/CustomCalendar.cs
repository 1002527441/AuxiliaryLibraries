using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries.Calendar
{
    public static class CustomCalendar
    {
        public static string datePattern = "yyyy/MM/dd";
        public static string datetimePattern = "yyyy-MM-dd HH:mm:ss";
        public static string utcDatetimePattern = "yyyy-MM-dd'T'hh:mm:ss.SSS";

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
            DateTime date = isUtc ? ConvertUTCtoDateTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")) : dateTime;
            string strdate = null;

            strdate = date.ToString(datePattern);

            var Date = DateTime.Parse(strdate);

            int year = shamsi.GetYear(Date);

            int month = shamsi.GetMonth(Date);

            int day = shamsi.GetDayOfMonth(Date);

            int hour = date.Hour;

            int minute = date.Minute;

            return $"{year}{delimiter}{month}{delimiter}{day} ساعت {hour}:{minute}";
        }

        /// <summary>
        /// Convert date time to persian (shamsi) date without time
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="delimiter">Separate Year,Month,Day with delimiter, the default valur is '/'</param>
        /// <returns>Persian date as string</returns>
        public static string ToPersianDate(this DateTime dateTime, bool isUtc, string delimiter = "/")
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            DateTime date = isUtc ? ConvertUTCtoDateTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")) : dateTime;
            string strdate = null;

            strdate = date.ToString(datePattern);

            var Date = DateTime.Parse(strdate);

            int year = shamsi.GetYear(Date);

            int month = shamsi.GetMonth(Date);
            string M = month < 10 ? "0" + month : month.ToString();

            int day = shamsi.GetDayOfMonth(Date);
            string D = day < 10 ? "0" + day : day.ToString();

            return $"{year}{delimiter}{M}{delimiter}{D}";
        }

        /// <summary>
        /// Convert date time to persian (shamsi) date time with Second and Millisecond
        /// </summary>
        /// <param name="dateTime">Miladi date time or Utc date time</param>
        /// <param name="isUtc">If Date is utc send it True, the default valur is False</param>
        /// <param name="delimiter">Separate Year,Month,Day with delimiter, the default valur is '/'</param>
        /// <returns>Persian date as string</returns>
        public static string ToPersianFullDateTime(this DateTime dateTime, bool isUtc, string delimiter = "/")
        {
            try
            {
                System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                DateTime date = isUtc ? ConvertUTCtoDateTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")) : dateTime;

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

                return $"{year}{delimiter}{month}{delimiter}{day} {hour}:{minute}:{second}:{millisecond}";
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert Utc date time to Milidai date time
        /// </summary>
        /// <param name="dateTime">Utc date time</param>
        /// <param name="destinationTimeZone">Time zone</param>
        /// <returns>Miladi date time</returns>
        public static DateTime ConvertUTCtoDateTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
        {
            try
            {
                //TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
                Console.WriteLine("The date and time are {0} {1}.",
                                  cstTime,
                                  destinationTimeZone.IsDaylightSavingTime(cstTime) ?
                                          destinationTimeZone.DaylightName : destinationTimeZone.StandardName);
                return cstTime;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
            }
            return DateTime.Now;
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
    }
}
