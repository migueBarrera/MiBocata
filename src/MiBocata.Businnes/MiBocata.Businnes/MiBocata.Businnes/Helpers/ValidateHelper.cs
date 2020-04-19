using System;
using System.Net.Mail;

namespace MiBocata.Businnes.Helpers
{
    public class ValidateHelper
    {
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidEmail(string email)
        {
            if (IsEmpty(email))
            {
                return false;
            }

            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException e)
            {
                System.Console.WriteLine(e.Message);

                return false;
            }
        }

        public static bool IsValidPassword(string pass)
        {
            return !IsEmpty(pass) && pass.Length >= 4;
        }
    }
}
