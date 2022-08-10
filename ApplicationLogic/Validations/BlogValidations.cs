using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.ApplicationLogic.Validations
{
    class BlogValidations
    {
        public static bool IsValidTitle(string title)
        {
            if (Validation.IsLengthBetween(title,5,35))
            {
                return true;
            }
            return false;
        }
        public static bool IsValidContent(string content)
        {
            if (Validation.IsLengthBetween(content, 5, 100))
            {
                return true;
            }
            return false;
        }
    }
}
