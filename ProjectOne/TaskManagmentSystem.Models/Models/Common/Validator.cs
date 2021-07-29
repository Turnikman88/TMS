using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Common
{
    public class Validator
    {
        public static void ValidateRange(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new UserInputException(message);
            }
        }

        public static void ValidateNameUniqueness(string name)
        {
            //TODO: Validate Name Uniqueness
        }

        public static void ValidateObjectIsNotNULL(object obj, string message)
        {
            if(obj is null)
            {
                throw new UserInputException(message);
            }
        }
    }
}
