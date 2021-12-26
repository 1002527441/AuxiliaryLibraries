![image](https://github.com/amin8zamany/AuxiliaryLibraries/raw/master/AuxiliaryLibraries_Logo.png)

## Table of Contents
* About The Project
* Auxiliary Calendar
* Auxiliary DirectoryFileHelper
* Auxiliary Encryption (RC4, AES, RSA)
* Auxiliary Http
* Auxiliary Object Copier
* Auxiliary Rest Api
* Auxiliary Zip
* Auxiliary IP Address
* Auxiliary String Extensions

## Get Started
	
### About AuxiliaryLibraries
This library helps you to get rid of some repetitive code blocks about date-times, IP, Rest, JSON, encryption, files, directories, queue, zip, objects, and string extensions.
This is a good helper, and saves lots of time, especially for Iranian programmers. Project URL : https://www.nuget.org/packages/AuxiliaryLibraries/
	
### Auxiliary Calendar
* ToPrettyDate()
=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true
=> Return: string

```bash
var persianDate = DateTime.Now.ToPrettyDate();
```

persianDate : "امروز ساعت 10:57"

 set isUtc as true

```bash
var persianDate2 = DateTime.UtcNow.ToPrettyDate(true);
```

persianDate2 : "امروز ساعت 10:57"

 set toPersian as false will return datetime in english

```bash
var persianDate3 = DateTime.UtcNow.ToPrettyDate(toPersian:false);
```

persianDate3 : "Today at 10:57"

* ToPrettyTime()

=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true

=> Return: string

```bash
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyTime();
```

persianDate : "10 دقیقه پیش"

 set isUtc as true

```bash
var persianDate = DateTime.UtcNow.AddDays(-15).ToPrettyTime(true);
```

persianDate2 : "2 هفته پیش"

 set toPersian as false will return datetime in english

```bash
var persianDate3 = DateTime.UtcNow.AddMonths(-3).ToPrettyTime(toPersian:false);
```

persianDate3 : "3 months ago"

* ToPrettyDateTime()

=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true

=> Return: string

```bash
var persianDate = DateTime.Now.AddMinutes(-10).ToPrettyDateTime();
```

persianDate : "هجدهم آذر ماه 1395 ساعت 14:30"

 set isUtc as true
 set toPersian as false will return datetime in english

```bash
var persianDate2 = DateTime.UtcNow.AddDays(-15).ToPrettyDateTime(isUtc:true, toPersian:false);
```

persianDate2 : "Ninth of December 2016 at 14:30"


* ToPrettyDay()

=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true

=> Return: string

```bash
var persianDate1 = DateTime.Now.ToPrettyDay();
```

persianDate1 : "امروز"

```bash
var persianDate2 = DateTime.Now.AddMinutes(-1).ToPrettyDay();
```

persianDate2 : "دیروز"

```bash
var persianDate3 = DateTime.Now.AddMinutes(1).ToPrettyDay();
```

persianDate3 : "فردا"

```bash
var persianDate4 = DateTime.Now.AddMinutes(-35).ToPrettyDay();
```

persianDate4 : "5 هفته پیش"

* ToPrettyDayOfWeek()

=> Parameters : DateTime dateTime, bool isUtc = false, bool toPersian = true, bool dayNumber = false

=> Return: string

```bash
var persianDate1 = DateTime.Now.ToPrettyDayOfWeek();
```

persianDate1 : "امروز"

```bash
var persianDate2 = DateTime.Now.AddMinutes(-1).ToPrettyDayOfWeek();
```

persianDate2 : "دیروز"

```bash
var persianDate3 = DateTime.Now.AddMinutes(+10).ToPrettyDayOfWeek();
```

persianDate3 : "جمعه"

```bash
var persianDate4 = DateTime.Now.AddMinutes(-5).ToPrettyDayOfWeek();
```

persianDate4 : "شنبه"


* ToPersianDateTime()

=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"

=> Return: string

```bash
var persianDate = DateTime.Now.ToPersianDateTime();
```

persianDate : "1395/9/30 ساعت 10:57"

```bash
var persianDate2 = DateTime.Now.ToPersianDateTime(false, "-");
```

persianDate2 : "1395-9-30 ساعت 10:57"

UTC:

```bash
var persianDate = DateTime.UtcNow.ToPersianDateTime(true);
```

persianDate : "1395/9/30 ساعت 10:57"

```bash
var persianDate2 = DateTime.UtcNow.ToPersianDateTime(true, "-");
```

persianDate2 : "1395-9-30 ساعت 10:57"

* ToPersianDate()

=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"

=> Return: string

```bash
var persianDate = DateTime.Now.ToPersianDate();
```

persianDate : "1395/09/30"

```bash
var persianDate2 = DateTime.Now.ToPersianDate(false, "-");
```

persianDate2 : "1395-9-30"

UTC:

```bash
var persianDate = DateTime.UtcNow.ToPersianDate(true);
```

persianDate : "1395/09/30"

```bash
var persianDate2 = DateTime.UtcNow.ToPersianDate(true, "-");
```

persianDate2 : "1395-9-30"


* ToPersianFullDateTime()

=> Parameters : DateTime dateTime, bool isUtc = false, string delimiter = "/"

=> Return: string

```bash
var persianDate = DateTime.Now.ToPersianFullDateTime();
```

persianDate : "1395/9/30 10:57:23:547"

```bash
var persianDate2 = DateTime.Now.ToPersianFullDateTime(false, "-");
```

persianDate2 : "1395-9-30 10:57:23:547"

UTC:

```bash
var persianDate = DateTime.UtcNow.ToPersianFullDateTime(true);
```

persianDate : "1395/09/30 10:57:23:547"

```bash
var persianDate2 = DateTime.UtcNow.ToPersianFullDateTime(true, "-");
```

persianDate2 : "1395-9-30 10:57:23:547"


* BeginDate()

```bash
var midnight = DateTime.Now.BeginDate();
```

midnight : 12/20/2016 00:00:00


* EndDate()

```bash
var tommorowMidnight = DateTime.Now.EndDate();
```

12/20/2016 23:59:59


* ConvertFromUTC()

=> Parameters : DateTime dateTime, TimeZoneInfo destinationTimeZone

=> Return: DateTime

```bash
var date = DateTime.UtcNow.ConvertFromUTC(TimeZoneInfo.FindSystemTimeZoneById(AuxiliaryCalendar.IranianTimeZone));
```

date : "2016/9/30 10:57:23:547"


* DayOfWeek()

=> Parameters : DateTime dateTime, bool toPersian = true, bool dayNumber = false

=> Return: string

```bash
var persianDate = DateTime.Now.DayOfWeek();
```

persianDate : " چهارشنبه"

```bash
var persianDate2 = DateTime.Now.DayOfWeek(toPersian:false);
```

persianDate2 : "Wednesday"

```bash
var persianDate3 = DateTime.Now.DayOfWeek(dayNumber:true);
```

persianDate3 : "4 شنبه"


* DayOfMonth()

=> Parameters : int day, bool isPersian

=> Return: string

```bash
var persianDate = AuxiliaryCalendar.DayOfMonth(20);
```

persianDate : " بیستم"



* Month()

=> Parameters : int day, bool isPersian

=> Return: string

```bash
var persianDate = AuxiliaryCalendar.Month(9);
```

persianDate : " آذر"



* GetFirstDayOfThisMonth()

=> Parameters : bool isPersian = true

=> Return: DateTime

Get the first day of current persian month

```bash
var date = AuxiliaryCalendar.GetFirstDayOfThisMonth();
```

date : "2016-06-18"

Get the first day of current miladi month

```bash
var date = AuxiliaryCalendar.GetFirstDayOfThisMonth(isPersian:false);
```

date : "2016-06-01"


* GetFirstDayOfThisWeek()

=> Parameters : bool isPersian = true

=> Return: DateTime

Get the first day of current persian week

```bash
var date = AuxiliaryCalendar.GetFirstDayOfThisWeek();
```

date : "2016-06-20"

Get the first day of current miladi week

```bash
var date = AuxiliaryCalendar.GetFirstDayOfThisWeek(isPersian:false);
//date : "2016-06-22"
```

* GetNextNearestWeekday()
Get Next Nearest day of Week, For example, get next nearest Sunday from Now

=> Parameters : DateTime start, DayOfWeek day
=> Return: DateTime

```bash
var date = DateTime.Now.GetNextNearestWeekday(DayOfWeek.Sunday);
//date : "2016-06-29"
```

* GetFirstDayOfLastXDay()
Get First Day Of Last X-Day
It will give you first nearest xDay from begining of month.

=> Parameters : int xDay, bool isPersian = true
=> Return: DateTime

```bash
//xDay = 7, Today = 23 => it will return : 21

//xDay = 15, Today = 23 => it will return : 15

//Be careful xDay Must be less than 30
```


### Auxiliary DirectoryFileHelper

* CreateFolderIfNeeded()
Create a folder if it doesn't exist

=> Parameters : string path
=> Return: bool

```bash
AuxiliaryDirectoryFileHelper.CreateFolderIfNeeded("C:/My Folder");
```


* CopyFolder()
Copy a Folder (including every files and sub folders in it) from 'sourcePath' to 'destinationPath'

=> Parameters : string sourcePath, string destinationPath
=> Return: bool

```bash
AuxiliaryDirectoryFileHelper.CopyFolder("C:/My Folder1", "C:/My Folder2");
```


* CopyFile()
Copy a File from 'sourcePath' to 'destinationPath'

=> Parameters : string sourcePath, string destinationPath
=> Return: bool

```bash
AuxiliaryDirectoryFileHelper.CopyFile("C:/My Folder1/image.jpg", "C:/My Folder2/image.jpg");
```

* Download()
Download a file by its 'url' and save it to 'destination'

=> Parameters : string url, string destination
=> Return: bool

```bash
AuxiliaryDirectoryFileHelper.Download("https://google.com/1.jpg", "C:/My Folder1/image.jpg");
```

* IsImage()
Undrasting this file is image or not

=> Parameters : string contentType
=> Return: bool

```bash
AuxiliaryDirectoryFileHelper.IsImage("C:/My Folder1", "C:/My Folder2");
```

* GetMimeTypeFromFilePath()
Get Mime Type From File Path

=> Parameters : string filePath
=> Return: string

```bash
AuxiliaryDirectoryFileHelper.GetMimeTypeFromFilePath("C:/My Folder1/image.jpg");
```

* GetMimeTypeFromFilePath()
Get Mime Type From File Format

=> Parameters : string format
=> Return: string

```bash
AuxiliaryDirectoryFileHelper.GetMimeTypeFromFilePath(".jpg");
```

### Auxiliary Encription


#### RC4

* Encrypt()
Encrypt your data with key

=> Parameters : string key, string data, Encoding encoding, bool skipZero = false
=> Return: string

```bash
var encrypted = AuxiliaryEncryption.RC4.Encrypt("KEY", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012");
```

* Encrypt()
Encrypt your data with key and encoding

=> Parameters : string key, string data
=> Return: string

```bash
var encrypted = AuxiliaryEncryption.RC4.Encrypt("KEY", "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we are encoding 6 bytes, or 48 bits, of data. According to the equation, we expect the output length to be (6 bytes / 3 bytes) * 4 characters = 8 characters .Nov 14, 2012", Encoding.UTF8);
```


* Decrypt()
Decrypt your data with key

=> Parameters : string key, string data, Encoding encoding, bool skipZero = false
=> Return: string

```bash
var decrypted = AuxiliaryEncryption.RC4.Decrypt("KEY", encrypted);
```

* Decrypt()
Decrypt your data with key and encoding

=> Parameters : string key, string data
=> Return: string

```bash
var decrypted = AuxiliaryEncryption.RC4.Decrypt("KEY", encrypted, Encoding.UTF8);
```

#### AES

* EncryptFile()
Encrypt your file, config of encyption is inside the constructor

=> Parameters : string inputFile, string outputFile
=> Return: string

```bash
var key = "4352821A-787A-4982-9A41-B1EB003BE9A1";
var aes = new AuxiliaryEncryption.AES(key, System.Text.Encoding.UTF8);
aes.EncryptFile(@"C:\Test.jpg", @"C:\Test_EncryptFile.jpg");
```

* Encrypt()
Encrypt your data

=> Parameters : string strtoencrypt
=> Return: byte[]

```bash
var key = "4352821A-787A-4982-9A41-B1EB003BE9A1";
var aes = new AuxiliaryEncryption.AES(key, System.Text.Encoding.UTF8);
var encrypted = aes.Encrypt("Some Text");
```

* DecryptFile()
Decrypt your file, , config of decryption is inside the constructor

=> Parameters : string inputFile, string outputFile
=> Return: string

```bash
var key = "4352821A-787A-4982-9A41-B1EB003BE9A1";
var aes = new AuxiliaryEncryption.AES(key, System.Text.Encoding.UTF8);
aes.EncryptFile(@"C:\Test.jpg", @"C:\Test_EncryptFile.jpg");
aes.DecryptFile(@"C:\Test_EncryptFile.jpg", @"C:\Test_DecryptFile.jpg");
```

* Decrypt()
Decrypt your data

=> Parameters : string strtoencrypt
=> Return: string

```bash
var key = "4352821A-787A-4982-9A41-B1EB003BE9A1";
var aes = new AuxiliaryEncryption.AES(key, System.Text.Encoding.UTF8);
var encrypted = aes.Encrypt("Some Text");
var decrypted = aes.Decrypt(encrypted);
```

#### RSA

*Initialize
RSA Encryption
```bash
//By creating an instance, public key and private key automatically will be generated by default
var rsa = new AuxiliaryEncryption.RSA(2048);

//You can use public key and private key and save them as *.xml or *.pem file by using functions SavePublicKeyToXmlFile, SavePublicKeyToPemFile,SavePrivateKeyToXmlFile, and SavePrivateKeyToPemFile
//XML File
rsa.SavePrivateKeyToXmlFile(@"C:\PrivateKey.xml");
rsa.SavePublicKeyToXmlFile(@"C:\PublicKey.xml");

//PEM File
rsa.SavePrivateKeyToPemFile(@"C:\PrivateKey.pem");
rsa.SavePublicKeyToPemFile(@"C:\PublicKey.pem");


//You can also replace public key and private key by your own public key and private key by using SetPublicKey and SetPrivateKey (pass the path of *.xml or *.pem file)
//XML File
rsa.SetPublicKey(@"C:\PrivateKey.xml");
rsa.SetPrivateKey(@"C:\PublicKey.xml");

//PEM File
rsa.SetPublicKey(@"C:\PrivateKey.pem");
rsa.SetPrivateKey(@"C:\PublicKey.pem");
```

* Encrypt()
Encrypt your data, config (Sign, PrivateKey, PublicKey) of encyption is inside the constructor

=> Parameters : string plainText
=> Return: string

```bash
var txt = "Keeping in mind that every every character of that string is 1 byte, or 8 bits, in size (assuming ASCII/UTF8 encoding), we";
var encrypted = rsa.Encrypt(txt);
```


* Decrypt()
Decrypt your data, , config (Sign, PrivateKey, PublicKey) of decryption is inside the constructor

=> Parameters : string inputFile, string outputFile
=> Return: string

```bash
var decrypted = rsa.Decrypt(encrypted);
```

### Auxiliary Http

* Read()
Fetch Requests (HttpRequestMessage) as List of object

=> Parameters : System.Net.Http.HttpRequestMessage Request
=> Return: System.Collections.Generic.List < object >

```bash
var objectList = Request.Read()
```


* IsMobileRequest()
Check the user request sent from mobile or not

=> Parameters : NONE
=> Return: bool

```bash
if(AuxiliaryHttp.IsMobileRequest())
{
    //Request is sent by a Mobile Device not Desktop
}
```

### Auxiliary Object Copier

* Clone()
Perform a deep copy of the object via serialization.
Be careful, The type must be serializable.

=> Parameters : T source
=> Return: T

```bash
var person = new Person();
var clonedPerson = person.Clone();
```


### Auxiliary RestApi

* Send()
Send your request with RestSharp

=> Parameters : string baseUrl, string functionName, Method method, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string userName, string password
=> Return: IRestResponse

```bash
IDictionary<string, object> body = new Dictionary<string, object>();
body.Add("action", action);
body.Add("CellNumber", phoneNumber);

IDictionary<string, string> header = new Dictionary<string, string>();
header.Add("content-type", "application/json;charset=UTF-8");

string output = AuxiliaryLibraries.AuxiliaryRestApi.Send(url, header, body, "POST");
var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(output);
```

* Send()
Send your request with RestSharp

=> Parameters : string baseUrl, string functionName, Method method, IDictionary<string, string> headers, object body, string userName, string password
=> Return: IRestResponse

```bash
IDictionary<string, object> body = new Dictionary<string, object>();
body.Add("action", action);
body.Add("CellNumber", phoneNumber);

IDictionary<string, string> header = new Dictionary<string, string>();
header.Add("content-type", "application/json;charset=UTF-8");

string output = AuxiliaryLibraries.AuxiliaryRestApi.Send(url, header, body, "POST");
var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(output);
```

* Send()
Send your request with WebClient

=> Parameters : string url, IDictionary<string, string> headers, IDictionary<string, object> parametersBody, string method = "GET", bool contentLength = true
=> Return: string

```bash
IDictionary<string, object> body = new Dictionary<string, object>();
body.Add("action", action);
body.Add("CellNumber", phoneNumber);

IDictionary<string, string> header = new Dictionary<string, string>();
header.Add("content-type", "application/json;charset=UTF-8");

string output = AuxiliaryLibraries.AuxiliaryRestApi.Send(url, header, body, "POST");
var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(output);
```

### AuxiliaryZip

* Compress()
Create a ZIP file of the files provided.

=> Parameters : IEnumerable< string > fileNames, string destinationFileName
=> Return: void


* Compress()
Compress entire folder by passing directory Path

=> Parameters : string directoryPath
=> Return: void


* Decompress()
Decompress *.zip file, if pathIsDirectory pass as false. (One Zip file)
Decompress every *.zip files inside filder by passing directory Path, if pathIsDirectory pass as true. (Multiple Zip files)

=> Parameters : string path, bool pathIsDirectory = false
=> Return: void
  

* Decompress()
Decompress *.zip file by passing FileInfo
If you pass newFileName, the zip file extracted on this path,
otherwise it (if you pass newFileName as null, default value) the zip file extracted on the parent folder

=> Parameters : System.IO.FileInfo fileToDecompress, string newFileName = null
=> Return: void


### Auxiliary IpAddress

* IsInRange()
Return true, if ip is in the range of lowerRange and upperRange

=> Parameters : IPAddress ip, IPAddress lowerRange, IPAddress upperRange
=> Return: bool


### Auxiliary StringExtensions

