using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.ApplicationLogic.Validations
{
    class BlogValidations
    {
        public bool IsValidTitle(string text)
        {
            if (Validation.IsLengthBetween(text, 5, 35))
            {
                return true;
            }
            Console.WriteLine("Length is not correct.");
            return false;
        }

        public bool IsValidContent(string content)
        {
            if (Validation.IsLengthBetween(content, 5, 100))
            {
                return true;
            }
            Console.WriteLine("Length is not correct.");
            return false;
        }
    }
}
