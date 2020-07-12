using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MikeUpjohnWebPortfolioV2CMS.Code
{
    // Adapted from http://tekeye.biz/2015/encrypt-decrypt-c-sharp-string
    public static class Encryption
    {
        private static string hashKey = "nh3&^NJD9&(*";
        private static string hashedPassKey = "njkfds43-*&£";
        private static int keysize = 256;

        public static string EncryptString(int textString)
        {
            return Encrypt(textString, hashedPassKey);
        }

        private static string Encrypt(int textString, string passKey)
        {
            byte[] hashKeyBytes = Encoding.UTF8.GetBytes(hashKey);
            byte[] textStringBytes = Encoding.UTF8.GetBytes(textString.ToString());
            PasswordDeriveBytes password = new PasswordDeriveBytes(passKey, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, hashKeyBytes);

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(textStringBytes, 0, textStringBytes.Length);
            cryptoStream.Flush();

            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherTextBytes);
        }

        public static int DecryptString(string cipherText)
        {
            return Decrypt(cipherText, hashedPassKey);
        }

        private static int Decrypt(string cipherText, string passKey)
        {
            byte[] hashKeyBytes = Encoding.ASCII.GetBytes(hashKey);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passKey, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, hashKeyBytes);

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] textStringBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(textStringBytes, 0, textStringBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();

            return int.Parse(Encoding.UTF8.GetString(textStringBytes, 0, decryptedByteCount));
        }
    }
}
