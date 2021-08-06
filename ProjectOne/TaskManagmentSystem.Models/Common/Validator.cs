using System;
using System.Text.RegularExpressions;
using TaskManagmentSystem.Models.Enums;
namespace TaskManagmentSystem.Models.Common
{
    public static class Validator
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
            if (obj is null || obj.ToString() == "")
            {
                throw new UserInputException(message);
            }
        }
        public static int ParseIntParameter(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new UserInputException(string.Format(Constants.PARSE_INT_ERR, value));
        }
        public static void ValidateParametersCount(int expected, int recived)
        {
            if (expected != recived)
            {
                throw new UserInputException(string.Format(Constants.NUM_OF_PARAMETERS_ERR, expected, recived));
            }
        }
        public static Role ParseRole(string role)
        {
            if (Enum.TryParse<Role>(role, true, out var result))
            {
                return result;
            }
            throw new UserInputException("This type of role doesn't exsist!");
        }
        public static void ValidatePattern(string value, string pattern, string message)
        {
            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
            {
                throw new UserInputException(message);
            }
        }
    }
}
