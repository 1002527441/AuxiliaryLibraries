using System;
using System.Linq;
using System.Text;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// RC4 Encryption
    /// </summary>
    public static class AuxiliaryEncryption
    {
        //#region Encrypt
        ///// <summary>
        ///// Encrypt you key
        ///// </summary>
        ///// <param name="key">key</param>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public static string Encrypt(string key, string data)
        //{
        //    Encoding unicode = Encoding.Unicode;
        //    return Convert.ToBase64String(Encrypt(unicode.GetBytes(key), unicode.GetBytes(data)));
        //}

        ///// <summary>
        ///// Encrypt you key
        ///// </summary>
        ///// <param name="key">key</param>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public static byte[] Encrypt(byte[] key, byte[] data)
        //{
        //    return EncryptOutput(key, data).ToArray();
        //}

        ///// <summary>
        ///// Encrypt you key
        ///// </summary>
        ///// <param name="key">key</param>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public static byte[] Encrypt(string key, byte[] data)
        //{
        //    byte[] ciphertext = new byte[data.Length];
        //    int i = 0, j = 0, k, t;
        //    byte tmp;
        //    var S = KeyMaker(key);
        //    for (int counter = 0; counter < data.Length; counter++)
        //    {
        //        i = (i + 1) & 0xFF;
        //        j = (j + S[i]) & 0xFF;
        //        tmp = S[j];
        //        S[j] = S[i];
        //        S[i] = tmp;
        //        t = (S[i] + S[j]) & 0xFF;
        //        k = S[t];
        //        ciphertext[counter] = (byte)(data[counter] ^ k);
        //    }
        //    return ciphertext;
        //}
        //#endregion

        //#region Decrypt
        ///// <summary>
        ///// Decrypt you key
        ///// </summary>
        ///// <param name="key">key</param>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public static string Decrypt(string key, string data)
        //{
        //    Encoding unicode = Encoding.Unicode;
        //    return unicode.GetString(Encrypt(unicode.GetBytes(key), Convert.FromBase64String(data)));
        //}

        ///// <summary>
        ///// Decrypt you key
        ///// </summary>
        ///// <param name="key">key</param>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public static byte[] Decrypt(byte[] key, byte[] data)
        //{
        //    return EncryptOutput(key, data).ToArray();
        //}

        ///// <summary>
        ///// Decrypt you key
        ///// </summary>
        ///// <param name="key">key</param>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public static byte[] Decrypt(string key, byte[] data)
        //{
        //    return Encrypt(key, data);
        //}
        //#endregion

        //private static byte[] EncryptInitalize(byte[] key)
        //{
        //    byte[] s = Enumerable.Range(0, 256)
        //      .Select(i => (byte)i)
        //      .ToArray();

        //    for (int i = 0, j = 0; i < 256; i++)
        //    {
        //        j = (j + key[i % key.Length] + s[i]) & 255;

        //        Swap(s, i, j);
        //    }

        //    return s;
        //}

        //private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
        //{
        //    byte[] s = EncryptInitalize(key);

        //    int i = 0;
        //    int j = 0;

        //    return data.Select((b) =>
        //    {
        //        i = (i + 1) & 255;
        //        j = (j + s[i]) & 255;

        //        Swap(s, i, j);

        //        return (byte)(b ^ s[(s[i] + s[j]) & 255]);
        //    });
        //}

        //private static void Swap(byte[] s, int i, int j)
        //{
        //    byte c = s[i];

        //    s[i] = s[j];
        //    s[j] = c;
        //}

        /// <summary>
        /// Encrypt your data with key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static string Encrypt(string key, string data)
        {
            Encoding encoding = Encoding.UTF8;
            return Encrypt(key, data, encoding);
        }

        /// <summary>
        /// Encrypt your data with key and encoding
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="skipZero"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string data, Encoding encoding, bool skipZero = false)
        {
            return Convert.ToBase64String(Encrypt(KeyMaker(key), encoding.GetBytes(data), skipZero));
        }

        private static byte[] Encrypt(byte[] pwd, byte[] data, bool skipZero = false)
        {
            int a, i, j, k, tmp;
            int[] key, box;
            byte[] cipher;

            key = new int[256];
            box = new int[256];
            cipher = new byte[data.Length];

            for (i = 0; i < 256; i++)
            {
                key[i] = pwd[i % pwd.Length];
                box[i] = i;
            }
            for (j = i = 0; i < 256; i++)
            {
                j = (j + box[i] + key[i]) % 256;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data.Length; i++)
            {
                a++;
                a %= 256;
                j += box[a];
                j %= 256;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[((box[a] + box[j]) % 256)];
                cipher[i] = (byte)(data[i] ^ k);
            }
            return skipZero ? cipher.Where(x => x > 0).ToArray() : cipher.ToArray();
        }

        /// <summary>
        /// Decrypt your data with key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static string Decrypt(string key, string data)
        {
            Encoding encoding = Encoding.UTF8;
            return Decrypt(key, data, encoding);
        }

        /// <summary>
        /// Decrypt your data with key and encoding
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="skipZero"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string data, Encoding encoding, bool skipZero = false)
        {
            return encoding.GetString(Decrypt(KeyMaker(key), Convert.FromBase64String(data), skipZero));
        }

        private static byte[] Decrypt(byte[] pwd, byte[] data, bool skipZero = false)
        {
            return Encrypt(pwd, data, skipZero);
        }

        private static byte[] KeyMaker(string keyString)
        {
            Encoding unicode = Encoding.UTF8;
            var key = unicode.GetBytes(keyString);
            if (key.Length < 1 || key.Length > 256)
            {
                throw new Exception("key must be between 1 and 256 bytes");
            }

            int keylen = key.Length;
            byte[] S = new byte[256];
            byte[] T = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
                T[i] = key[i % keylen];
            }
            int j = 0;
            byte tmp;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) & 0xFF;
                tmp = S[j];
                S[j] = S[i];
                S[i] = tmp;
            }
            return S;
        }
    }
}