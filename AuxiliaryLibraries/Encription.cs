using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// RC4 Encription
    /// </summary>
    public static class Encription
    {
        /// <summary>
        /// Encrypt you key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static string Encrypt(string key, string data)
        {
            Encoding unicode = Encoding.Unicode;
            return Convert.ToBase64String(Encrypt(unicode.GetBytes(key), unicode.GetBytes(data)));
        }

        /// <summary>
        /// Decrypt you key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static string Decrypt(string key, string data)
        {
            Encoding unicode = Encoding.Unicode;

            return unicode.GetString(Encrypt(unicode.GetBytes(key), Convert.FromBase64String(data)));
        }

        /// <summary>
        /// Encrypt you key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static byte[] Encrypt(byte[] key, byte[] data)
        {
            return EncryptOutput(key, data).ToArray();
        }

        /// <summary>
        /// Encrypt you key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static byte[] Encrypt(string key, byte[] data)
        {
            byte[] ciphertext = new byte[data.Length];
            int i = 0, j = 0, k, t;
            byte tmp;
            var S = KeyMaker(key);
            for (int counter = 0; counter < data.Length; counter++)
            {
                i = (i + 1) & 0xFF;
                j = (j + S[i]) & 0xFF;
                tmp = S[j];
                S[j] = S[i];
                S[i] = tmp;
                t = (S[i] + S[j]) & 0xFF;
                k = S[t];
                ciphertext[counter] = (byte)(data[counter] ^ k);
            }
            return ciphertext;
        }

        /// <summary>
        /// Decrypt you key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            return EncryptOutput(key, data).ToArray();
        }

        /// <summary>
        /// Decrypt you key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public static byte[] Decrypt(string key, byte[] data)
        {
            return Encrypt(key, data);
        }

        private static byte[] EncryptInitalize(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
              .Select(i => (byte)i)
              .ToArray();

            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + key[i % key.Length] + s[i]) & 255;

                Swap(s, i, j);
            }

            return s;
        }

        private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
        {
            byte[] s = EncryptInitalize(key);

            int i = 0;
            int j = 0;

            return data.Select((b) =>
            {
                i = (i + 1) & 255;
                j = (j + s[i]) & 255;

                Swap(s, i, j);

                return (byte)(b ^ s[(s[i] + s[j]) & 255]);
            });
        }

        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];

            s[i] = s[j];
            s[j] = c;
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
