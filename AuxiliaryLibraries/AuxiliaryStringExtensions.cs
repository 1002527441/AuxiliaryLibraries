﻿using AuxiliaryLibraries.Enums;
using AuxiliaryLibraries.Extentions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Persian and arabic characters, numbers, currency.
    /// Email, cellnumber and nationalId validation
    /// Verify or generate words as password(MD5)
    /// </summary>
    public static class AuxiliaryStringExtensions
    {
        /// <summary>
        /// Persian alphabet
        /// </summary>
        public static string FA_CHARS = "ابپتثجچحخدذرزژسصضطظعغکگلمنوهیء";

        /// <summary>
        /// If the term be English, it will return True
        /// </summary>
        /// <param name="term">Considered word</param>
        /// <returns>bool</returns>
        public static bool IsEnglish(this string term)
        {
            if (string.IsNullOrEmpty(term))
                return false;
            foreach (var ch in term)
            {
                if (FA_CHARS.Contains(ch))
                    return false;
            }
            return true;

        }

        /// <summary>
        /// Take chunk of text about 'length' characters
        /// </summary>
        /// <param name="text">Considered word</param>
        /// <param name="length">Considered length</param>
        /// <returns>string</returns>
        public static string TakeChunk(this string text, int length)
        {
            if (text == null || text.Length <= length)
                return text;
            var index = text.Substring(0, length).LastIndexOfAny(new[] { '\n', ' ' });
            return string.Format("{0}...", text.Substring(0, index + 1));
        }

        /// <summary>
        /// Fill replacementDic inside template, this function is used especially for creating email template 
        /// </summary>
        /// <param name="template">Email template, etc</param>
        /// <param name="replacementDic">Informations</param>
        /// <returns>string</returns>
        public static string FillTemplate(this string template, Dictionary<string, string> replacementDic)
        {
            foreach (var item in replacementDic)
            {
                template = template.Replace(item.Key.ToUpper(), item.Value);
            }
            return template;
        }

        /// <summary>
        /// Convert English numbers To Persian numbers
        /// </summary>
        /// <param name="number">Numbers as string</param>
        /// <returns>string</returns>
        public static string ToPersianNumber(this string number)
        {
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() == "en".ToLower())
                return Convert.ToString(number);
            string s = number.ToString();
            StringBuilder _sb = new StringBuilder();
            char[] _numbers = s.ToCharArray();
            foreach (var item in _numbers)
            {
                int _number = (int)item;
                if ((int)_number >= 48 && (int)_number <= 57)
                {
                    _number += 1728;
                    _sb.Append((char)_number);
                }
                else
                {
                    _sb.Append(item);
                }
            }
            return _sb.ToString();
        }

        /// <summary>
        /// Convert English numbers To Persian numbers
        /// </summary>
        /// <param name="number">Numbers as string</param>
        /// <param name="isMoney">Is numbers money</param>
        /// <returns>string</returns>
        public static string ToPersianNumber(int number, bool isMoney)
        {
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() == "en".ToLower())
                return Convert.ToString(number);
            string s = number.ToString();
            if (isMoney == true)
            {
                s = number.ToString("N0");
            }
            StringBuilder _sb = new StringBuilder();
            char[] _numbers = s.ToCharArray();
            foreach (var item in _numbers)
            {
                int _number = (int)item;
                if ((int)_number >= 48 && (int)_number <= 57)
                {
                    _number += 1728;
                    _sb.Append((char)_number);
                }
                else
                {
                    _sb.Append(item);
                }
            }
            return _sb.ToString();
        }

        /// <summary>
        /// Convert Persian number to English numbers
        /// </summary>
        /// <param name="number">Numbers as string</param>
        /// <returns>string</returmns>
        public static string ToEnglishNumber(this string number)
        {
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() == "en".ToLower())
                return Convert.ToString(number);
            string s = number.ToString();
            StringBuilder _sb = new StringBuilder();
            char[] _numbers = s.ToCharArray();
            foreach (var item in _numbers)
            {
                int _number = (int)item;
                if ((int)_number >= 1776 && (int)_number <= 1785)
                {
                    _number -= 1728;
                    _sb.Append((char)_number);
                }
                else
                {
                    _sb.Append(item);
                }
            }
            return _sb.ToString();
        }

        /// <summary>
        /// Replace all Arabic and English numbers to Persian numbers
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPersianNumbers(this string number)
        {
            //persinNum = [۰, ۱, ۲, ۳, ۴, ۵, ۶, ۷, ۸, ۹]
            if (string.IsNullOrEmpty(number))
                return string.Empty;
            return number.Replace("٠", "۰").Replace("١", "۱").Replace("٢", "۲").Replace("٣", "۳").Replace("٤", "۴")
                .Replace("٥", "۵").Replace("٦", "۶").Replace("٧", "۷").Replace("٨", "۸").Replace("٩", "۹")
                .Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴")
                .Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹").Trim();
        }

        /// <summary>
        /// Replace all Persian and English numbers to Arabic numbers
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToArabicNumbers(this string number)
        {
            //١٢٣٤٥٦٧٨٩٠
            if (string.IsNullOrEmpty(number))
                return string.Empty;
            return number.Replace("0", "٠").Replace("1", "١").Replace("2", "٢").Replace("3", "٣").Replace("4", "٤")
                .Replace("5", "٥").Replace("6", "٦").Replace("7", "٧").Replace("8", "٨").Replace("9", "٩")
                .Replace("۰", "٠").Replace("۱", "١").Replace("۲", "٢").Replace("۳", "٣").Replace("۴", "٤")
                .Replace("۵", "٥").Replace("۶", "٦").Replace("۷", "٧").Replace("۸", "٨").Replace("۹", "٩").Trim();
        }

        /// <summary>
        /// Replace all Persian and Arabic numbers to English numbers
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToEnglishNumbers(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return string.Empty;
            return number.Replace("٠", "0").Replace("١", "1").Replace("٢", "2").Replace("٣", "3").Replace("٤", "4")
                .Replace("٥", "5").Replace("٦", "6").Replace("٧", "7").Replace("٨", "8").Replace("٩", "9")
                .Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4")
                .Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9").Trim();
        }

        /// <summary>
        /// Normalize mobile number as Iranian mobile number (989*********).
        /// You can pass '+98', '98' or '0' on 'startWith'. Def
        /// If phone number is not valid, it will return number itself.
        /// </summary>
        /// <param name="number">Phone number</param>
        /// <param name="startWith">Phone number will be start with 'startWith' parameter</param>
        /// <returns>string</returns>
        public static string NormalizeMobileNumber(this string number, string startWith = "98")
        {
            var numberLength = 10;
            if (string.IsNullOrEmpty(number))
                return number;

            number = ToEnglishNumbers(number).Trim();
            string Number = string.Empty;

            if (!number.IsCellNumberValid())
                return Number;

            if (number.Length >= numberLength)
            {
                var dif = number.Length - numberLength;
                Number = $"{startWith}{number.Remove(0, dif)}";
            }

            return !string.IsNullOrEmpty(Number) ? Number.Trim() : Number;
        }

        /// <summary>
        /// Check validity of Iranian national identity 
        /// </summary>
        /// <param name="nationalId">national Identity</param>
        /// <returns>bool</returns>
        public static bool IsNationalIdValid(this string nationalId)
        {
            if (string.IsNullOrEmpty(nationalId))
                return false;
            Regex rgx = new Regex(RegexPatterns.NationalID);
            return rgx.IsMatch(nationalId);
        }

        /// <summary>
        /// Check validity of email format
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>bool</returns>
        public static bool IsEmailValid(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            Regex rgx = new Regex(RegexPatterns.Email);
            return rgx.IsMatch(email);
        }

        /// <summary>
        /// Check validity of iranian mobile number format
        /// </summary>
        /// <param name="number">Mobile number</param>
        /// <returns>bool</returns>
        public static bool IsCellNumberValid(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;
            return new Regex(RegexPatterns.MobileNumber).IsMatch(number);
        }

        /// <summary>
        /// Convert 'input' to MD5
        /// </summary>
        /// <param name="input">Everything</param>
        /// <returns>string</returns>
        public static string GetMd5(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                var hash = GetMd5Hash(md5Hash, input);

                return hash.ToUpper();
            }
        }

        /// <summary>
        /// Calculate MD5 hash from input
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>string</returns>
        public static string CalculateMD5Hash(this string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        /// <summary>
        /// Verify Md5 Hash
        /// </summary>
        /// <param name="md5Hash">Md5 Hash</param>
        /// <param name="input">input</param>
        /// <param name="hash">hash</param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            return 0 == StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash);
        }

        /// <summary>
        /// If yor number is less than 10, it will put a zero before it.
        /// For example if the number is 1, it will return "01", ...
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToTowDigits(this int number)
        {
            return number < 10 ? $"0{number}" : number.ToString();
        }

        /// <summary>
        /// Verift the password with hash
        /// Be careful to pass password as first parameter and hash as the second.
        /// type contains the encryption type, and the default value is SHA1
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool VerifyPassword(this string password, string hash, EncryptType type = EncryptType.SHA1)
        {
            switch (type)
            {
                //case EncryptType.BCrypt:
                //    {
                //        return BCrypt.Net.BCrypt.Verify(password, hash);
                //    }
                case EncryptType.SHA1:
                    {
                        return password.EncriptSHA1().Equals(hash);
                    }
            }
            return false;
        }

        /// <summary>
        /// Convert text to hash
        /// type contains the encryption type, and the default value is SHA1
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Hash(this string text, EncryptType type = EncryptType.SHA1)
        {
            switch (type)
            {
                //case EncryptType.BCrypt:
                //    {
                //        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());//"$2a$10$9ldyfl0iAogMsGQiI490dO"
                //    }
                case EncryptType.SHA1:
                    {
                        return text.EncriptSHA1();
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert price to string and Seperate it by separator
        /// </summary>
        /// <param name="price"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToMoney(this int price, string separator = ".")
        {
            return ((long)price).ToMoney(separator);
        }

        /// <summary>
        /// Convert price to string and Seperate it by separator
        /// </summary>
        /// <param name="price"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToMoney(this long price, string separator = ".")
        {
            return price.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = separator
            });
        }

        /// <summary>
        ///  Convert price to string and Seperate it by separator and put 'تومان' after it
        ///  If pass isRial as true, divide price to 10 first.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="isRial"></param>
        /// <returns></returns>
        public static string ToToman(this int price, bool isRial = true)
        {
            return ((long)price).ToMoney();
        }

        /// <summary>
        /// Convert price to string and Seperate it by separator and put 'تومان' after it
        /// If pass isRial as true, divide price to 10 first. And the default value is true
        /// </summary>
        /// <param name="price"></param>
        /// <param name="isRial"></param>
        /// <returns></returns>
        public static string ToToman(this long price, bool isRial = true)
        {
            if (price <= 0)
                return string.Format("{0} تومان", price);

            if (isRial)
                price /= 10;

            return string.Format("{0} تومان", (object)price.ToMoney());
        }

        /// <summary>
        /// Convert price to string and Seperate it by separator and put 'ريال' after it
        /// If pass isToman as true, multiplication price by 10 first. And the default value is true
        /// </summary>
        /// <param name="price"></param>
        /// <param name="isToman"></param>
        /// <returns></returns>
        public static string ToRial(this int price, bool isToman = true)
        {
            return ((long)price).ToRial();
        }

        /// <summary>
        /// Convert price to string and Seperate it by separator and put 'ريال' after it
        /// If pass isToman as true, multiplication price by 10 first. And the default value is true
        /// </summary>
        /// <param name="price"></param>
        /// <param name="isToman"></param>
        /// <returns></returns>
        public static string ToRial(this long price, bool isToman = true)
        {
            if (price <= 0)
                return string.Format("{0} ريال", price);

            if (isToman)
                price *= 10;

            return string.Format("{0} ريال", price.ToMoney());
        }

        /// <summary>
        /// This function extracts from the text just digits.
        /// If you pass text something like that = "salam 123 o4k5", it will return just 12345 as int
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ToInt32(this string text)
        {
            var digitContent = string.Join("", text.ToCharArray().Where(Char.IsDigit));
            int result = 0;
            if (int.TryParse(digitContent, out result))
                return result;
            return -1;
        }

        /// <summary>
        /// Convert a persian date with format '1397/1/1' TO '1397/01/01'.
        /// Be Careful that the year must be 4 characters.
        /// </summary>
        /// <param name="date">Contains the persian date</param>
        /// <param name="separator">The separator of year, month and day. The default value is '/'.</param>
        /// <returns></returns>
        public static string NormalizePersianDate(this string date, char separator = '/')
        {
            if (string.IsNullOrEmpty(date))
                return string.Empty;
            var result = string.Empty;
            var parts = date.Split(separator);
            if (parts.Length != 3)
                return string.Empty;
            var firstPart = true;
            foreach (var part in parts)
            {
                if (firstPart && part.Length != 4)
                    return string.Empty;
                else if (firstPart)
                    result = part;

                if (!firstPart && (part.Length > 2 || part.Length < 1))
                    return string.Empty;
                else if (!firstPart)
                {
                    result += part.Length == 2 ? part : string.Format($"{separator}0{0}", part);
                }
                firstPart = false;
            }
            return result;
        }

        /// <summary>
        /// Reverse the term
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public static string Reverse(this string term)
        {
            char[] arr = term.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        /// <summary>
        /// Return mimeType, format, basePath and name of the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="format"></param>
        /// <param name="basePath"></param>
        /// <param name="nameOfFile"></param>
        /// <returns></returns>
        public static string AnalyseFileName(this string fileName, out string format, out string basePath, out string nameOfFile)
        {
            format = fileName.Substring(fileName.LastIndexOf('.') + 1);
            basePath = fileName.Substring(0, fileName.LastIndexOf('\\'));
            nameOfFile = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            string mimeType = "";
            switch (format)
            {
                case "jpg":
                    {
                        mimeType = "image/jpeg";
                    }
                    break;
                case "jpeg":
                    {
                        mimeType = "image/jpeg";
                    }
                    break;
                case "gif":
                    {
                        mimeType = "image/gif";
                    }
                    break;
                case "ico":
                    {
                        mimeType = "image/x-icon";
                    }
                    break;
                case "png":
                    {
                        mimeType = "image/png";
                    }
                    break;
                case "bmp":
                    {
                        mimeType = "image/bmp";
                    }
                    break;
                case "tif":
                    {
                        mimeType = "image/tiff";
                    }
                    break;
                case "tiff":
                    {
                        mimeType = "image/tiff";
                    }
                    break;
            }
            return mimeType;
        }

        /// <summary>
        /// Replace Arabic characters to Persian
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ReplaceArabicChar(this string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;

            string strName = name.Replace("ي", "ی").Replace("ك", "ک").Replace("ة", "ه").Replace("ئ", "ئ").Replace("ه", "ه");
            strName = ToPersianNumbers(strName);

            strName = strName.Trim();

            //strName = String.Join(" ", strName.Split(new char[] { ' ' },
            //StringSplitOptions.RemoveEmptyEntries));

            return strName;
        }

        /// <summary>
        /// Encript term as SHA1
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public static string EncriptSHA1(this string term)
        {
            StringBuilder Encript = new StringBuilder();
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(term));
            foreach (byte n in hash) Encript.Append(Convert.ToInt32(n + 256).ToString("x2"));
            return Encript.ToString();
        }

        /// <summary>
        /// Encript term as SHA1
        /// </summary>
        /// <param name="term"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncriptSHA1(this string term, Encoding encoding)
        {
            StringBuilder Encript = new StringBuilder();
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            byte[] hash = sha1.ComputeHash(encoding.GetBytes(term));
            foreach (byte n in hash) Encript.Append(Convert.ToInt32(n + 256).ToString("x2"));
            return Encript.ToString();
        }

        /// <summary>
        /// Convert date to solr date format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string SolrDate(this DateTime date)
        {
            return string.Format("{0}-{1}-{2}T{3}:{4}:{5}.{6}Z", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        /// <summary>
        /// Retuen current what percent maximum as double
        /// </summary>
        /// <param name="current"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static double Percent(this long current, long maximum)
        {
            return Math.Round(((double)current / (double)maximum) * 100, 2);
        }

        /// <summary>
        /// Decode term as html encoder
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public static string HtmlDecoder(this string term)
        {
            return HttpUtility.UrlDecode(HttpUtility.HtmlDecode(term));
        }

        /// <summary>
        /// If you pass "2,3,5,6,8,2,6,1" return a list of long which is contains { 2,3,5,6,8,2,6,1 }
        /// delimiter can be any character
        /// </summary>
        /// <param name="term"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<long> ToListLong(this string term, char delimiter = ',')
        {
            var result = new List<long>();
            var strList = term.Split(delimiter).ToList();
            foreach (var item in strList)
            {
                long element = 0;
                long.TryParse(item, out element);
                if (element > 0)
                    result.Add(element);
            }
            return result;
        }

        /// <summary>
        /// If you pass "2,3,5,6,8,2,6,1" return a list of int which is contains { 2,3,5,6,8,2,6,1 }
        /// delimiter can be any character
        /// </summary>
        /// <param name="term"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<int> ToListInt(this string term, char delimiter = ',')
        {
            var result = new List<int>();
            var strList = term.Split(delimiter).ToList();
            foreach (var item in strList)
            {
                int element = 0;
                int.TryParse(item, out element);
                if (element > 0)
                    result.Add(element);
            }
            return result;
        }

        /// <summary>
        /// Encode plainText to Base64String
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Encode(this string plainText, Encoding encoding)
        {
            try
            {
                var plainTextBytes = encoding.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decode base64EncodedData from Base64String
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(this string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Encode plainText to ASCII
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string AsciiEncode(this string plainText)
        {
            try
            {
                var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
                return string.Join(";", plainTextBytes);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decode asciiEncodedData from ASCII
        /// </summary>
        /// <param name="asciiEncodedData"></param>
        /// <returns></returns>
        public static string AsciiDecode(this string asciiEncodedData)
        {
            try
            {
                var list = asciiEncodedData.Split(';').ToList();
                var array = list.Select(x => Convert.ToByte(x)).ToArray();
                return Encoding.ASCII.GetString(array);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// This function splits text into different parts separately.
        /// For example, plainText is 'AuxiliaryLibraries'. If you set partLength to 4, it splits to ["Auxi", "liar", "yLib", "ibra", "arie", "s"].
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="partLength"></param>
        /// <returns></returns>
        public static IEnumerable<String> SplitInParts(this String plainText, Int32 partLength)
        {
            if (plainText == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < plainText.Length; i += partLength)
                yield return plainText.Substring(i, Math.Min(partLength, plainText.Length - i));
        }
    }
}
