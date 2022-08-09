using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.ApplicationLogic.Validations
{
    public static class Validation
    {
        public static bool IsValidGender(string text)
        {
            if (text is "male" or "female")
            {
                return true;
            }
            Console.WriteLine("Gender is not correct!");
            return false;
        }

        public static bool IsLengthBetween(string text, int start, int end)
        {
            return text.Length >= start && text.Length < end;
        }

    }
}
