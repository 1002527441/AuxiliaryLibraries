using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// RC4 Encryption
    /// </summary>
    public class AuxiliaryEncryption
    {
        /// <summary>
        /// Encruption By RC4
        /// </summary>
       public class RC4
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

        /// <summary>
        /// Encryption By AES
        /// </summary>
        public class AES
        {
            /// <summary>
            /// Encrypt File
            /// </summary>
            /// <param name="inputFile"></param>
            /// <param name="outputFile"></param>
            /// <param name="skey"></param>
            public static void EncryptFile(string inputFile, string outputFile, string skey)
            {
                try
                {
                    var salt = skey.Substring(0, 16);
                    using (System.Security.Cryptography.RijndaelManaged aes = new System.Security.Cryptography.RijndaelManaged())
                    {
                        byte[] key = ASCIIEncoding.UTF8.GetBytes(salt);

                        byte[] IV = ASCIIEncoding.UTF8.GetBytes(salt);

                        using (System.IO.FileStream fsCrypt = new System.IO.FileStream(outputFile, System.IO.FileMode.Create))
                        {
                            using (System.Security.Cryptography.ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                            {
                                using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(fsCrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                                {
                                    using (System.IO.FileStream fsIn = new System.IO.FileStream(inputFile, System.IO.FileMode.Open))
                                    {
                                        int data;
                                        while ((data = fsIn.ReadByte()) != -1)
                                        {
                                            cs.WriteByte((byte)data);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // failed to encrypt file
                }
            }

            /// <summary>
            /// Decrypt File
            /// </summary>
            /// <param name="inputFile"></param>
            /// <param name="outputFile"></param>
            /// <param name="skey"></param>
            public static void DecryptFile(string inputFile, string outputFile, string skey)
            {
                try
                {
                    var salt = skey.Substring(0, 16);
                    using (System.Security.Cryptography.RijndaelManaged aes = new System.Security.Cryptography.RijndaelManaged())
                    {
                        byte[] key = ASCIIEncoding.UTF8.GetBytes(salt);

                        byte[] IV = ASCIIEncoding.UTF8.GetBytes(salt);

                        using (System.IO.FileStream fsCrypt = new System.IO.FileStream(inputFile, System.IO.FileMode.Open))
                        {
                            using (System.IO.FileStream fsOut = new System.IO.FileStream(outputFile, System.IO.FileMode.Create))
                            {
                                using (System.Security.Cryptography.ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                                {
                                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(fsCrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                                    {
                                        int data;
                                        while ((data = cs.ReadByte()) != -1)
                                        {
                                            fsOut.WriteByte((byte)data);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // failed to decrypt file
                }
            }
        }

        /// <summary>
        /// Encryption By RSA
        /// </summary>
        public class RSA
        {
            #region RSA
            private UnicodeEncoding ByteConverter = new UnicodeEncoding();
            private RSACryptoServiceProvider CSP = new RSACryptoServiceProvider();
            private RSAParameters _privateKey;
            private RSAParameters _publicKey;

            /// <summary>
            /// Constructor
            /// </summary>
            public RSA()
            {
                _privateKey = CSP.ExportParameters(true);
                _publicKey = CSP.ExportParameters(false);
            }

            /// <summary>
            /// Public key generator
            /// </summary>
            /// <returns></returns>
            public string PublicKeyString()
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(typeof(RSAParameters));
                xs.Serialize(sw, _publicKey);
                return sw.ToString();
            }

            /// <summary>
            /// Encrypt
            /// </summary>
            /// <param name="plainText"></param>
            /// <returns></returns>
            public string Encrypt(string plainText)
            {
                CSP = new RSACryptoServiceProvider();
                CSP.ImportParameters(_publicKey);

                var data = Encoding.Unicode.GetBytes(plainText);
                var cypher = CSP.Encrypt(data, false);
                return Convert.ToBase64String(cypher);
            }

            /// <summary>
            /// Decrypt
            /// </summary>
            /// <param name="cypherText"></param>
            /// <returns></returns>
            public string Decrypt(string cypherText)
            {
                var dataBytes = Convert.FromBase64String(cypherText);
                CSP.ImportParameters(_privateKey);
                var plainText = CSP.Decrypt(dataBytes, false);
                return Encoding.Unicode.GetString(plainText);
            }

            //public static string RsaCrypto(string plainTextData)
            //{
            //    //lets take a new CSP with a new 2048 bit rsa key pair
            //    var csp = new System.Security.Cryptography.RSACryptoServiceProvider(2048);

            //    //how to get the private key
            //    var privKey = csp.ExportParameters(true);

            //    //and the public key ...
            //    var pubKey = csp.ExportParameters(false);

            //    //converting the public key into a string representation
            //    string pubKeyString;
            //    {
            //        //we need some buffer
            //        var sw = new System.IO.StringWriter();
            //        //we need a serializer
            //        var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //        //serialize the key into the stream
            //        xs.Serialize(sw, pubKey);
            //        //get the string from the stream
            //        pubKeyString = sw.ToString();
            //    }

            //    //converting it back
            //    {
            //        //get a stream from the string
            //        var sr = new System.IO.StringReader(pubKeyString);
            //        //we need a deserializer
            //        var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //        //get the object back from the stream
            //        pubKey = (RSAParameters)xs.Deserialize(sr);
            //    }

            //    //conversion for the private key is no black magic either ... omitted

            //    //we have a public key ... let's get a new csp and load that key
            //    csp = new RSACryptoServiceProvider();
            //    csp.ImportParameters(pubKey);

            //    //for encryption, always handle bytes...
            //    var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

            //    //apply pkcs#1.5 padding and encrypt our data 
            //    var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

            //    //we might want a string representation of our cypher text... base64 will do
            //    var cypherText = Convert.ToBase64String(bytesCypherText);


            //    /*
            //     * some transmission / storage / retrieval
            //     * 
            //     * and we want to decrypt our cypherText
            //     */

            //    //first, get our bytes back from the base64 string ...
            //    bytesCypherText = Convert.FromBase64String(cypherText);

            //    //we want to decrypt, therefore we need a csp and load our private key
            //    csp = new RSACryptoServiceProvider();
            //    csp.ImportParameters(privKey);

            //    //decrypt and strip pkcs#1.5 padding
            //    bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

            //    //get our original plainText back...
            //    return System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
            //}

            //public static string Encryption(string plainTextData)
            //{
            //    byte[] plaintext;
            //    byte[] encryptedtext;

            //    plaintext = ByteConverter.GetBytes(plainTextData);
            //    encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            //    return ByteConverter.GetString(encryptedtext);
            //}

            //public static string Decryption(string plainTextData)
            //{
            //    UnicodeEncoding ByteConverter = new UnicodeEncoding();
            //    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //    byte[] plaintext;
            //    plaintext = ByteConverter.GetBytes(plainTextData);

            //    byte[] decryptedtex = Decryption(plaintext, RSA.ExportParameters(true), false);
            //    return ByteConverter.GetString(decryptedtex);
            //}

            //static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
            //{
            //    try
            //    {
            //        byte[] encryptedData;
            //        using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            //        {
            //            RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
            //        }
            //        return encryptedData;
            //    }
            //    catch (CryptographicException e)
            //    {
            //        Console.WriteLine(e.Message);
            //        return null;
            //    }
            //}

            //static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
            //{
            //    try
            //    {
            //        byte[] decryptedData;
            //        using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            //        {
            //            RSA.ImportParameters(RSAKey);
            //            decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
            //        }
            //        return decryptedData;
            //    }
            //    catch (CryptographicException e)
            //    {
            //        Console.WriteLine(e.ToString());
            //        return null;
            //    }
            //}
            #endregion
        }
    }
}