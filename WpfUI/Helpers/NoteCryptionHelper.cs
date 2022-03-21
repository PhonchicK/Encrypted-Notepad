using Core.Utilites.Security.Cryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Helpers
{
    public static class NoteCryptionHelper
    {
        public static string EncryptText(string text, string password)
        {
            if (string.IsNullOrEmpty(password))
                return text;
            return CryptionHelper.Encrypt(UTF8Encoding.UTF8.GetBytes(text), password);
        }
        public static string DecryptText(string encryptedText, string password)
        {
            if (string.IsNullOrEmpty(password))
                return encryptedText;
            return UTF8Encoding.UTF8.GetString(CryptionHelper.Decrypt(encryptedText, password));
        }
        public static string EncryptFile(byte[] fileByteArray, string password = "")
        {
            return string.IsNullOrEmpty(password) ? Convert.ToBase64String(fileByteArray) : 
                CryptionHelper.Encrypt(fileByteArray, password);
        }
        public static byte[] DecryptFile(string encryptedText, string password = "")
        {
            return string.IsNullOrEmpty(password) ? Convert.FromBase64String(encryptedText) : 
                CryptionHelper.Decrypt(encryptedText, password);
        }
    }
}
