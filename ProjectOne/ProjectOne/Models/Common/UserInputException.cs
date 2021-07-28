using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Common
{
    public class UserInputException : ApplicationException
    {
        public UserInputException(string message)
            :base(message)
        {

        }
    }
}
