using AuxiliaryLibraries.Core.Enums;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace AuxiliaryLibraries.Core.AuxilaryServices
{
    /// <summary>
    /// Auxiliary Encryption
    /// </summary>
    public class AuxiliaryEncryption
    {
        /// <summary>
        /// Auxiliary Encryption
        /// </summary>
        public AuxiliaryEncryption()
        {

        }

        /// <summary>
        /// RC4 Encryption
        /// </summary>
        public static class RC4
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
                    k = box[(box[a] + box[j]) % 256];
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
                    j = j + S[i] + T[i] & 0xFF;
                    tmp = S[j];
                    S[j] = S[i];
                    S[i] = tmp;
                }
                return S;
            }
        }

        /// <summary>
        /// Encryption Mode
        /// On File enctyption, specificlly on AES, you should choose the mode
        /// </summary>
        public enum EncryptionMode
        {
            /// <summary>
            /// Read all files using File.ReadAllBytes
            /// </summary>
            Bytes = 1,
            /// <summary>
            /// Read all files using File.ReadAllText and Encoding
            /// </summary>
            Encoding = 2,
            /// <summary>
            /// Read all files using FileStream
            /// </summary>
            FileStream = 3
        }

        /// <summary>
        /// AES Encryption
        /// </summary>
        public class AES
        {
            private ICryptoTransform decryptor;
            private ICryptoTransform encryptor;
            private Encoding encoding;
            private PaddingMode paddingMode;
            private CipherMode cipherMode;
            private int keySize = 256;
            private int blockSize = 128;
            private EncryptionMode encryptionMode = EncryptionMode.FileStream;
            // Replace me with a 16-byte key, share between Java and C#
            private static byte[] rawSecretKey = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            /// <summary>
            /// AES Constructor
            /// </summary>
            /// <param name="passphrase"></param>
            public AES(string passphrase)
            {
                Initialize(passphrase, Encoding.UTF8);
            }

            /// <summary>
            /// AES Constructor
            /// </summary>
            /// <param name="passphrase"></param>
            /// <param name="_encoding"></param>
            /// <param name="_paddingMode"></param>
            /// <param name="_cipherMode"></param>
            /// <param name="_keySize"></param>
            /// <param name="_blockSize"></param>
            /// <param name="_encryptionMode"></param>
            public AES(string passphrase, Encoding _encoding, PaddingMode _paddingMode = PaddingMode.PKCS7, CipherMode _cipherMode = CipherMode.CBC, int _keySize = 256, int _blockSize = 128, EncryptionMode _encryptionMode = EncryptionMode.FileStream)
            {
                Initialize(passphrase, _encoding, _paddingMode, _cipherMode, _keySize, _blockSize, _encryptionMode);
            }

            #region Encrypt
            /// <summary>
            /// EncryptFile
            /// </summary>
            /// <param name="inputFile"></param>
            /// <param name="outputFile"></param>
            public void EncryptFile(string inputFile, string outputFile)
            {
                switch (encryptionMode)
                {
                    case EncryptionMode.Bytes:
                        {
                            EncryptFileUsingBytes(inputFile, outputFile);
                        }
                        break;
                    case EncryptionMode.Encoding:
                        {
                            EncryptFileUsingEncoding(inputFile, outputFile);
                        }
                        break;
                    case EncryptionMode.FileStream:
                        {
                            EncryptFileUsingFileStream(inputFile, outputFile);
                        }
                        break;
                }
            }

            /// <summary>
            /// Encrypt a plain Text to an Encrypted Text
            /// </summary>
            /// <param name="plainText"></param>
            /// <returns></returns>
            public string Encrypt(string plainText)
            {
                byte[] encrypted = EncryptToByteArray(plainText);
                return Convert.ToBase64String(encrypted);
            }

            /// <summary>
            /// Encrypt plain Text to an Encrypted Byte Array
            /// </summary>
            /// <param name="plainText"></param>
            /// <returns></returns>
            public byte[] EncryptToBytes(string plainText)
            {
                var bytesToEncrypt = encoding.GetBytes(plainText);
                return Encrypt(bytesToEncrypt);
            }

            /// <summary>
            /// EncryptAsBase64
            /// </summary>
            /// <param name="clearData"></param>
            /// <returns></returns>
            public string EncryptAsBase64(byte[] clearData)
            {
                byte[] encryptedData = Encrypt(clearData);
                return Convert.ToBase64String(encryptedData);
            }

            /// <summary>
            /// Encrypt
            /// </summary>
            /// <param name="bytesToEncrypt"></param>
            /// <returns></returns>
            public byte[] Encrypt(byte[] bytesToEncrypt)
            {
                var mstream = new MemoryStream();
                using (var cstream = new CryptoStream(mstream, decryptor, CryptoStreamMode.Write))
                {
                    cstream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    cstream.FlushFinalBlock();
                }
                return mstream.ToArray();
            }

            /// <summary>
            /// EncodeDigest
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public byte[] EncodeDigest(string text)
            {
                MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
                byte[] data = encoding.GetBytes(text);
                return x.ComputeHash(data);
            }
            #endregion

            #region Decrypt
            /// <summary>
            /// DecryptFile
            /// </summary>
            /// <param name="inputFile"></param>
            /// <param name="outputFile"></param>
            public void DecryptFile(string inputFile, string outputFile)
            {
                switch (encryptionMode)
                {
                    case EncryptionMode.Bytes:
                        {
                            DecryptFileUsingBytes(inputFile, outputFile);
                        }
                        break;
                    case EncryptionMode.Encoding:
                        {
                            EncryptFileUsingEncoding(inputFile, outputFile);
                        }
                        break;
                    case EncryptionMode.FileStream:
                        {
                            DecryptFileUsingFileStream(inputFile, outputFile);
                        }
                        break;
                }
            }

            /// <summary>
            /// Decrypt 
            /// </summary>
            /// <param name="strtoencrypt"></param>
            /// <returns></returns>
            public string Decrypt(string strtoencrypt)
            {
                return Decrypt(Convert.FromBase64String(strtoencrypt));
            }

            ///// <summary>
            ///// Decrypt
            ///// </summary>
            ///// <param name="strtoencrypt"></param>
            ///// <returns></returns>
            //public string Decrypt(string strtoencrypt)
            //{
            //    var bytesToEncrypt = encoding.GetBytes(strtoencrypt);
            //    return Decrypt(bytesToEncrypt);
            //}

            /// <summary>
            /// Decrypt
            /// </summary>
            /// <param name="encryptedData"></param>
            /// <returns></returns>
            public string Decrypt(byte[] encryptedData)
            {
                byte[] newClearData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return encoding.GetString(newClearData);
            }
            #endregion

            #region Private Functions
            private void Initialize(string passphrase, Encoding _encoding, PaddingMode _paddingMode = PaddingMode.PKCS7, CipherMode _cipherMode = CipherMode.CBC, int _keySize = 256, int _blockSize = 128, EncryptionMode _encryptionMode = EncryptionMode.FileStream)
            {
                if (passphrase.Length < 16)
                    throw new Exception("The minimum Length for passphrase is 16, but it is recommended to be more than 32 characters");

                encoding = _encoding;
                paddingMode = _paddingMode;
                cipherMode = _cipherMode;
                keySize = _keySize;
                blockSize = _blockSize;
                encryptionMode = _encryptionMode;

                //byte[] passwordKey = encodeDigest(passphrase);
                var salt = passphrase.Substring(0, 16);
                var iv = passphrase.Length >= 32 ? passphrase.Substring(16, 16) : salt;
                byte[] key = encoding.GetBytes(salt); //encodeDigest(salt);
                //File.WriteAllLines(@"X:\Amin\EncryptDecrypt\Ali Test\New Way\key Byte Array.txt", key.Select(x => x.ToString()).ToArray());
                byte[] IV = encoding.GetBytes(iv);  //encodeDigest(iv);
                //File.WriteAllLines(@"X:\Amin\EncryptDecrypt\Ali Test\New Way\IV Byte Array.txt", IV.Select(x => x.ToString()).ToArray());

                //var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                //AES.Key = key.GetBytes(AES.KeySize / 8);
                //AES.IV = key.GetBytes(AES.BlockSize / 8);

                RijndaelManaged rijndael = new RijndaelManaged();//AesManaged//RijndaelManaged
                rijndael.Padding = paddingMode;
                rijndael.Mode = cipherMode;
                if (keySize > 0)
                    rijndael.KeySize = keySize;
                if (blockSize > 0)
                    rijndael.BlockSize = blockSize;
                encryptor = rijndael.CreateEncryptor(key, IV);
                decryptor = rijndael.CreateDecryptor(key, IV);
            }

            private void DecryptFileUsingBytes(string inputFile, string outputFile)
            {
                try
                {
                    byte[] bytesToBeDecrypted = File.ReadAllBytes(inputFile);
                    //File.WriteAllLines(outputFile.Replace(".jpg", " - DecryptFile Byte Array.txt"), bytesToBeDecrypted.Select(x => x.ToString()).ToArray());
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        byte[] decryptedBytes = ms.ToArray();
                        File.WriteAllBytes(outputFile, decryptedBytes);
                    }
                }
                catch (Exception ex)
                {
                    // failed to decrypt file
                    ex.ToString();
                }
            }

            private void DecryptFileUsingEncoding(string inputFile, string outputFile)
            {
                try
                {
                    var text = File.ReadAllText(inputFile, encoding);
                    byte[] bytesToBeDecrypted = encoding.GetBytes(text);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        byte[] decryptedBytes = ms.ToArray();
                        File.WriteAllBytes(outputFile, decryptedBytes);
                    }
                }
                catch (Exception ex)
                {
                    // failed to decrypt file
                    ex.ToString();
                }
            }

            private void DecryptFileUsingFileStream(string inputFile, string outputFile)
            {
                try
                {
                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
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
                catch (Exception ex)
                {
                    // failed to decrypt file
                    ex.ToString();
                }
            }

            private byte[] EncryptToByteArray(string plainText)
            {
                byte[] encrypted;
                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                // Return the encrypted bytes from the memory stream. 
                return encrypted;
            }

            private void EncryptFileUsingBytes(string inputFile, string outputFile)
            {
                try
                {
                    byte[] bytesToBeEncrypted = File.ReadAllBytes(inputFile);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        byte[] decryptedBytes = ms.ToArray();
                        //File.WriteAllLines(outputFile.Replace(".jpg", " - EncryptFile Byte Array.txt"), bytesToBeEncrypted.Select(x => x.ToString()).ToArray());
                        File.WriteAllBytes(outputFile, decryptedBytes);
                    }

                    //using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    //{
                    //    using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                    //    {
                    //        using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                    //        {
                    //            int data;
                    //            while ((data = fsIn.ReadByte()) != -1)
                    //            {
                    //                cs.WriteByte((byte)data);
                    //            }
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    // failed to encrypt file
                    ex.ToString();
                }
            }

            private void EncryptFileUsingEncoding(string inputFile, string outputFile)
            {
                try
                {
                    var text = File.ReadAllText(inputFile, encoding);
                    byte[] bytesToBeEncrypted = encoding.GetBytes(text);
                    File.WriteAllLines(outputFile.Replace(".jpg", " - EncryptFile Byte Array.txt"), bytesToBeEncrypted.Select(x => x.ToString()).ToArray());

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        byte[] decryptedBytes = ms.ToArray();
                        File.WriteAllBytes(outputFile, decryptedBytes);
                    }
                }
                catch (Exception ex)
                {
                    // failed to encrypt file
                    ex.ToString();
                }
            }

            private void EncryptFileUsingFileStream(string inputFile, string outputFile)
            {
                try
                {
                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
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
                catch (Exception ex)
                {
                    // failed to encrypt file
                }
            }
            #endregion
        }

        //https://security.stackexchange.com/questions/33434/rsa-maximum-bytes-to-encrypt-comparison-to-aes-in-terms-of-security
        //https://tls.mbed.org/kb/cryptography/rsa-encryption-maximum-data-size
        /// <summary>
        /// RSA Encryption
        /// By creating an instance, public key and private key automatically will be generated by default
        /// You can ethier use them and save them as *.xml or *.pem file by using functions SavePublicKeyToXmlFile, SavePublicKeyToPemFile,SavePrivateKeyToXmlFile, and SavePrivateKeyToPemFile
        /// You can also replace them by your own public key and private key by using SetPublicKey and SetPrivateKey (pass the path of *.xml or *.pem file)
        /// </summary>
        public class RSA
        {
            #region RSA
            private const string pemMimeType = "application/x-x509-ca-cert", xmlMimeType = "application/xml";
            private static UnicodeEncoding ByteConverter = new UnicodeEncoding();
            private static RSACryptoServiceProvider CSP;
            private RSAParameters _privateKey;
            private RSAParameters _publicKey;
            private Encoding _encoding = Encoding.Unicode;
            public bool IsPublicKeyGenerated => !_publicKey.Equals(default);
            public bool IsPrivateKeyGenerated => !_privateKey.Equals(default);

            /// <summary>
            /// Initializes a new instance of the System.Security.Cryptography.RSACryptoServiceProvider class with
            /// the specified key size,
            /// the specified parameters,
            /// and the specified rsaEncryptionKeys.
            /// </summary>
            public RSA(int dwKeySize = 0, CspParameters parameters = null)
            {
                CSP = parameters != null && dwKeySize > 0 ? new RSACryptoServiceProvider(dwKeySize, parameters) :
                                  parameters != null && dwKeySize <= 0 ? new RSACryptoServiceProvider(parameters) :
                                  parameters is null && dwKeySize > 0 ? new RSACryptoServiceProvider(dwKeySize) : new RSACryptoServiceProvider();
                _privateKey = CSP.ExportParameters(true);
                _publicKey = CSP.ExportParameters(false);
                //switch (rsaEncryptionKeys)
                //{
                //    case RSAEncryptionKeys.XML:
                //    default:
                //        {
                //            CSP = parameters != null && dwKeySize > 0 ? new RSACryptoServiceProvider(dwKeySize, parameters) :
                //                  parameters != null && dwKeySize <= 0 ? new RSACryptoServiceProvider(parameters) :
                //                  parameters is null && dwKeySize > 0 ? new RSACryptoServiceProvider(dwKeySize) : new RSACryptoServiceProvider();
                //            _privateKey = CSP.ExportParameters(true);
                //            _publicKey = CSP.ExportParameters(false);
                //        }
                //        break;
                //    case RSAEncryptionKeys.PEM:
                //        {
                //            string public_pem = "D:\\Projects\\Crypt\\ConsoleApplication1\\posvendor.pub.pem";
                //            string private_pem = "D:\\Projects\\Crypt\\ConsoleApplication1\\posvendor.key.pem";

                //            var pub = GetPublicKeyFromPemFile(public_pem);
                //            var pri = GetPrivateKeyFromPemFile(private_pem);

                //            _privateKey = pri.ExportParameters(true);
                //            _publicKey = pub.ExportParameters(false);
                //        }
                //        break;
                //}
            }

            #region Public Functions
            /// <summary>
            /// coverting a string representation into the public key (converting it back)
            /// </summary>
            /// <returns>void</returns>
            public void SetPublicKey(string publicKeyPath)
            {
                var rsaEncryptionKeys = ValidateFile(publicKeyPath);
                switch (rsaEncryptionKeys)
                {
                    case RSAEncryptionKeys.XML:
                    default:
                        {
                            var publicKeyString = File.ReadAllText(publicKeyPath);
                            //get a stream from the string
                            var sr = new StringReader(publicKeyString);
                            //we need a deserializer
                            var xs = new XmlSerializer(typeof(RSAParameters));
                            //get the object back from the stream
                            _publicKey = (RSAParameters)xs.Deserialize(sr);
                        }
                        break;
                    case RSAEncryptionKeys.PEM:
                        {
                            var pub = GetPublicKeyFromPemFile(publicKeyPath);
                            _publicKey = pub.ExportParameters(false);
                        }
                        break;
                }
            }

            /// <summary>
            /// Converting the public key into a string representation
            /// Save the public key to a file.
            /// It is better to pass the path as a XML file.
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool SavePublicKeyToXmlFile(string path)
            {
                try
                {
                    var sw = new StringWriter();
                    var xs = new XmlSerializer(typeof(RSAParameters));
                    xs.Serialize(sw, _publicKey);
                    File.WriteAllText(path, sw.ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            /// <summary>
            /// Converting the public key into a string representation
            /// Save the public key to a file.
            /// It is better to pass the path as a PEM file.
            /// https://stackoverflow.com/questions/28406888/c-sharp-rsa-public-key-output-not-correct/28407693#28407693
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool SavePublicKeyToPemFile(string path)
            {
                try
                {
                    using (TextWriter outputStream = new StreamWriter(path))
                    {
                        using (var stream = new MemoryStream())
                        {
                            var writer = new BinaryWriter(stream);
                            writer.Write((byte)0x30); // SEQUENCE
                            using (var innerStream = new MemoryStream())
                            {
                                var innerWriter = new BinaryWriter(innerStream);
                                innerWriter.Write((byte)0x30); // SEQUENCE
                                EncodeLength(innerWriter, 13);
                                innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                                var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                                EncodeLength(innerWriter, rsaEncryptionOid.Length);
                                innerWriter.Write(rsaEncryptionOid);
                                innerWriter.Write((byte)0x05); // NULL
                                EncodeLength(innerWriter, 0);
                                innerWriter.Write((byte)0x03); // BIT STRING
                                using (var bitStringStream = new MemoryStream())
                                {
                                    var bitStringWriter = new BinaryWriter(bitStringStream);
                                    bitStringWriter.Write((byte)0x00); // # of unused bits
                                    bitStringWriter.Write((byte)0x30); // SEQUENCE
                                    using (var paramsStream = new MemoryStream())
                                    {
                                        var paramsWriter = new BinaryWriter(paramsStream);
                                        EncodeIntegerBigEndian(paramsWriter, _publicKey.Modulus); // Modulus
                                        EncodeIntegerBigEndian(paramsWriter, _publicKey.Exponent); // Exponent
                                        var paramsLength = (int)paramsStream.Length;
                                        EncodeLength(bitStringWriter, paramsLength);
                                        bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                                    }
                                    var bitStringLength = (int)bitStringStream.Length;
                                    EncodeLength(innerWriter, bitStringLength);
                                    innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                                }
                                var length = (int)innerStream.Length;
                                EncodeLength(writer, length);
                                writer.Write(innerStream.GetBuffer(), 0, length);
                            }

                            var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                            outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                            for (var i = 0; i < base64.Length; i += 64)
                            {
                                outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                            }
                            outputStream.WriteLine("-----END PUBLIC KEY-----");
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return false;
                }
            }

            /// <summary>
            /// coverting a string representation into the private key (converting it back)
            /// </summary>
            /// <returns>void</returns>
            public void SetPrivateKey(string privateKeyPath)
            {
                var rsaEncryptionKeys = ValidateFile(privateKeyPath);
                switch (rsaEncryptionKeys)
                {
                    case RSAEncryptionKeys.XML:
                    default:
                        {
                            //get a stream from the string
                            var sr = new StringReader(privateKeyPath);
                            //we need a deserializer
                            var xs = new XmlSerializer(typeof(RSAParameters));
                            //get the object back from the stream
                            _privateKey = (RSAParameters)xs.Deserialize(sr);
                        }
                        break;
                    case RSAEncryptionKeys.PEM:
                        {
                            var pri = GetPrivateKeyFromPemFile(privateKeyPath);
                            _privateKey = pri.ExportParameters(true);
                        }
                        break;
                }
            }

            /// <summary>
            /// Converting the private key into a string representation
            /// Save the private key to a file.
            /// It is better to pass the path as a XML file.
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool SavePrivateKeyToXmlFile(string path)
            {
                try
                {
                    var sw = new StringWriter();
                    var xs = new XmlSerializer(typeof(RSAParameters));
                    xs.Serialize(sw, _privateKey);
                    File.WriteAllText(path, sw.ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            /// <summary>
            /// Converting the private key into a string representation
            /// Save the private key to a file.
            /// It is better to pass the path as a PEM file.
            /// https://stackoverflow.com/questions/23734792/c-sharp-export-private-public-rsa-key-from-rsacryptoserviceprovider-to-pem-strin
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool SavePrivateKeyToPemFile(string path)
            {
                try
                {
                    if (CSP.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
                    using (TextWriter outputStream = new StreamWriter(path))
                    {
                        using (var stream = new MemoryStream())
                        {
                            var writer = new BinaryWriter(stream);
                            writer.Write((byte)0x30); // SEQUENCE
                            using (var innerStream = new MemoryStream())
                            {
                                var innerWriter = new BinaryWriter(innerStream);
                                EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                                EncodeIntegerBigEndian(innerWriter, _privateKey.Modulus);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.Exponent);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.D);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.P);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.Q);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.DP);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.DQ);
                                EncodeIntegerBigEndian(innerWriter, _privateKey.InverseQ);
                                var length = (int)innerStream.Length;
                                EncodeLength(writer, length);
                                writer.Write(innerStream.GetBuffer(), 0, length);
                            }

                            var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                            outputStream.WriteLine("-----BEGIN RSA PRIVATE KEY-----");
                            // Output as Base64 with lines chopped at 64 characters
                            for (var i = 0; i < base64.Length; i += 64)
                            {
                                outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                            }
                            outputStream.WriteLine("-----END RSA PRIVATE KEY-----");
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return false;
                }
            }

            /// <summary>
            /// Encrypt
            /// </summary>
            /// <param name="plainText"></param>
            /// <returns></returns>
            public string Encrypt(string plainText)
            {
                CSP.ImportParameters(_publicKey);

                var data = _encoding.GetBytes(plainText);
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
                return _encoding.GetString(plainText);
            }

            /// <summary>
            /// Sign the data using the Smart Card CryptoGraphic Provider.
            /// </summary>
            /// <param name="content"></param>
            /// <param name="privateKey"></param>
            /// <returns></returns>
            public string Sign(string content, string privateKey)
            {
                var byteContent = Encoding.UTF8.GetBytes(content);
                CSP.FromXmlString(privateKey);
                var sign = CSP.SignData(byteContent, new SHA256CryptoServiceProvider());
                return Convert.ToBase64String(sign);
            }

            /// <summary>
            /// Verify the data using the Smart Card CryptoGraphic Provider.
            /// </summary>
            /// <param name="content"></param>
            /// <param name="sign"></param>
            /// <returns></returns>
            public bool Verify(string content, string sign)
            {
                var byteContent = Encoding.UTF8.GetBytes(content);
                var sig = Convert.FromBase64String(sign);
                return CSP.VerifyData(byteContent, new SHA256CryptoServiceProvider(), sig);
            }
            #endregion

            #region Private Functions
            private void EncodeLength(BinaryWriter stream, int length)
            {
                if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
                if (length < 0x80)
                {
                    // Short form
                    stream.Write((byte)length);
                }
                else
                {
                    // Long form
                    var temp = length;
                    var bytesRequired = 0;
                    while (temp > 0)
                    {
                        temp >>= 8;
                        bytesRequired++;
                    }
                    stream.Write((byte)(bytesRequired | 0x80));
                    for (var i = bytesRequired - 1; i >= 0; i--)
                    {
                        stream.Write((byte)(length >> 8 * i & 0xff));
                    }
                }
            }

            private void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
            {
                stream.Write((byte)0x02); // INTEGER
                var prefixZeros = 0;
                for (var i = 0; i < value.Length; i++)
                {
                    if (value[i] != 0) break;
                    prefixZeros++;
                }
                if (value.Length - prefixZeros == 0)
                {
                    EncodeLength(stream, 1);
                    stream.Write((byte)0);
                }
                else
                {
                    if (forceUnsigned && value[prefixZeros] > 0x7f)
                    {
                        // Add a prefix zero to force unsigned if the MSB is 1
                        EncodeLength(stream, value.Length - prefixZeros + 1);
                        stream.Write((byte)0);
                    }
                    else
                    {
                        EncodeLength(stream, value.Length - prefixZeros);
                    }
                    for (var i = prefixZeros; i < value.Length; i++)
                    {
                        stream.Write(value[i]);
                    }
                }
            }

            private static RSAEncryptionKeys ValidateFile(string filePath)
            {
                var mimeType = filePath.GetMimeTypeFromFilePath();
                if (mimeType.ToLower() != pemMimeType.ToLower() && mimeType.ToLower() != xmlMimeType.ToLower())
                    throw new Exception($"Fie {mimeType} is not Valid");
                return mimeType.ToLower() == pemMimeType.ToLower() ? RSAEncryptionKeys.PEM : RSAEncryptionKeys.XML;
            }

            private RSACryptoServiceProvider GetPrivateKeyFromPemFile(string filePath)
            {
                using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(filePath)))
                {
                    AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

                    RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                    RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
                    csp.ImportParameters(rsaParams);
                    return csp;
                }
            }

            private RSACryptoServiceProvider GetPublicKeyFromPemFile(string filePath)
            {
                using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(filePath)))
                {
                    RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

                    RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKeyParam);

                    RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
                    csp.ImportParameters(rsaParams);
                    return csp;
                }
            }
            #endregion

            #endregion
        }
    }
}