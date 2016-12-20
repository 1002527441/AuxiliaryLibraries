# AuxiliaryLibraries
This library helps you to solve your problems about datetimes, ip, rest, json and string extensions.
## Persian Calendar
* ToPrettyDate()
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

* ToPersianDateTime()
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
