using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValidEmail(this string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return false;
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(emailRegex);
            return re.IsMatch(Email);
        }
        public static bool IsValidString(this string Email)
        {
            return string.IsNullOrWhiteSpace(Email);
        }
        public static bool IsValidPassword(this string Password)
        {
            if (string.IsNullOrEmpty(Password))
                return false;
            return Password.Length >= 8;
        }
    }
}
