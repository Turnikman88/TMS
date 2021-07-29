namespace TaskManagmentSystem.Models.Common
{
    public class Constants
    {
        public const string TerminationCommand = "exit";
        public const string EmptyCommandError = "Command cannot be empty.";


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

        public const int RATING_MIN_VALUE = 0;
        public const int RATING_MAX_VALUE = 101;
        public static string RATING_OUTOFRANGE_ERR = $"Rating must be between {RATING_MIN_VALUE} and {RATING_MAX_VALUE}";

        public const string STATUS_ADVANCE_ERROR = "Status cannot be Advanced! Current status {0}!";
        public const string STATUS_REVERT_ERROR = "Status cannot be Reverted! Current status {0}!";

    }
}
