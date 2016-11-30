using System;
using System.Security.Cryptography;

namespace RustBotCSharp.GUI
{
    public class Encryption
    {
        public static string EncryptString(string message, string passphrase)
        {
            if (message.Length == 0)
                return "";

            try
            {
                byte[] results;
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
                MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
                byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));

                TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider
                {
                    Key = tdesKey,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                byte[] dataToEncrypt = utf8.GetBytes(message);

                try
                {
                    ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
                    results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                }
                finally
                {
                    tdesAlgorithm.Clear();
                    hashProvider.Clear();
                }

                return Convert.ToBase64String(results);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string DecryptString(string message, string passphrase)
        {
            if (message.Length == 0)
                return "";

            try
            {
                byte[] results;
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
                MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
                byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));

                TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider
                {
                    Key = tdesKey,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                byte[] dataToDecrypt = Convert.FromBase64String(message);

                try
                {
                    ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                    results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                }
                finally
                {
                    tdesAlgorithm.Clear();
                    hashProvider.Clear();
                }

                return utf8.GetString(results);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
