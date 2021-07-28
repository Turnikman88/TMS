namespace ProjectOne.Models.Common
{
    public class Constants
    {
        public const string STRING_LENGHT_ERR = "{0} must be between {1} and {2} symbols long!";


        public const string NAME_UNIQUE_ERR = "The name {0} already exists!";
        public const string ITEM_NULL_ERR = "{0} cannot be null or empty!";

        public const int TEAM_NAME_MIN_SYMBOLS = 5;
        public const int TEAM_NAME_MAX_SYMBOLS = 15;

        public const int MEMBER_NAME_MIN_SYMBOLS = 5;
        public const int MEMBER_NAME_MAX_SYMBOLS = 15;
        public const string MEMBER_FIRST_TASK_NULL = "Member's Task cannot be NULL or empty. Members should have at least one task";

        public const int BOARD_NAME_MIN_SYMBOLS = 5;
        public const int BOARD_NAME_MAX_SYMBOLS = 10;

        public const int TITLE_MIN_SYMBOLS = 10;
        public const int TITLE_MAX_SYMBOLS = 50;

        public const int DESCRIPTION_MIN_SYMBOLS = 10;
        public const int DESCRIPTION_MAX_SYMBOLS = 500;

    }
}
