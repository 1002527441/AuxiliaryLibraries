<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/amin8zamany/AuxiliaryLibraries">
    <img src="AuxiliaryLibraries_Logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">AuxiliaryLibraries</h3>
  <p align="left">
    This library helps you to get rid of some repetitive code blocks about date-times, IP, Rest, JSON, encryption, files, directories, queue, zip, objects, and string    extensions.
    <br />
     This is a good helper, and saves lots of time, especially for Iranian programmers.
    <br />
    <br />
  </p>
  <p align="center">
    <a href="https://www.nuget.org/packages/AuxiliaryLibraries/">Nuget Package >></a>
  </p>
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#auxiliarylibraries-1">About The Project</a></li>
    <li><a href="#auxiliary-calendar">Auxiliary Calendar</a></li>
    <li><a href="#auxiliary-directoryfilehelper">Auxiliary DirectoryFileHelper</a></li>
    <li>
      <a href="#auxiliary-encription">Auxiliary Encryption</a>
      <ul>
        <li><a href="#rc4">RC4</a></li>
        <li><a href="#aes">AES</a></li>
        <li><a href="#rsa">RSA</a></li>
      </ul>
    </li>
    <li><a href="#auxiliary-http">Auxiliary Http</a></li>
    <li><a href="#auxiliaryobjectcopier">Auxiliary Object Copier</a></li>
    <li><a href="#auxiliary-restapi">Auxiliary Rest Api</a></li>
    <li><a href="#auxiliaryzip">Auxiliary Zip</a></li>
    <li><a href="#auxiliary-ipaddress">Auxiliary IP Address</a></li>
    <li><a href="#auxiliary-stringextensions">Auxiliary String Extensions</a></li>
  </ol>
</details>

# AuxiliaryLibraries
This library helps you to get rid of some repetitive code blocks about date-times, IP, Rest, JSON, encryption, files, directories, queue, zip, objects, and string extensions.
This is a good helper, and saves lots of time, especially for Iranian programmers.
## Auxiliary Calendar
* ToPrettyDate()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
<br/>
=> Return: string
```CS
var persianDate = DateTime.Now.ToPrettyDate();
```
//persianDate : "امروز ساعت 10:57"

// set isUtc as true
```CS
var persianDate2 = DateTime.UtcNow.ToPrettyDate(true);
```
//persianDate2 : "امروز ساعت 10:57"

// set toPersian as false will return datetime in english
```CS
var persianDate3 = DateTime.UtcNow.ToPrettyDate(toPersian:false);
```
//persianDate3 : "Today at 10:57"
<br/>
* ToPrettyTime()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
<br/>
=> Return: string
```
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyTime();
```
//persianDate : "10 دقیقه پیش"

// set isUtc as true
```
var persianDate = DateTime.UtcNow.AddDays(-15).ToPrettyTime(true);
```
//persianDate2 : "2 هفته پیش"

// set toPersian as false will return datetime in english
```
var persianDate3 = DateTime.UtcNow.AddMonths(-3).ToPrettyTime(toPersian:false);
```
//persianDate3 : "3 months ago"
<br/>
* ToPrettyDateTime()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
<br/>
=> Return: string
```
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyDateTime();
```
//persianDate : "هجدهم آذر ماه 1395 ساعت 14:30"

// set isUtc as true
// set toPersian as false will return datetime in english
```
var persianDate2 = DateTime.UtcNow.AddDays(-15).ToPrettyDateTime(isUtc:true, toPersian:false);
```
//persianDate2 : "Ninth of December 2016 at 14:30"
<br/>

* ToPrettyDay()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
<br/>
=> Return: string
```
var persianDate1 = DateTime.Now.ToPrettyDay();
```
//persianDate1 : "امروز"

```
var persianDate2 = DateTime.Now.AddMinutes(-1).ToPrettyDay();
```
//persianDate2 : "دیروز"

```
var persianDate3 = DateTime.Now.AddMinutes(1).ToPrettyDay();
```
//persianDate3 : "فردا"

```
var persianDate4 = DateTime.Now.AddMinutes(-35).ToPrettyDay();
```
//persianDate4 : "5 هفته پیش"
<br/>
* ToPrettyDayOfWeek()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true, bool dayNumber = false
<br/>
=> Return: string
```
var persianDate1 = DateTime.Now.ToPrettyDayOfWeek();
```
//persianDate1 : "امروز"

```
var persianDate2 = DateTime.Now.AddMinutes(-1).ToPrettyDayOfWeek();
```
//persianDate2 : "دیروز"

```
var persianDate3 = DateTime.Now.AddMinutes(+10).ToPrettyDayOfWeek();
```
//persianDate3 : "جمعه"

```
var persianDate4 = DateTime.Now.AddMinutes(-5).ToPrettyDayOfWeek();
```
//persianDate4 : "شنبه"

<br/>
* ToPersianDateTime()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
<br/>
=> Return: string
```
var persianDate = DateTime.Now.ToPersianDateTime();
```
//persianDate : "1395/9/30 ساعت 10:57"

```
var persianDate2 = DateTime.Now.ToPersianDateTime(false, "-");
```
//persianDate2 : "1395-9-30 ساعت 10:57"
<br/>
UTC:
```
var persianDate = DateTime.UtcNow.ToPersianDateTime(true);
```
//persianDate : "1395/9/30 ساعت 10:57"

```
var persianDate2 = DateTime.UtcNow.ToPersianDateTime(true, "-");
```
//persianDate2 : "1395-9-30 ساعت 10:57"
<br/>
* ToPersianDate()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
<br/>
=> Return: string
```
var persianDate = DateTime.Now.ToPersianDate();
```
//persianDate : "1395/09/30"

```
var persianDate2 = DateTime.Now.ToPersianDate(false, "-");
```
//persianDate2 : "1395-9-30"
<br/>
UTC:
```
var persianDate = DateTime.UtcNow.ToPersianDate(true);
```
//persianDate : "1395/09/30"

```
var persianDate2 = DateTime.UtcNow.ToPersianDate(true, "-");
```
//persianDate2 : "1395-9-30"
<br/>

* ToPersianFullDateTime()
<br/>
=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"
<br/>
=> Return: string
```
var persianDate = DateTime.Now.ToPersianFullDateTime();
```
//persianDate : "1395/9/30 10:57:23:547"

```
var persianDate2 = DateTime.Now.ToPersianFullDateTime(false, "-");
```
//persianDate2 : "1395-9-30 10:57:23:547"
<br/>
UTC:
```
var persianDate = DateTime.UtcNow.ToPersianFullDateTime(true);
```
//persianDate : "1395/09/30 10:57:23:547"

```
var persianDate2 = DateTime.UtcNow.ToPersianFullDateTime(true, "-");
```
//persianDate2 : "1395-9-30 10:57:23:547"
<br/>

* BeginDate()
```
var midnight = DateTime.Now.BeginDate();
```
// midnight : 12/20/2016 00:00:00
<br/>

* EndDate()
```
var tommorowMidnight = DateTime.Now.EndDate();
```
//12/20/2016 23:59:59
<br/>

* ConvertFromUTC()
<br/>
=> Parameters : DateTime dateTime, TimeZoneInfo destinationTimeZone
<br/>
=> Return: DateTime
```
var date = DateTime.UtcNow.ConvertFromUTC(TimeZoneInfo.FindSystemTimeZoneById(AuxiliaryCalendar.IranianTimeZone));
```
//date : "2016/9/30 10:57:23:547"
<br/>

* DayOfWeek()
<br/>
=> Parameters : DateTime dateTime, bool toPersian = true, bool dayNumber = false
<br/>
=> Return: string
```
var persianDate = DateTime.Now.DayOfWeek();
```
//persianDate : " چهارشنبه"

```
var persianDate2 = DateTime.Now.DayOfWeek(toPersian:false);
```
//persianDate2 : "Wednesday"

```
var persianDate3 = DateTime.Now.DayOfWeek(dayNumber:true);
```
//persianDate3 : "4 شنبه"
<br/>

* DayOfMonth()
<br/>
=> Parameters : int day, bool isPersian
<br/>
=> Return: string
```
var persianDate = AuxiliaryCalendar.DayOfMonth(20);
//persianDate : " بیستم"
```

* Month()
<br/>
=> Parameters : int day, bool isPersian
<br/>
=> Return: string
```
var persianDate = AuxiliaryCalendar.Month(9);
//persianDate : " آذر"
```

* GetFirstDayOfThisMonth()
<br/>
=> Parameters : bool isPersian = true
<br/>
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
<br/>
=> Parameters : bool isPersian = true
<br/>
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
<br/>
=> Parameters : DateTime start, DayOfWeek day
<br/>
=> Return: DateTime
```
Get Next Nearest day of Week, For example, get next nearest Sunday from Now
var date = DateTime.Now.GetNextNearestWeekday(DayOfWeek.Sunday);
//date : "2016-06-29"
```

* GetFirstDayOfLastXDay()
<br/>
=> Parameters : int xDay, bool isPersian = true
<br/>
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
<br/>
=> Parameters : string path
<br/>
=> Return: bool
```
Create a folder if it doesn't exist
```

* CopyFolder()
<br/>
=> Parameters : string sourcePath, string destinationPath
<br/>
=> Return: bool
```
Copy a folder with all of its dub directories and files
```


* CopyFile()
<br/>
=> Parameters : string sourcePath, string destinationPath
<br/>
=> Return: bool
```
Copy a file
```

* Download()
<br/>
=> Parameters : string url, string destination
<br/>
=> Return: bool
```
Download a file
```


* IsImage()
<br/>
=> Parameters : string contentType
<br/>
=> Return: bool
```
Undrasting this file is image or not
```

## Auxiliary Encription
## RC4
* Encrypt()
<br/>
=> Parameters : string key, string data, Encoding encoding, bool skipZero = false
<br/>
=> Return: string
```
Encrypt your data with key and encoding
```

* Encrypt()
<br/>
=> Parameters : string key, string data
<br/>
=> Return: string
```
Encrypt your data with key
```


* Decrypt()
<br/>
=> Parameters : string key, string data, Encoding encoding, bool skipZero = false
<br/>
=> Return: string
```
Decrypt your data with key and encoding
```

* Decrypt()
<br/>
=> Parameters : string key, string data
<br/>
=> Return: string
```
Decrypt your data with key
```
## AES
* EncryptFile()
<br/>
=> Parameters : string inputFile, string outputFile
<br/>
=> Return: string
```
Encrypt your file, config of encyption is inside the constructor
```

* Encrypt()
<br/>
=> Parameters : string strtoencrypt
<br/>
=> Return: byte[]
```
Encrypt your data with key
```


* DecryptFile()
<br/>
=> Parameters : string inputFile, string outputFile
<br/>
=> Return: string
```
Decrypt your file, , config of decryption is inside the constructor
```

* Decrypt()
<br/>
=> Parameters : string strtoencrypt
<br/>
=> Return: string
```
Decrypt your data with key
```

## RSA
* Encrypt()
<br/>
=> Parameters : string plainText
<br/>
=> Return: string
```
Encrypt your data, config (Sign, PrivateKey, PublicKey) of encyption is inside the constructor
```

* Decrypt()
<br/>
=> Parameters : string inputFile, string outputFile
<br/>
=> Return: string
```
Decrypt your data, , config (Sign, PrivateKey, PublicKey) of decryption is inside the constructor
```

## Auxiliary Http
* Read()
<br/>
=> Parameters : System.Net.Http.HttpRequestMessage Request
<br/>
=> Return: System.Collections.Generic.List<object>
```
Fetch Requests (HttpRequestMessage) as List of object
```
 
* IsMobileRequest()
<br/>
=> Parameters : NONE
<br/>
=> Return: bool
```
Check the user request sent from mobile or not
```

## AuxiliaryObjectCopier
* Clone()
<br/>
=> Parameters : T source
<br/>
=> Return: T
```
Perform a deep copy of the object via serialization.
var person = new Person();
var clonedPerson = person.Clone();
Be careful, The type must be serializable
```
 
## Auxiliary RestApi
* Send()
<br/>
=> Parameters : string baseUrl, string functionName, Method method, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string userName, string password
<br/>
=> Return: IRestResponse
```
Send your request with RestSharp
```

* Send()
<br/>
=> Parameters : string baseUrl, string functionName, Method method, IDictionary<string, string> headers, object body, string userName, string password
<br/>
=> Return: IRestResponse
```
Send your request with RestSharp
```

* Send()
<br/>
=> Parameters : string url, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string method = "GET", bool contentLength = true
<br/>
=> Return: string
```
Send your request with WebClient
```

## AuxiliaryZip
* Compress()
<br/>
=> Parameters : IEnumerable<string> fileNames, string destinationFileName
<br/>
=> Return: void
```
Create a ZIP file of the files provided.
```
  
* Compress()
<br/>
=> Parameters : string directoryPath
<br/>
=> Return: void
```
Compress entire folder by passing directory Path
```
  
* Decompress()
<br/>
=> Parameters : string path, bool pathIsDirectory = false
<br/>
=> Return: void
```
Decompress *.zip file, if pathIsDirectory pass as false. (One Zip file)
Decompress every *.zip files inside filder by passing directory Path, if pathIsDirectory pass as true. (Multiple Zip files)
```
  

* Decompress()
<br/>
=> Parameters : System.IO.FileInfo fileToDecompress, string newFileName = null
<br/>
=> Return: void
```
Decompress *.zip file by passing FileInfo
If you pass newFileName, the zip file extracted on this path,
otherwise it (if you pass newFileName as null, default value) the zip file extracted on the parent folder
```

## Auxiliary IpAddress
* IsInRange()
<br/>
=> Parameters : IPAddress ip, IPAddress lowerRange, IPAddress upperRange
<br/>
=> Return: bool
```
Return true, if ip is in the range of lowerRange and upperRange
```
  
## Auxiliary StringExtensions
