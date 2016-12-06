using AuxiliaryLibraries.Enums;
using AuxiliaryLibraries.Extentions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AuxiliaryLibraries
{
    public static class StringExtensions
    {
        public static string FA_CHARS = "ابپتثجچحخدذرزژسصضطظعغکگلمنوهیء";
        public static bool IsEnglish(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            foreach (var ch in str)
            {
                if (FA_CHARS.Contains(ch))
                    return false;
            }
            return true;

        }
        public static string TakeChunk(this string text, int length)
        {
            if (text == null || text.Length <= length)
                return text;
            var index = text.Substring(0, length).LastIndexOfAny(new[] { '\n', ' ' });
            return string.Format("{0}...", text.Substring(0, index + 1));
        }
        public static string FillTemplate(this string template, Dictionary<string, string> replacementDic)
        {
            foreach (var item in replacementDic)
            {
                template = template.Replace(item.Key.ToUpper(), item.Value);
            }
            return template;
        }
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
        public static string ToEnglishNumbers(this string number)
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
        public static string ConvertToPersian(int number, bool isMoney)
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
        public static string NormalizeMobileNumber(this string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return string.Empty;
            }
            number = ReplacePersianNumbers(number);
            string Number = string.Empty;

            if (!number.IsCellNumberValid())
            {
                return null;
            }

            if (number.Length == 10)
            {
                //9126765172
                Number = "98" + number;
            }
            else if (number.Length == 11)
            {
                //09126765172
                Number = "98" + number.Remove(0, 1);
            }
            else if (number.Length == 12)
            {
                //989126765172
                Number = number;
            }
            else if (number.Length == 13)
            {
                //+989126765172
                Number = number.Remove(0, 1);
            }

            return !string.IsNullOrEmpty(Number) ? Number.Trim() : Number;
        }
        public static string NormalizeMobileNumberForSms(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return string.Empty;

            number = ReplacePersianNumbers(number);
            string Number = string.Empty;
            if (!number.IsCellNumberValid())
            {
                return null;
            }

            if (number.Length == 10)
            {
                //9126765172
                Number = "0" + number;
            }
            else if (number.Length == 11)
            {
                //09126765172
                Number = number;
            }
            else if (number.Length == 12)
            {
                //989126765172
                Number = "0" + number.Remove(0, 2);
            }
            else if (number.Length == 13)
            {
                //+989126765172
                Number = "0" + number.Remove(0, 3);
            }

            return Number;
        }
        public static bool IsNationalIdValid(this string nationalId)
        {
            if (string.IsNullOrEmpty(nationalId))
                return false;
            Regex rgx = new Regex(RegexPatterns.NationalID);
            return rgx.IsMatch(nationalId);
        }
        public static bool IsEmailValid(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            Regex rgx = new Regex(RegexPatterns.Email);
            return rgx.IsMatch(email);
        }
        public static bool IsCellNumberValid(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;
            return new Regex(RegexPatterns.MobileNumber).IsMatch(number);
        }
        public static string ToMd5(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                var hash = GetMd5Hash(md5Hash, input);

                return hash.ToUpper();
            }
        }
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
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        public static string Hash(this string password, EncryptType type = EncryptType.SHA1)
        {
            switch (type)
            {
                //case EncryptType.BCrypt:
                //    {
                //        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());//"$2a$10$9ldyfl0iAogMsGQiI490dO"
                //    }
                case EncryptType.SHA1:
                    {
                        return password.EncriptSHA1();
                    }
            }
            return string.Empty;
        }
        public static string Money(this int price, string separator = ".")
        {
            return price.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = separator
            });
        }
        public static string Money(this long price, string separator = ".")
        {
            return price.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = separator
            });
        }
        public static string CurrencyFormat(this int price)
        {
            if (price <= 0)
                return string.Format("{0} تومان", price);
            price /= 10;
            return string.Format("{0} تومان", price.Money());
        }
        public static string CurrencyFormat(this long price)
        {
            if (price <= 0)
                return string.Format("{0} تومان", price);
            price /= 10;
            return string.Format("{0} تومان", price.Money());
        }
        public static string CurrencyFormat(this int price, string currency)
        {
            if (string.IsNullOrEmpty(currency))
                return price.CurrencyFormat();
            if (price <= 0)
                return string.Format("{0} {1}", price, currency);
            price /= 10;
            return string.Format("{0} {1}", price.Money(), currency);
        }
        public static string CurrencyFormat(this long price, string currency)
        {
            if (string.IsNullOrEmpty(currency))
                return price.CurrencyFormat();
            if (price <= 0)
                return string.Format("{0} {1}", price, currency);
            price /= 10;
            return string.Format("{0} {1}", price.Money(), currency);
        }
        public static string ReplacePersianNumbers(this string text)
        {
            //persinNum = [۰, ۱, ۲, ۳, ۴, ۵, ۶, ۷, ۸, ۹]
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9").Trim();
        }
        public static string ReplaceArabicNumbersToPersian(this string text)
        {
            //١٢٣٤٥٦٧٨٩٠
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text.Replace("٠", "۰").Replace("١", "۱").Replace("٢", "۲").Replace("٣", "۳").Replace("٤", "۴")
                .Replace("٥", "۵").Replace("٦", "۶").Replace("٧", "۷").Replace("٨", "۸").Replace("٩", "۹").Trim();
        }
        public static string ReplaceArabicNumbers(this string text)
        {
            //١٢٣٤٥٦٧٨٩٠
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text.Replace("٠", "0").Replace("١", "1").Replace("٢", "2").Replace("٣", "3").Replace("٤", "4")
                .Replace("٥", "5").Replace("٦", "6").Replace("٧", "7").Replace("٨", "8").Replace("٩", "9").Trim();
        }
        public static string ReplaceToEnglishNumbers(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text.ReplacePersianNumbers().ReplaceArabicNumbers().Trim();
        }
        public static int ToInt32(this string text)
        {
            var digitContent = string.Join("", text.ToCharArray().Where(Char.IsDigit));
            int result = 0;
            if (int.TryParse(digitContent, out result))
                return result;
            return -1;
        }
        public static string NormalizePersianDate(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var result = string.Empty;
            var parts = text.Split('/');
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
                    result += part.Length == 2 ? part : string.Format("/0{0}", part);
                }
                firstPart = false;
            }
            return result;
        }
        public static string Reverse(this string term)
        {
            char[] arr = term.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string AnalyseFileName(this string FileName, out string format, out string basePath, out string fileName)
        {
            format = FileName.Substring(FileName.LastIndexOf('.') + 1);
            basePath = FileName.Substring(0, FileName.LastIndexOf('\\'));
            fileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
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
        public static string ReplaceArabicChar(this string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;

            string strName = name.Replace("ي", "ی").Replace("ك", "ک").Replace("ة", "ه").Replace("ئ", "ئ").Replace("ه", "ه");
            strName = ReplaceArabicNumbersToPersian(strName);

            strName = strName.Trim();

            //strName = String.Join(" ", strName.Split(new char[] { ' ' },
            //StringSplitOptions.RemoveEmptyEntries));

            return strName;
        }
        public static string EncriptSHA1(this string term)
        {
            StringBuilder Encript = new StringBuilder();
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(term));
            foreach (byte n in hash) Encript.Append(Convert.ToInt32(n + 256).ToString("x2"));
            return Encript.ToString();
        }
        public static string EncriptSHA1(this string term, Encoding encoding)
        {
            StringBuilder Encript = new StringBuilder();
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            byte[] hash = sha1.ComputeHash(encoding.GetBytes(term));
            foreach (byte n in hash) Encript.Append(Convert.ToInt32(n + 256).ToString("x2"));
            return Encript.ToString();
        }
        public static string SolrDate(this DateTime date)
        {
            return string.Format("{0}-{1}-{2}T{3}:{4}:{5}.{6}Z", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }
        internal static double Percent(this long current, long maximum)
        {
            return Math.Round(((double)current / (double)maximum) * 100, 2);
        }
    }
}
