using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;

namespace ASG.Common
{
    public  class Security
    {
        private static string AES_KEY = "axQ8kBnPjcmBcPWcmcKwpQ==";
        private static string AES_IV = "xzyLMjDtHu5kHwHYxEiOvg==";

        public Security()
        {

        }

        public static string Encrypt(string source)
        {
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            aesProvider.KeySize = 128;
            aesProvider.Key = Convert.FromBase64String(AES_KEY);
            aesProvider.IV = Convert.FromBase64String(AES_IV);

            string encryptedString;

            MemoryStream stream = new MemoryStream();
            BinaryFormatter serializer = new BinaryFormatter();

            CryptoStream cryptostream = new CryptoStream(stream, aesProvider.CreateEncryptor(aesProvider.Key, aesProvider.IV), CryptoStreamMode.Write);

            serializer.Serialize(cryptostream, source);
            cryptostream.FlushFinalBlock();

            encryptedString = Convert.ToBase64String(stream.ToArray());

            cryptostream.Close();

            return encryptedString;
        }

        public static string Decrypt(string source)
        {
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            aesProvider.KeySize = 128;
            aesProvider.Key = Convert.FromBase64String(AES_KEY);
            aesProvider.IV = Convert.FromBase64String(AES_IV);

            string deCypheredText = string.Empty;

            if (source != null || source.Length > 0)
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter deserializer = new BinaryFormatter();

                CryptoStream cryptostream = new CryptoStream(stream, aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV), CryptoStreamMode.Read);

                Byte[] buffer = Convert.FromBase64String(source);

                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;

                deCypheredText = (string)deserializer.Deserialize(cryptostream);

                cryptostream.Close();
            }

            return deCypheredText;
        }
    }
}