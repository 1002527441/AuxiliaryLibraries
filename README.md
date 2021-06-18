# AuxiliaryLibraries
This library helps you to get rid of some repetitive code blocks about date-times, IP, Rest, JSON, encryption, files, directories, queue, zip, objects, and string extensions.
This is a good helper, and saves lots of time, especially for Iranian programmers.
## Auxiliary Calendar
* ToPrettyDate()
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
=> Return: string
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
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
=> Return: string
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
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
=> Return: string
```
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyDateTime();
//persianDate : "هجدهم آذر ماه 1395 ساعت 14:30"

// set isUtc as true
// set toPersian as false will return datetime in english
var persianDate2 = DateTime.UtcNow.AddDays(-15).ToPrettyDateTime(isUtc:true, toPersian:false);
//persianDate2 : "Ninth of December 2016 at 14:30"
```

* ToPrettyDay()
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
=> Return: string
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
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true, bool dayNumber = false
=> Return: string
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
=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
=> Return: string
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
=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
=> Return: string
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
=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
=> Return: string
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
=> Parameters : DateTime dateTime, TimeZoneInfo destinationTimeZone
=> Return: DateTime
```
var date = DateTime.UtcNow.ConvertFromUTC(TimeZoneInfo.FindSystemTimeZoneById(AuxiliaryCalendar.IranianTimeZone));
//date : "2016/9/30 10:57:23:547"
```

* DayOfWeek()
=> Parameters : DateTime dateTime, bool toPersian = true, bool dayNumber = false
=> Return: string
```
var persianDate = DateTime.Now.DayOfWeek();
//persianDate : " چهارشنبه"

var persianDate2 = DateTime.Now.DayOfWeek(toPersian:false);
//persianDate2 : "Wednesday"

var persianDate3 = DateTime.Now.DayOfWeek(dayNumber:true);
//persianDate3 : "4 شنبه"
```

* DayOfMonth()
=> Parameters : int day, bool isPersian
=> Return: string
```
var persianDate = AuxiliaryCalendar.DayOfMonth(20);
//persianDate : " بیستم"
```

* Month()
=> Parameters : int day, bool isPersian
=> Return: string
```
var persianDate = AuxiliaryCalendar.Month(9);
//persianDate : " آذر"
```

* GetFirstDayOfThisMonth()
=> Parameters : bool isPersian = true
=> Return: DateTime
```
Get the first day of current persian month
var date = AuxiliaryCalendar.GetFirstDayOfThisMonth();
//date : "2016-06-18"

Get the first day of current miladi month
var date = AuxiliaryCalendar.GetFirstDayOfThisMonth(isPersian:false);
//date : "2016-06-01"
```

* GetFirstDayOfThisWeek()
=> Parameters : bool isPersian = true
=> Return: DateTime
```
Get the first day of current persian week
var date = AuxiliaryCalendar.GetFirstDayOfThisWeek();
//date : "2016-06-20"

Get the first day of current miladi week
var date = AuxiliaryCalendar.GetFirstDayOfThisWeek(isPersian:false);
//date : "2016-06-22"
```

* GetNextNearestWeekday()
=> Parameters : DateTime start, DayOfWeek day
=> Return: DateTime
```
Get Next Nearest day of Week, For example, get next nearest Sunday from Now
var date = DateTime.Now.GetNextNearestWeekday(DayOfWeek.Sunday);
//date : "2016-06-29"
```

* GetFirstDayOfLastXDay()
=> Parameters : int xDay, bool isPersian = true
=> Return: DateTime
```
Get First Day Of Last X-Day
It will give you first nearest xDay from begining of month.
xDay = 7, Today = 23 => it will return : 21
xDay = 15, Today = 23 => it will return : 15
Be careful xDay Must be less than 30
```

## Auxiliary DirectoryFileHelper
* CreateFolderIfNeeded()
=> Parameters : string path
=> Return: bool
```
Create a folder if it doesn't exist
```

* CopyFolder()
=> Parameters : string sourcePath, string destinationPath
=> Return: bool
```
Copy a folder with all of its dub directories and files
```


* CopyFile()
=> Parameters : string sourcePath, string destinationPath
=> Return: bool
```
Copy a file
```

* Download()
=> Parameters : string url, string destination
=> Return: bool
```
Download a file
```


* IsImage()
=> Parameters : string contentType
=> Return: bool
```
Undrasting this file is image or not
```

## Auxiliary Encription
## RC4
* Encrypt()
=> Parameters : string key, string data, Encoding encoding, bool skipZero = false
=> Return: string
```
Encrypt your data with key and encoding
```

* Encrypt()
=> Parameters : string key, string data
=> Return: string
```
Encrypt your data with key
```


* Decrypt()
=> Parameters : string key, string data, Encoding encoding, bool skipZero = false
=> Return: string
```
Decrypt your data with key and encoding
```

* Decrypt()
=> Parameters : string key, string data
=> Return: string
```
Decrypt your data with key
```
## AES
* EncryptFile()
=> Parameters : string inputFile, string outputFile
=> Return: string
```
Encrypt your file, config of encyption is inside the constructor
```

* Encrypt()
=> Parameters : string strtoencrypt
=> Return: byte[]
```
Encrypt your data with key
```


* DecryptFile()
=> Parameters : string inputFile, string outputFile
=> Return: string
```
Decrypt your file, , config of decryption is inside the constructor
```

* Decrypt()
=> Parameters : string strtoencrypt
=> Return: string
```
Decrypt your data with key
```

## RSA
* Encrypt()
=> Parameters : string plainText
=> Return: string
```
Encrypt your data, config (Sign, PrivateKey, PublicKey) of encyption is inside the constructor
```

* Decrypt()
=> Parameters : string inputFile, string outputFile
=> Return: string
```
Decrypt your data, , config (Sign, PrivateKey, PublicKey) of decryption is inside the constructor
```

## Auxiliary Http
* Read()
=> Parameters : System.Net.Http.HttpRequestMessage Request
=> Return: System.Collections.Generic.List<object>
```
Fetch Requests (HttpRequestMessage) as List of object
```
 
* IsMobileRequest()
=> Parameters : NONE
=> Return: bool
```
Check the user request sent from mobile or not
```

## AuxiliaryObjectCopier
* Clone()
=> Parameters : T source
=> Return: T
```
Perform a deep copy of the object via serialization.
var person = new Person();
var clonedPerson = person.Clone();
Be careful, The type must be serializable
```
 
## Auxiliary RestApi
* Send()
=> Parameters : string baseUrl, string functionName, Method method, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string userName, string password
=> Return: IRestResponse
```
Send your request with RestSharp
```

* Send()
=> Parameters : string baseUrl, string functionName, Method method, IDictionary<string, string> headers, object body, string userName, string password
=> Return: IRestResponse
```
Send your request with RestSharp
```

* Send()
=> Parameters : string url, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string method = "GET", bool contentLength = true
=> Return: string
```
Send your request with WebClient
```

## AuxiliaryZip
* Compress()
=> Parameters : IEnumerable<string> fileNames, string destinationFileName
=> Return: void
```
Create a ZIP file of the files provided.
```
  
* Compress()
=> Parameters : string directoryPath
=> Return: void
```
Compress entire folder by passing directory Path
```
  
* Decompress()
=> Parameters : string path, bool pathIsDirectory = false
=> Return: void
```
Decompress *.zip file, if pathIsDirectory pass as false. (One Zip file)
Decompress every *.zip files inside filder by passing directory Path, if pathIsDirectory pass as true. (Multiple Zip files)
```
  

* Decompress()
=> Parameters : System.IO.FileInfo fileToDecompress, string newFileName = null
=> Return: void
```
Decompress *.zip file by passing FileInfo
If you pass newFileName, the zip file extracted on this path,
otherwise it (if you pass newFileName as null, default value) the zip file extracted on the parent folder
```

## Auxiliary IpAddress
* IsInRange()
=> Parameters : IPAddress ip, IPAddress lowerRange, IPAddress upperRange
=> Return: bool
```
Return true, if ip is in the range of lowerRange and upperRange
```
  
## Auxiliary StringExtensions
