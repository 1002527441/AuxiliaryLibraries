# AuxiliaryLibraries
This library helps you to solve your problems about datetimes, ip, rest, json and string extensions.
## AuxiliaryCalendar
* ToPrettyDate()
* -> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
* -> Return: string
```
var persianDate = DateTime.Now.ToPrettyDate();
//persianDate : "امروز ساعت 10:57"

// set isUtc as true
var persianDate2 = DateTime.UtcNow.ToPrettyDate(true);
//persianDate2 : "امروز ساعت 10:57"

// set toPersian as false will return datetime in english
var persianDate3 = DateTime.UtcNow.ToPrettyDate(toPersian:false);
//persianDate3 : "Today at 10:57"
```

* ToPrettyTime()
* -> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
* -> Return: string
```
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyTime();
//persianDate : "10 دقیقه پیش"

// set isUtc as true
var persianDate = DateTime.UtcNow.AddDays(-15).ToPrettyTime(true);
//persianDate2 : "2 هفته پیش"

// set toPersian as false will return datetime in english
var persianDate3 = DateTime.UtcNow.AddMonths(-3).ToPrettyTime(toPersian:false);
//persianDate3 : "3 months ago"
```

* ToPrettyDateTime()
* -> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
* -> Return: string
```
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyDateTime();
//persianDate : "هجدهم آذر ماه 1395 ساعت 14:30"

// set isUtc as true
// set toPersian as false will return datetime in english
var persianDate2 = DateTime.UtcNow.AddDays(-15).ToPrettyDateTime(isUtc:true, toPersian:false);
//persianDate2 : "Ninth of December 2016 at 14:30"
```

* ToPrettyDay()
* -> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
* -> Return: string
```
var persianDate1 = DateTime.Now.ToPrettyDay();
//persianDate1 : "امروز"

var persianDate2 = DateTime.Now.AddMinutes(-1).ToPrettyDay();
//persianDate2 : "دیروز"

var persianDate3 = DateTime.Now.AddMinutes(1).ToPrettyDay();
//persianDate3 : "فردا"

var persianDate4 = DateTime.Now.AddMinutes(-35).ToPrettyDay();
//persianDate4 : "5 هفته پیش"
```

* ToPrettyDayOfWeek()
* -> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true, bool dayNumber = false
* -> Return: string
```
var persianDate1 = DateTime.Now.ToPrettyDayOfWeek();
//persianDate1 : "امروز"

var persianDate2 = DateTime.Now.AddMinutes(-1).ToPrettyDayOfWeek();
//persianDate2 : "دیروز"

var persianDate3 = DateTime.Now.AddMinutes(+10).ToPrettyDayOfWeek();
//persianDate3 : "جمعه"

var persianDate4 = DateTime.Now.AddMinutes(-5).ToPrettyDayOfWeek();
//persianDate4 : "شنبه"
```

* ToPersianDateTime()
* -> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
* -> Return: string
```
var persianDate = DateTime.Now.ToPersianDateTime();
//persianDate : "1395/9/30 ساعت 10:57"

var persianDate2 = DateTime.Now.ToPersianDateTime(false, "-");
//persianDate2 : "1395-9-30 ساعت 10:57"
```
UTC:
```
var persianDate = DateTime.UtcNow.ToPersianDateTime(true);
//persianDate : "1395/9/30 ساعت 10:57"

var persianDate2 = DateTime.UtcNow.ToPersianDateTime(true, "-");
//persianDate2 : "1395-9-30 ساعت 10:57"
```

* ToPersianDate()
* -> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
* -> Return: string
```
var persianDate = DateTime.Now.ToPersianDate();
//persianDate : "1395/09/30"

var persianDate2 = DateTime.Now.ToPersianDate(false, "-");
//persianDate2 : "1395-9-30"
```
UTC:
```
var persianDate = DateTime.UtcNow.ToPersianDate(true);
//persianDate : "1395/09/30"

var persianDate2 = DateTime.UtcNow.ToPersianDate(true, "-");
//persianDate2 : "1395-9-30"
```

* ToPersianFullDateTime()
* -> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
* -> Return: string
```
var persianDate = DateTime.Now.ToPersianFullDateTime();
//persianDate : "1395/9/30 10:57:23:547"

var persianDate2 = DateTime.Now.ToPersianFullDateTime(false, "-");
//persianDate2 : "1395-9-30 10:57:23:547"
```
UTC:
```
var persianDate = DateTime.UtcNow.ToPersianFullDateTime(true);
//persianDate : "1395/09/30 10:57:23:547"

var persianDate2 = DateTime.UtcNow.ToPersianFullDateTime(true, "-");
//persianDate2 : "1395-9-30 10:57:23:547"
```

* BeginDate()
```
var midnight = DateTime.Now.BeginDate();
// midnight : 12/20/2016 00:00:00
```

* EndDate()
```
var tommorowMidnight = DateTime.Now.EndDate();
//12/20/2016 23:59:59
```

* ConvertFromUTC()
* -> Parameters : DateTime dateTime, TimeZoneInfo destinationTimeZone
* -> Return: DateTime
```
var date = DateTime.UtcNow.ConvertFromUTC(TimeZoneInfo.FindSystemTimeZoneById(AuxiliaryCalendar.IranianTimeZone));
//date : "2016/9/30 10:57:23:547"
```

* DayOfWeek()
* -> Parameters : DateTime dateTime, bool toPersian = true, bool dayNumber = false
* -> Return: string
```
var persianDate = DateTime.Now.DayOfWeek();
//persianDate : " چهارشنبه"

var persianDate2 = DateTime.Now.DayOfWeek(toPersian:false);
//persianDate2 : "Wednesday"

var persianDate3 = DateTime.Now.DayOfWeek(dayNumber:true);
//persianDate3 : "4 شنبه"
```

* DayOfMonth()
* -> Parameters : int day, bool isPersian
* -> Return: string
```
var persianDate = AuxiliaryCalendar.DayOfMonth(20);
//persianDate : " بیستم"
```

* Month()
* -> Parameters : int day, bool isPersian
* -> Return: string
```
var persianDate = AuxiliaryCalendar.Month(9);
//persianDate : " آذر"
```

* GetFirstDayOfThisMonth()
* -> Parameters : bool isPersian = true
* -> Return: DateTime
```
Get the first day of current persian month
var date = AuxiliaryCalendar.GetFirstDayOfThisMonth();
//date : "2016-06-18"

Get the first day of current miladi month
var date = AuxiliaryCalendar.GetFirstDayOfThisMonth(isPersian:false);
//date : "2016-06-01"
```

* GetFirstDayOfThisWeek()
* -> Parameters : bool isPersian = true
* -> Return: DateTime
```
Get the first day of current persian week
var date = AuxiliaryCalendar.GetFirstDayOfThisWeek();
//date : "2016-06-20"

Get the first day of current miladi week
var date = AuxiliaryCalendar.GetFirstDayOfThisWeek(isPersian:false);
//date : "2016-06-22"
```

* GetNextNearestWeekday()
* -> Parameters : DateTime start, DayOfWeek day
* -> Return: DateTime
```
Get Next Nearest day of Week, For example, get next nearest Sunday from Now
var date = DateTime.Now.GetNextNearestWeekday(DayOfWeek.Sunday);
//date : "2016-06-29"
```

* GetFirstDayOfLastXDay()
* -> Parameters : int xDay, bool isPersian = true
* -> Return: DateTime
```
Get First Day Of Last X-Day, For example, get next nearest Sunday from Now
var date = AuxiliaryCalendar.GetFirstDayOfLastXDay(15);
//date : "2016-06-29"
```

## RestApi

## StringExtensions

## IpAddress

## FileHelpers

## Encription
