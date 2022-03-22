using Core.Utilites.Security.Cryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Helpers
{
    public static class PasswordHelper
    {
        private static string PasswordString = "6gYEYrduisvEbg3BSxSjDVN4sGCeP9SSFwlfc8eaAajkGroL";
        //For validate password
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";
            return CryptionHelper.Encrypt(Convert.FromBase64String(PasswordString), password);
        }

        public static bool PasswordControl(string encryptedPassword, string password)
        {
            return CryptionHelper.Encrypt(Convert.FromBase64String(PasswordString), password) == encryptedPassword;
        }
    }
}
