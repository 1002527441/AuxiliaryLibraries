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
    /// <summary>
    /// Persian and arabic characters, numbers, currency.
    /// Email, cellnumber and nationalId validation
    /// Verify or generate words as password(MD5)
    /// </summary>
    public static class StringExtensions
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
        /// <returns>string</returns>
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

        /// <summary>
        /// Normalize mobile number as Iranian mobile number (989*********).
        /// You can pass '+98', '98' or '0' on 'startWith'. Def
        /// If phone number is not valid, it will return string.Empty.
        /// </summary>
        /// <param name="number">Phone number</param>
        /// <param name="startWith">Phone number will be start with 'startWith' parameter</param>
        /// <returns>string</returns>
        public static string NormalizeMobileNumber(this string number, string startWith = "98")
        {
            var numberLength = 10;
            if (string.IsNullOrEmpty(number))
            {
                return string.Empty;
            }
            number = ReplacePersianNumbers(number).Trim();
            string Number = string.Empty;

            if (!number.IsCellNumberValid())
            {
                return Number;
            }

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
        public static string ToMoney(this int price, string separator = ".")
        {
            return price.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = separator
            });
        }
        public static string ToMoney(this long price, string separator = ".")
        {
            return price.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = separator
            });
        }
        public static string ToToman(this int price)
        {
            if (price <= 0)
                return string.Format("{0} تومان", price);
            price /= 10;
            return string.Format("{0} تومان", price.ToMoney());
        }
        public static string ToToman(this long price)
        {
            if (price <= 0)
                return string.Format("{0} تومان", price);
            price /= 10;
            return string.Format("{0} تومان", (object)price.ToMoney());
        }
        public static string ToRial(this int price)
        {
            if (price <= 0)
                return string.Format("{0} تومان", price);
            return string.Format("{0} تومان", price.ToMoney());
        }
        public static string ToRial(this long price)
        {
            if (price <= 0)
                return string.Format("{0} ريال", price);
            return string.Format("{0} ريال", price.ToMoney());
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
