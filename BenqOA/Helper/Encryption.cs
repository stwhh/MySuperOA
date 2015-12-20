/*****************************************************************************
 *
 * Filename: Encryption
 * Copyright (C) 2006 BenQ Corporation.All right reserved.
 * Creator:	Tager.Yang
 * Create Date: 2012-9-19 8:34:19
 * Modifier: 
 * Modify Date: 
 * Description: 
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace BenqOA.Helper
{
    /// <summary>
    /// DES3加密解密
    /// </summary>
    public class Des3
    {
        #region CBC模式**
        /// <summary>
        /// DES3 CBC模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="data">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] Des3EncodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            //复制于MSDN
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;             //默认值
                tdsp.Padding = PaddingMode.PKCS7;       //默认值
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);
                // Write the byte array to the crypto stream and flush it.
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();
                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();
                // Close the streams.
                cStream.Close();
                mStream.Close();
                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
        /// <summary>
        /// DES3 CBC模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="data">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        public static byte[] Des3DecodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                tdsp.Padding = PaddingMode.PKCS7;
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);
                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[data.Length];
                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
        #endregion
        #region ECB模式
        /// <summary>
        /// DES3 ECB模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] Des3EncodeECB(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);
                // Write the byte array to the crypto stream and flush it.
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();
                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();
                // Close the streams.
                cStream.Close();
                mStream.Close();
                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
        /// <summary>
        /// DES3 ECB模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        public static byte[] Des3DecodeECB(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);
                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[data.Length];
                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
        #endregion
        /// <summary>
        /// 类测试
        /// </summary>
        public static void Test()
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            //key为abcdefghijklmnopqrstuvwx的Base64编码
            byte[] key = Convert.FromBase64String("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4");
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };      //当模式为ECB时，IV无用
            byte[] data = utf8.GetBytes("中国ABCabc123");
            System.Console.WriteLine("ECB模式:");
            byte[] str1 = Des3.Des3EncodeECB(key, iv, data);
            byte[] str2 = Des3.Des3DecodeECB(key, iv, str1);
            System.Console.WriteLine(Convert.ToBase64String(str1));
            System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(str2));
            System.Console.WriteLine();
            System.Console.WriteLine("CBC模式:");
            byte[] str3 = Des3.Des3EncodeCBC(key, iv, data);
            byte[] str4 = Des3.Des3DecodeCBC(key, iv, str3);
            System.Console.WriteLine(Convert.ToBase64String(str3));
            System.Console.WriteLine(utf8.GetString(str4));
            System.Console.WriteLine();
        }
    }
    
   /// <summary>
   /// Summary description for EncryptAndDecrypt
   /// </summary>
  public class EncryptAndDecrypt
  {
     //默认密钥向量
      private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF }; 
   
      /// <summary>
      /// DES加密字符串
      /// </summary>
      /// <param name="encryptString">待加密的字符串
      /// <param name="encryptKey">加密密钥,要求为8位
      /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
      public static string EncryptDES(string encryptString, string encryptKey)
      {
          try
          {
              byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
              byte[] rgbIV = Keys;
              byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
              DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
              MemoryStream mStream = new MemoryStream();
              CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
              cStream.Write(inputByteArray, 0, inputByteArray.Length);
              cStream.FlushFinalBlock();
              return Convert.ToBase64String(mStream.ToArray());
          }
          catch
          {
              throw;
          }
      }
      /// <summary>
      /// DES解密字符串
      /// </summary>
      /// <param name="decryptString">待解密的字符串
      /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同
      /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
      public static string DecryptDES(string decryptString, string decryptKey)
      {
          try
          {
              byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
              byte[] rgbIV = Keys;
              byte[] inputByteArray = Convert.FromBase64String(decryptString);
              DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
              MemoryStream mStream = new MemoryStream();
              CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
              cStream.Write(inputByteArray, 0, inputByteArray.Length);
              cStream.FlushFinalBlock();
              return Encoding.UTF8.GetString(mStream.ToArray());
          }
          catch
          {
              throw;
          }
      } 
  }


    /// <summary>
    /// 加密算法处理类

    /// </summary>
    /// <remarks>
    /// <para>这里提供了五种加密算法，分别为Des、Rc2、TripleDes、Rijndael、Md5。</para>
    /// <para>
    ///	Md5算法无需设定秘钥，其余四种都需要设定秘钥且对秘钥的长度有一定的限制，Des算法为8位，Rc2算法为16位，TripleDes算法为24位，
    ///	Rijndael算法为32位，TripleDes算法的秘钥必须是一些特殊字符、字母、数字的混合体

    /// </para>
    /// </remarks>
    public class Encryption
    {
        #region variable

        private EncryptionAlgorithm m_alGid;
        private string IV_STRING = "DES";

        #endregion

        #region functions

        /// <summary>
        /// 加密算法构造函数，制定算法类型
        /// </summary>
        /// <param name="algid">加密算法的类型</param>
        public Encryption(EncryptionAlgorithm algid)
        {
            this.m_alGid = algid;
        }



        /// <summary>
        /// 加密函数
        /// </summary>
        /// <example>
        /// <code>
        /// Encryption oEncryption = new Encryption(EncryptionAlgorithm.TripleDes);
        /// string sVal = "encryption";
        /// string sKey = "123456781#$4567812345678";
        /// //加密
        /// string sResult = oEncryption.encrypt(sVal, sKey);
        /// System.Console.WriteLine(sResult);
        /// //解密
        /// sResult = oEncryption.decrypt(sResult, sKey);
        /// System.Console.WriteLine(sResult);
        /// </code>
        /// </example>
        /// <param name="encrytString">加密字符串</param>
        /// <param name="key">秘钥: Des算法为8位，Rc2算法为16位，TripleDes算法为24位且必须是一些特殊字符、字母、数字的混合体，Rijndael算法为32位，Md5无需设定传null之值</param>
        /// <returns>加密后的字符串</returns>
        public string encrypt(string encrytString, string key)
        {
            Encryptor ency = new Encryptor(this.m_alGid);
            if (this.m_alGid.Equals(EncryptionAlgorithm.Rijndael))
                ency.IV = Encoding.ASCII.GetBytes(this.convertString(this.IV_STRING, 16));
            else
                ency.IV = Encoding.ASCII.GetBytes(this.convertString(this.IV_STRING, 8));
            return Convert.ToBase64String(ency.Encrypt(Encoding.ASCII.GetBytes(encrytString),
                Encoding.ASCII.GetBytes(this.getCorrectKeyString(key, this.m_alGid))));
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="encrytString">加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public string encrypt(string encrytString)
        {
            return this.encrypt(encrytString, "123456%W0rdPa$$");
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <code>
        /// Encryption oEncryption = new Encryption(EncryptionAlgorithm.TripleDes);
        /// string sVal = "encryption";
        /// string sKey = "123456781#$4567812345678";
        /// //加密
        /// string sResult = oEncryption.encrypt(sVal, sKey);
        /// System.Console.WriteLine(sResult);
        /// //解密
        /// sResult = oEncryption.decrypt(sResult, sKey);
        /// System.Console.WriteLine(sResult);
        /// </code>
        /// <param name="encrytedString">解密字符串</param>
        /// <param name="key">秘钥: Des算法为8位，Rc2算法为16位，TripleDes算法为24位且必须是一些特殊字符、字母、数字的混合体，Rijndael算法为32位，Md5无需设定传null之值</param>
        /// <returns>加密后的字符串</returns>
        public string decrypt(string encrytedString, string key)
        {
            Decryptor decy = new Decryptor(this.m_alGid);
            if (this.m_alGid.Equals(EncryptionAlgorithm.Rijndael))
                decy.IV = Encoding.ASCII.GetBytes(this.convertString(this.IV_STRING, 16));
            else
                decy.IV = Encoding.ASCII.GetBytes(this.convertString(this.IV_STRING, 8));
            return Encoding.ASCII.GetString(decy.Decrypt(Convert.FromBase64String(encrytedString),
                Encoding.ASCII.GetBytes(this.getCorrectKeyString(key, this.m_alGid))));
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="encrytedString">解密字符串</param>
        /// <returns>加密后的字符串</returns>
        public string decrypt(string encrytedString)
        {
            return this.decrypt(encrytedString, "123456%W0rdPa$$");
        }
        /// <summary>
        /// this function provide to get the correct key string 
        /// </summary>
        /// <param name="val">key</param>
        /// <param name="algid">EncryptionAlgorithm</param>
        /// <returns>correct key string</returns>
        private string getCorrectKeyString(string val, EncryptionAlgorithm algid)
        {
            if (val == null)
                val = "BenQ.Public.EPS";
            switch (algid)
            {
                case EncryptionAlgorithm.Des:
                    return this.convertString(val, 8);
                case EncryptionAlgorithm.Rc2:
                    return this.convertString(val, 16);
                case EncryptionAlgorithm.TripleDes:
                    return this.convertString(val, 24);
                case EncryptionAlgorithm.Rijndael:
                    return this.convertString(val, 32);
                case EncryptionAlgorithm.Md5:
                    return this.convertString(val, 8);
                default:
                    return null;
            }
        }

        /// <summary>
        /// this function provide to convert string by certain length
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="len">length</param>
        /// <returns>certain length string</returns>
        private string convertString(string val, int len)
        {
            if (val.Length == len)
                return val;
            if (val.Length > len)
                return val.Substring(0, len);
            if (val.Length < len)
            {
                int n = len - val.Length;
                for (int i = 0; i < n; i++)
                {
                    val += "s";
                    if (val.Length == len)
                        return val;
                }
            }
            return val;
        }
        #endregion
    }

    /// <summary>
    /// Encryptor
    /// </summary>
    internal class Encryptor
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="algId">Encryption Algorithm</param>
        public Encryptor(EncryptionAlgorithm algId)
        {
            transformer = new EncryptTransformer(algId);
        }

        private EncryptTransformer transformer;
        private byte[] initVec;
        private byte[] encKey;

        /// <summary>
        /// get or set iv
        /// </summary>
        public byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }

        /// <summary>
        /// get or set key
        /// </summary>
        public byte[] Key
        {
            get { return encKey; }
        }

        /// <summary>
        /// this function provide to encrypt the data
        /// </summary>
        /// <param name="bytesData">data</param>
        /// <param name="bytesKey">key</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
        {
            // 设置将保存加密数据的流

            MemoryStream memStreamEncryptedData = new MemoryStream();

            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream encStream = new CryptoStream(memStreamEncryptedData, transform, CryptoStreamMode.Write);
            try
            {
                // 加密数据，并将它们写入内存流
                encStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("将加密数据写入流时出错： \n" + ex.Message);
            }
            // 为客户端进行检索设置初始化向量和密钥

            encKey = transformer.Key;
            initVec = transformer.IV;
            encStream.FlushFinalBlock();
            encStream.Close();

            // 发送回数据
            return memStreamEncryptedData.ToArray();
            // end Encrypt
        }
    }

    /// <summary>
    /// 加密算法枚举类型
    /// </summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>
        /// Des算法
        /// </summary>
        Des = 1,
        /// <summary>
        /// Rc2算法
        /// </summary>
        Rc2,
        /// <summary>
        /// Rijndael算法
        /// </summary>
        Rijndael,
        /// <summary>
        /// TripleDes算法
        /// </summary>
        TripleDes,
        /// <summary>
        /// Md5算法
        /// </summary>
        Md5
    };

    /// <summary>
    /// EncryptTransformer
    /// </summary>
    internal class EncryptTransformer
    {
        #region -- 构造函数 --
        internal EncryptTransformer(EncryptionAlgorithm algId)
        {
            // 保存正在使用的算法

            algorithmID = algId;
        }
        #endregion

        #region -- 私有变量 --
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;
        private byte[] encKey;
        #endregion

        #region -- 公共属性 --
        /// <summary>
        /// get or set iv
        /// </summary>
        internal byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }
        /// <summary>
        /// get or set key
        /// </summary>
        internal byte[] Key
        {
            get { return encKey; }
        }
        #endregion

        #region -- 公共方法 --
        /// <summary>
        /// this function provide to Get CryptoServiceProvider 
        /// </summary>
        /// <param name="bytesKey">key</param>
        /// <returns>CryptoServiceProvider</returns>
        internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            // 选取提供程序。

            switch (algorithmID)
            {
                case EncryptionAlgorithm.Des:
                    {
                        DES des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;

                        // 查看是否提供了密钥

                        if (null == bytesKey)
                        {
                            encKey = des.Key;
                        }
                        else
                        {
                            des.Key = bytesKey;
                            encKey = des.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            // 让算法创建一个

                            initVec = des.IV;
                        }
                        else
                        {
                            //不，将它提供给算法

                            des.IV = initVec;
                        }
                        return des.CreateEncryptor();
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES des3 = new TripleDESCryptoServiceProvider();
                        des3.Mode = CipherMode.CBC;
                        // See if a key was provided
                        if (null == bytesKey)
                        {
                            encKey = des3.Key;
                        }
                        else
                        {
                            des3.Key = bytesKey;
                            encKey = des3.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            //是，让算法创建一个

                            initVec = des3.IV;
                        }
                        else
                        {
                            //不，将它提供给算法。

                            des3.IV = initVec;
                        }
                        return des3.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc2 = new RC2CryptoServiceProvider();
                        rc2.Mode = CipherMode.CBC;
                        // 测试是否提供了密钥

                        if (null == bytesKey)
                        {
                            encKey = rc2.Key;
                        }
                        else
                        {
                            rc2.Key = bytesKey;
                            encKey = rc2.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            //是，让算法创建一个

                            initVec = rc2.IV;
                        }
                        else
                        {
                            //不，将它提供给算法。

                            rc2.IV = initVec;
                        }
                        return rc2.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        rijndael.Mode = CipherMode.CBC;
                        // 测试是否提供了密钥

                        if (null == bytesKey)
                        {
                            encKey = rijndael.Key;
                        }
                        else
                        {
                            rijndael.Key = bytesKey;
                            encKey = rijndael.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            // 是，让算法创建一个

                            initVec = rijndael.IV;
                        }
                        else
                        {
                            // 不，将它提供给算法。

                            rijndael.IV = initVec;
                        }
                        return rijndael.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Md5:
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();
                        return md5;
                    }
                default:
                    {
                        throw new CryptographicException("算法 ID '" + algorithmID + "' 不受支持");
                    }
            }
        }
        #endregion
    }

    /// <summary>
    /// Decryptor
    /// </summary>
    internal class Decryptor
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="algId">EncryptionAlgorithm</param>
        public Decryptor(EncryptionAlgorithm algId)
        {
            transformer = new DecryptTransformer(algId);
        }

        private DecryptTransformer transformer;
        private byte[] initVec;

        public byte[] IV
        {
            set { initVec = value; }
        }

        /// <summary>
        /// decrypt
        /// </summary>
        /// <param name="bytesData">data</param>
        /// <param name="bytesKey">key</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
        {
            // 为解密数据设置内存流
            MemoryStream memStreamDecryptedData = new MemoryStream();

            // 传递初始化向量
            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream decStream = new CryptoStream(memStreamDecryptedData, transform, CryptoStreamMode.Write);
            try
            {
                decStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("将加密数据写入流时出错： \n" + ex.Message);
            }
            decStream.FlushFinalBlock();
            decStream.Close();
            // 发送回数据
            return memStreamDecryptedData.ToArray();
            // end Decrypt
        }
    }

    /// <summary>
    /// DecryptTransformer
    /// </summary>
    internal class DecryptTransformer
    {
        #region -- 构造函数 --
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="deCryptId">EncryptionAlgorithm</param>
        internal DecryptTransformer(EncryptionAlgorithm deCryptId)
        {
            algorithmID = deCryptId;
        }
        #endregion

        #region -- 私有变量 --
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;
        #endregion

        #region -- 公共属性 --
        /// <summary>
        /// set iv
        /// </summary>
        internal byte[] IV
        {
            set { initVec = value; }
        }
        #endregion

        #region -- 公共方法 --
        /// <summary>
        /// this function provide to GetCryptoServiceProvider
        /// </summary>
        /// <param name="bytesKey">key</param>
        /// <returns></returns>
        internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            // Pick the provider.
            switch (algorithmID)
            {
                case EncryptionAlgorithm.Des:
                    {
                        DES des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;
                        des.Key = bytesKey;
                        des.IV = initVec;
                        return des.CreateDecryptor();
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES des3 = new TripleDESCryptoServiceProvider();
                        des3.Mode = CipherMode.CBC;
                        return des3.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc2 = new RC2CryptoServiceProvider();
                        rc2.Mode = CipherMode.CBC;
                        return rc2.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        rijndael.Mode = CipherMode.CBC;
                        return rijndael.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Md5:
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();
                        return md5;
                    }
                default:
                    {
                        throw new CryptographicException("算法 ID '" + algorithmID + "' 不支持");
                    }
            }
            //end GetCryptoServiceProvider
        }
        #endregion
    }
}
