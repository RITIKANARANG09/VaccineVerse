
using System.Text.RegularExpressions;

namespace Project
{
    internal class RegexValid
    {
        private static string pattern;
        static RegexValid()
        {
            pattern = @"^(?:\+91)?[6-9][0-9]{9}$";
        }
        public static bool PhoneNoVerify(string phoneNo)
        {
            if (Regex.IsMatch(phoneNo, pattern))
            {
                return true;
            }
            ExceptionController.Message("Invalid credentials");
            return false;
        }
    }
}
