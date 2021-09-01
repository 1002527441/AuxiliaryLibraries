using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace AuxiliaryLibraries
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

            /// <summary>
            /// EncodeDigest
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public byte[] EncodeDigest(string text)
            {
                MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] data = encoding.GetBytes(text);
                return x.ComputeHash(data);
            }
        }

        /// <summary>
        /// RSA Encryption
        /// </summary>
        public class RSA
        {
            #region RSA
            private static UnicodeEncoding ByteConverter = new UnicodeEncoding();
            private static RSACryptoServiceProvider CSP;
            private RSAParameters _privateKey;
            private RSAParameters _publicKey;
            private Encoding _encoding = Encoding.Unicode;

            /// <summary>
            /// 
            /// </summary>
            public RSA()
            {
                CSP = new RSACryptoServiceProvider();
                _privateKey = CSP.ExportParameters(true);
                _publicKey = CSP.ExportParameters(false);
            }

            /// <summary>
            /// Summary:
            ///     Initializes a new instance of the System.Security.Cryptography.RSACryptoServiceProvider
            ///     class with the specified key size.
            /// 
            /// Parameters:
            ///   dwKeySize:
            ///     The size of the key to use in bits.
            /// 
            /// Exceptions:
            ///   T:System.Security.Cryptography.CryptographicException:
            ///     The cryptographic service provider (CSP) cannot be acquired.
            /// </summary>
            /// <param name="dwKeySize"></param>
            public RSA(int dwKeySize)
            {
                //lets take a new CSP with a new dwKeySize bit rsa key pair
                CSP = new RSACryptoServiceProvider(dwKeySize);
                _privateKey = CSP.ExportParameters(true);
                _publicKey = CSP.ExportParameters(false);
            }

            /// <summary>
            /// Summary:
            ///     Initializes a new instance of the System.Security.Cryptography.RSACryptoServiceProvider
            ///     class with the specified parameters.
            /// 
            /// Parameters:
            ///   parameters:
            ///     The parameters to be passed to the cryptographic service provider (CSP).
            /// 
            /// Exceptions:
            ///   T:System.Security.Cryptography.CryptographicException:
            ///     The CSP cannot be acquired.
            /// </summary>
            /// <param name="parameters"></param>
            public RSA(CspParameters parameters)
            {
                //lets take a new CSP with parameters
                CSP = new RSACryptoServiceProvider(parameters);
                _privateKey = CSP.ExportParameters(true);
                _publicKey = CSP.ExportParameters(false);
            }

            /// <summary>
            /// Summary:
            ///     Initializes a new instance of the System.Security.Cryptography.RSACryptoServiceProvider
            ///     class with the specified key size and parameters.
            /// 
            /// Parameters:
            ///   dwKeySize:
            ///     The size of the key to use in bits.
            /// 
            ///   parameters:
            ///     The parameters to be passed to the cryptographic service provider (CSP).
            /// 
            /// Exceptions:
            ///   T:System.Security.Cryptography.CryptographicException:
            ///     The CSP cannot be acquired.-or- The key cannot be created.
            /// </summary>
            /// <param name="dwKeySize"></param>
            /// <param name="parameters"></param>
            public RSA(int dwKeySize, CspParameters parameters)
            {
                //lets take a new CSP with a new dwKeySize bit rsa key pair and parameters
                CSP = new RSACryptoServiceProvider(dwKeySize, parameters);
                _privateKey = CSP.ExportParameters(true);
                _publicKey = CSP.ExportParameters(false);
            }

            /// <summary>
            /// converting the public key into a string representation
            /// </summary>
            /// <returns>string</returns>
            public string PublicKey()
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(typeof(RSAParameters));
                xs.Serialize(sw, _publicKey);
                return sw.ToString();
            }

            /// <summary>
            /// coverting a string representation into the public key (converting it back)
            /// </summary>
            /// <returns>void</returns>
            public void PublicKey(string publicKeyString)
            {
                //get a stream from the string
                var sr = new StringReader(publicKeyString);
                //we need a deserializer
                var xs = new XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                _publicKey = (RSAParameters)xs.Deserialize(sr);
            }

            /// <summary>
            /// Save the public key to a file.
            /// It is better to pass the path as a XML file.
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool SavePublicKey(string path)
            {
                try
                {
                    File.WriteAllText(path, PublicKey());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            /// <summary>
            /// converting the private key into a string representation
            /// </summary>
            /// <returns>string</returns>
            public string PrivateKey()
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(typeof(RSAParameters));
                xs.Serialize(sw, _privateKey);
                return sw.ToString();
            }

            /// <summary>
            /// coverting a string representation into the private key (converting it back)
            /// </summary>
            /// <returns>void</returns>
            public void PrivateKey(string privateKeyString)
            {
                //get a stream from the string
                var sr = new StringReader(privateKeyString);
                //we need a deserializer
                var xs = new XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                _privateKey = (RSAParameters)xs.Deserialize(sr);
            }

            /// <summary>
            /// Save the private key to a file.
            /// It is better to pass the path as a XML file.
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool SavePrivateKey(string path)
            {
                try
                {
                    File.WriteAllText(path, PrivateKey());
                    return true;
                }
                catch (Exception ex)
                {
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