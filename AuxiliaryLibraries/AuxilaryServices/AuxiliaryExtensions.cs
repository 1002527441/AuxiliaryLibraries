using AuxiliaryLibraries.Enums;
using AuxiliaryLibraries.Extentions;
using AuxiliaryLibraries.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Persian and arabic characters, numbers, currency.
    /// Email, cellnumber and nationalId validation
    /// Verify or generate words as password(MD5)
    /// </summary>
    public static class AuxiliaryExtensions
    {
        #region Constat Values
        /// <summary>
        /// Persian alphabet
        /// </summary>
        public static readonly string FA_CHARS = "ابپتثجچحخدذرزژسصضطظعغکگلمنوهیء";

        /// <summary>
        /// English Size Suffixes
        /// </summary>
        public static readonly string[] SizeSuffixes =
           { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// Persian Size Suffixes
        /// </summary>
        public static readonly string[] PersianSizeSuffixes =
                   { "بایت", "کیلوبایت", "مگابایت", "گیگابایت", "ترابایت", "پتابایت", "اگزابایت", "زتابایت", "یوتابایت" };
        #endregion

        #region Convert Letters To Letters
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
        public static string ToPersianNumber(this int number, bool isMoney)
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
        /// Replace Arabic characters to Persian
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToPersianLetters(this string name)
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
        /// Convert Number To Persian Letters
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPersianLetters(this int number)
        {
            return ((long)number).ToPersianLetters();
        }

        /// <summary>
        /// Convert Number To Persian Letters
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPersianLetters(this long number)
        {
            var _price = string.Empty;
            long hundreds = number >= 100 ? number / 100 : 0, tens = 0, units = 0;

            #region tens
            if (hundreds > 0)
                tens = (number % 100) >= 10 ? (number % 100) / 10 : 0;
            else
                tens = number >= 10 ? number / 10 : 0;
            #endregion

            #region units
            if (hundreds > 0)
            {
                if (tens > 0)
                    units = ((number % 100) % 10);
                else
                    units = (number % 100);
            }
            else
            {
                if (tens > 0)
                    units = (number % 10);
                else
                    units = number;
            }
            #endregion

            var lst = new List<string>();
            lst.Add(GetLetter(hundreds, NumberType.hundreds));
            if (tens == 1 && units > 0)
                lst.Add(GetLetter(Convert.ToInt32($"{tens}{units}"), NumberType.tens));
            else
            {
                lst.Add(GetLetter(tens, NumberType.tens));
                lst.Add(GetLetter(units, NumberType.units));
            }

            return string.Join(" و ", lst.Where(s => !string.IsNullOrEmpty(s)).ToList());
        }

        private static string GetLetter(this long number, NumberType numberType)
        {
            switch (number)
            {
                case 19:
                    return DisplayNames.Number_Nineteen;
                case 18:
                    return DisplayNames.Number_Eighteen;
                case 17:
                    return DisplayNames.Number_Seventeen;
                case 16:
                    return DisplayNames.Number_Sixteen;
                case 15:
                    return DisplayNames.Number_Fifteen;
                case 14:
                    return DisplayNames.Number_Fourteen;
                case 13:
                    return DisplayNames.Number_Thirteen;
                case 12:
                    return DisplayNames.Number_Twelve;
                case 11:
                    return DisplayNames.Number_Eleven;
                case 9:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_NineHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Ninety;
                            case NumberType.units:
                                return DisplayNames.Number_Nine;
                        }
                    }
                    break;
                case 8:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_EightHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Eighty;
                            case NumberType.units:
                                return DisplayNames.Number_Eight;
                        }
                    }
                    break;
                case 7:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_SevenHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Seventeen;
                            case NumberType.units:
                                return DisplayNames.Number_Seven;
                        }
                    }
                    break;
                case 6:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_SixHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Sixty;
                            case NumberType.units:
                                return DisplayNames.Number_Six;
                        }
                    }
                    break;
                case 5:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_FiveHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Fifty;
                            case NumberType.units:
                                return DisplayNames.Number_Five;
                        }
                    }
                    break;
                case 4:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_FourHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Fourty;
                            case NumberType.units:
                                return DisplayNames.Number_Four;
                        }
                    }
                    break;
                case 3:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_ThreeHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Thirty;
                            case NumberType.units:
                                return DisplayNames.Number_Three;
                        }
                    }
                    break;
                case 2:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_TwoHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Twenty;
                            case NumberType.units:
                                return DisplayNames.Number_Two;
                        }
                    }
                    break;
                case 1:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_OneHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Ten;
                            case NumberType.units:
                                return DisplayNames.Number_One;
                        }
                    }
                    break;
            }
            return string.Empty;
        }
        #endregion

        #region User Identity Information
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
            if (!AuxiliaryRegexPatterns.NationalID.IsMatch(nationalId))
                return false;

            var check = Convert.ToInt32(nationalId.Substring(9, 1));
            var sum = Enumerable.Range(0, 9)
                .Select(x => Convert.ToInt32(nationalId.Substring(x, 1)) * (10 - x))
                .Sum() % 11;

            return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
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
            return AuxiliaryRegexPatterns.Email.IsMatch(email);
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
            return AuxiliaryRegexPatterns.MobileNumber.IsMatch(number);
        }

        /// <summary>
        /// Check validity of iranian (Urban telephone) phone number format
        /// </summary>
        /// <param name="number">Mobile number</param>
        /// <returns>bool</returns>
        public static bool IsPhoneNumberValid(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;
            return AuxiliaryRegexPatterns.PhoneNumber.IsMatch(number);
        }

        /// <summary>
        /// Validate sheba number 
        /// </summary>
        /// <param name="sheba">should contain IR</param>
        public static bool IsShebaValid(this string sheba)
        {
            sheba = sheba.ToUpper();
            var firstFourCharacter = sheba.Substring(0, 4);
            var withOutFirstFourCharacter = sheba.Substring(4);
            firstFourCharacter = firstFourCharacter.Replace("I", "18").Replace("R", "27");

            var finalNumber = withOutFirstFourCharacter + firstFourCharacter;
            var newNumber = decimal.Parse(finalNumber);
            var leftOver = newNumber % 97;
            return leftOver == 1;
        }

        // https://iica.ir/useful-articles/2621-9502089
        #endregion

        #region Hash AND Encryption
        /// <summary>
        /// Convert 'input' to MD5
        /// </summary>
        /// <param name="input">Everything</param>
        /// <returns>string</returns>
        /// <returns>string</returns>
        public static string GetMd5(this string input)
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

        private static string GetMd5Hash(this MD5 md5Hash, string input)
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
        public static bool VerifyMd5Hash(this MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            return 0 == StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash);
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
        /// Get Random Password
        /// </summary>
        /// <param name="len"></param>
        /// <param name="isNumberic"></param>
        /// <returns></returns>
        public static string GetRandomPassword(int len = 6, bool isNumberic = false)
        {
            if (isNumberic)
            {
                Random generator = new Random();
                return generator.Next(0, 999999).ToString("D6");
            }
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, len);
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
        #endregion

        #region Comma Delimited
        /// <summary>
        /// Convert price to string and Seperate it by separator
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="separatedSize"></param>
        /// <returns></returns>
        public static string ToCommaDelimited(this int number, string separator = ",", int separatedSize = 3)
        {
            return ((long)number).ToCommaDelimited(separator, separatedSize);
        }

        /// <summary>
        /// Convert number to string and Seperate it by separator
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="separatedSize"></param>
        /// <returns></returns>
        public static string ToCommaDelimited(this long number, string separator = ",", int separatedSize = 3)
        {
            return number.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { separatedSize },
                NumberGroupSeparator = separator
            });
        }

        /// <summary>
        /// Convert number to string and Seperate it by separator
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="separatedSize"></param>
        /// <returns></returns>
        public static string ToCommaDelimited(this float number, string separator = ",", int separatedSize = 3)
        {
            return Convert.ToDecimal(number).ToCommaDelimited(separator, separatedSize);
        }

        /// <summary>
        /// Convert number to string and Seperate it by separator
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="separatedSize"></param>
        /// <returns></returns>
        public static string ToCommaDelimited(this double number, string separator = ",", int separatedSize = 3)
        {
            return Convert.ToDecimal(number).ToCommaDelimited(separator, separatedSize);
        }

        /// <summary>
        /// Convert number to string and Seperate it by separator
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="separatedSize"></param>
        /// <returns></returns>
        public static string ToCommaDelimited(this decimal number, string separator = ",", int separatedSize = 3)
        {
            var numbers = number.ToString().Split('.').ToList();
            var numberFormatInfo = new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { separatedSize },
                NumberGroupSeparator = separator
            };
            var num = decimal.Parse(numbers[0]).ToString("N0", numberFormatInfo);
            var point = string.Empty;
            if (numbers.Count == 2)
                point = CreateCommaSeparatedDecimalPoints(numbers[1]);

            return !string.IsNullOrEmpty(point) ? $"{num}.{point}" : $"{num}";

            string CreateCommaSeparatedDecimalPoints(string number)
            {
                //if all are zero return null

                if (number.All(y => y == '0' || y == 0)) return null;

                if (number.StartsWith("0"))
                {
                    number = '1' + number.Remove(0, 1);
                    var point = decimal.Parse(number).ToString("N0", numberFormatInfo);
                    return $"0{point.Remove(0, 1)}";
                }
                return decimal.Parse(number).ToString("N0", numberFormatInfo);
            }
        }
        #endregion

        #region Money And Currency
        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <param name="targetCurrency">The currency of result</param>
        /// <returns></returns>
        public static AuxiliaryIntPriceModel ToMoney(this int price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryIntPriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <param name="targetCurrency">The currency of result</param>
        /// <returns></returns>
        public static AuxiliaryIntPriceModel ToToman(this int price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryIntPriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <param name="targetCurrency">The currency of result</param>
        /// <returns></returns>
        public static AuxiliaryLongPriceModel ToToman(this long price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryLongPriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryFloatPriceModel ToToman(this float price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryFloatPriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDoublePriceModel ToToman(this double price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryDoublePriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDecimalPriceModel ToToman(this decimal price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryDecimalPriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryIntPriceModel ToRial(this int price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryIntPriceModel(price, baseCurrency, Currency.Toman);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryLongPriceModel ToRial(this long price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryLongPriceModel(price, baseCurrency, Currency.IRR);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryFloatPriceModel ToRial(this float price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryFloatPriceModel(price, baseCurrency, Currency.IRR);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDoublePriceModel ToRial(this double price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryDoublePriceModel(price, baseCurrency, Currency.IRR);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="baseCurrency">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDecimalPriceModel ToRial(this decimal price, Currency baseCurrency = Currency.IRR) =>
            new AuxiliaryDecimalPriceModel(price, baseCurrency, Currency.IRR);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryFloatPriceModel ToBTC(this float price, bool metricSystem = true) =>
            new AuxiliaryFloatPriceModel(price, Currency.Toman, Currency.BTC, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryFloatPriceModel ToUSD(this float price, bool metricSystem = false) =>
            new AuxiliaryFloatPriceModel(price, Currency.Toman, Currency.USD, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryFloatPriceModel ToGBP(this float price, bool metricSystem = false) =>
            new AuxiliaryFloatPriceModel(price, Currency.Toman, Currency.GBP, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryFloatPriceModel ToEUR(this float price, bool metricSystem = false) =>
            new AuxiliaryFloatPriceModel(price, Currency.Toman, Currency.EUR, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDoublePriceModel ToBTC(this double price, bool metricSystem = true) =>
            new AuxiliaryDoublePriceModel(price, Currency.Toman, Currency.BTC, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDoublePriceModel ToUSD(this double price, bool metricSystem = false) =>
            new AuxiliaryDoublePriceModel(price, Currency.Toman, Currency.USD, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDoublePriceModel ToGBP(this double price, bool metricSystem = false) =>
            new AuxiliaryDoublePriceModel(price, Currency.Toman, Currency.GBP, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDoublePriceModel ToEUR(this double price, bool metricSystem = false) =>
            new AuxiliaryDoublePriceModel(price, Currency.Toman, Currency.EUR, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDecimalPriceModel ToBTC(this decimal price, bool metricSystem = true) =>
            new AuxiliaryDecimalPriceModel(price, Currency.Toman, Currency.BTC, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDecimalPriceModel ToUSD(this decimal price, bool metricSystem = false) =>
            new AuxiliaryDecimalPriceModel(price, Currency.Toman, Currency.USD, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDecimalPriceModel ToGBP(this decimal price, bool metricSystem = false) =>
            new AuxiliaryDecimalPriceModel(price, Currency.Toman, Currency.GBP, metricSystem);

        /// <summary>
        ///  Convert price to AuxiliaryPriceModel
        ///  AuxiliaryPriceModel includes Price iteself, Short Format of price, Price Currency, and the pretty format of price.
        /// </summary>
        /// <param name="price">The price you need to convert</param>
        /// <param name="metricSystem">The currency of price parameter</param>
        /// <returns></returns>
        public static AuxiliaryDecimalPriceModel ToEUR(this decimal price, bool metricSystem = false) =>
            new AuxiliaryDecimalPriceModel(price, Currency.Toman, Currency.EUR, metricSystem);
        #endregion

        #region DateTime
        /// <summary>
        /// Convert DateTime to TimeStamp
        /// Unix time is a system for describing a point in time. It is the number of seconds that have elapsed since the Unix epoch, that is the time 00:00:00 UTC on 1 January 1970, minus leap seconds.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var epoch = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)epoch.TotalSeconds;
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
        /// Convert TimeStamp to DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime FromTimeStampToDateTime(this long timeStamp, DateTimeKind dateTimeKind = DateTimeKind.Local)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Convert datetime to mobile datetime format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToMobileDateTime(this DateTime dateTime) => dateTime.ToDateTimeFormat("yyyy-MM-dd'T'HH:mm:sszzz");

        /// <summary>
        /// Convert DateTime to any other format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDateTimeFormat(this DateTime dateTime, string format = "yyyyMMddHHmmssffff") => dateTime.ToString(format);

        /// <summary>
        /// Normalize Persian Date
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
        #endregion

        #region Utilities
        /// <summary>
        /// ToPrettySize helps you to get size of files as a pereety format
        /// 1) 512 MG
        /// 2) 512 مگابایت
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimalPlaces"></param>
        /// <param name="toPersian"></param>
        /// <returns></returns>
        public static string ToPrettySize(this Int64 value, int decimalPlaces = 1, bool toPersian = true)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + ToPrettySize(-value); }
            if (value == 0) { return toPersian ? string.Format("{0:n" + decimalPlaces + "} بایت", 0) : string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }
            var msg = toPersian ? PersianSizeSuffixes[mag] : SizeSuffixes[mag];
            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                msg);
        }

        /// <summary>
        /// Conpare first and last is Equal or not
        /// https://stackoverflow.com/questions/6371150/comparing-two-strings-ignoring-case-in-c-sharp
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Equals(this string first, string last, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Equals(first, last, stringComparison);
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
        /// Splitting a string into chunks of a certain size
        /// </summary>
        /// <param name="text">Considered word</param>
        /// <param name="chunkSize">Considered length</param>
        /// <returns>string</returns>
        //https://stackoverflow.com/questions/1450774/splitting-a-string-into-chunks-of-a-certain-size
        public static List<string> Split(this string text, int chunkSize)
        {
            if (text == null || text.Length <= chunkSize)
                return new List<string>() { text };
            return Enumerable.Range(0, text.Length / chunkSize)
                    .Select(i => text.Substring(i * chunkSize, chunkSize)).ToList();
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
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AnalyseFileName(this string fileName, out string format, out string basePath, out string name)
        {
            format = fileName.Substring(fileName.LastIndexOf('.') + 1);
            basePath = fileName.Substring(0, fileName.LastIndexOf('\\'));
            name = fileName.Substring(fileName.LastIndexOf('\\') + 1);
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
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<long> ToListLong(this string term, char separator = ',')
        {
            var result = new List<long>();
            var strList = term.Split(separator).ToList();
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
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<int> ToListInt(this string term, char separator = ',')
        {
            var result = new List<int>();
            var strList = term.Split(separator).ToList();
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
        /// Convert object to boolean
        /// </summary>
        /// <param name="term"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBooleanOrDefault(this string term, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(term))
                return defaultValue;
            string value = term.ToLower();
            switch (value)
            {
                case "yes":
                case "true":
                case "ok":
                case "y":
                    return true;
                case "no":
                case "false":
                case "n":
                    return false;
                default:
                    bool b;
                    if (bool.TryParse(term, out b))
                        return b;
                    break;
            }
            return defaultValue;
        }
        #endregion
    }
}