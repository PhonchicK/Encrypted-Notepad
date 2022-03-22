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
        #region Text Cryption
        public static string EncryptText(string text, string password)
        {
            if (string.IsNullOrEmpty(password))
                return text;
            return CryptionHelper.Encrypt(Convert.FromBase64String(text), password);
        }
        public static string DecryptText(string encryptedText, string password)
        {
            if (string.IsNullOrEmpty(password))
                return encryptedText;
            if (string.IsNullOrEmpty(encryptedText))
                return "";
            byte[] data = CryptionHelper.Decrypt(Convert.FromBase64String(encryptedText), password);
            return Convert.ToBase64String(data, 0, data.Length);
        }
        #endregion
        #region File Cryption
        public static string EncryptFile(byte[] fileByteArray, string password = "")
        {
            return string.IsNullOrEmpty(password) ? Convert.ToBase64String(fileByteArray) : 
                CryptionHelper.Encrypt(fileByteArray, password);
        }

        public static byte[] DecryptFile(string encryptedText, string password = "")
        {
            return string.IsNullOrEmpty(password) ? Convert.FromBase64String(encryptedText) : 
                CryptionHelper.Decrypt(Convert.FromBase64String(encryptedText), password);
        }
        #endregion
    }
}
