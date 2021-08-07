namespace TaskManagmentSystem.Models.Common
{
    public class Constants
    {
        public const string CORE_ASSEMBLY_KEY = "Core";
        public const string MODELS_ASSEMBLY_KEY = "Models";

        public const string TerminationCommand = "exit";
        public const string EmptyCommandError = "Command cannot be empty!";
        public const string EVENT_WAS_CREATED = "{0} event with ID: {1} was created!";

        public const string STRING_LENGHT_ERR = "{0} must be between {1} and {2} symbols long!";


        public const string NAME_UNIQUE_ERR = "The name {0} already exists!";
        public const string ITEM_NULL_ERR = "{0} cannot be null or empty!";

        public const string NAME_MUST_BE_UNIQUE = "Name must be unique!";
        public const string GIVEN_TYPE_ERR = "Given {0} type doesn't exsits!";
        public const int TEAM_NAME_MIN_SYMBOLS = 5;
        public const int TEAM_NAME_MAX_SYMBOLS = 15;

        public const int MEMBER_NAME_MIN_SYMBOLS = 5;
        public const int MEMBER_NAME_MAX_SYMBOLS = 15;
        public const string MEMBER_FIRST_TASK_NULL = "Member's Task cannot be NULL or empty. Members should have at least one task!";

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

        public const string PARSE_INT_ERR = "Invalid value for {0}. Should be an integer number!";
        public const string NUM_OF_PARAMETERS_ERR = "Invalid number of arguments. Expected: {0}, Received: {1}";
        public const string TASK_TYPE_ERR = "Task of type {0} doesnt exist";
        public const string PRINT_INFO_SEPARATOR = "--------------";
        public const string INVALID_COMMAND_ERR = "Command with name: {0} doesn't exist!";

        public const string PASSWORD_PATTERN = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";
        public const string PASSWORD_CHANGE_ERR = "Password doesn't match!";
        public const string PASSWORD_CHANGED_SUCC = "Password succsessfully changed from {0} to {1}";
        public const string WRONG_PASSWORD = "Wrong password!";
        public const string PASSWORD_PATTERN_ERR = "Password must be at least 8 characters long and containse at least one upper-case, special symbol and digit!";

        public const string WRONG_USERNAME = "Wrong username!";
        public const string USER_ALREADY_EXIST = "User {0} already exist. Choose a different username!";
        public const string USER_NOT_LOGGED_IN = "You are not logged!";
        public const string THIS_USER_LOGGED_IN = "You are already logged!";
        public const string USER_LOGGED_OUT = "You logged out!";
        public const string NO_USER_LOGGED = "You can't log out, no one is logged!";
        public const string USER_LOGGED_IN = "User {0} successfully logged in!";
        public const string USER_LOGGED_IN_ALREADY = "User {0} is logged in! Please log out first!";
        public const string USER_DOESNT_EXSIST = "User {0} doesn't exsist";
        public const string USER_NOT_ROOT = "You are not the owner!";

        public const string MEMBER_NOT_IN_TEAM = "User {0} is not member of the team!";
        public const string MEMBER_ALREADY_IN_TEAM = "User {0} is already member of the team";
        public const string MEMBER_NOT_ALLOWED_JOINING = "You are not allowed to join!";

        public const string TEAM_DOESNT_EXSIST = "Team {0} doesn't exsist!";
        public const string BOARD_DOESNT_EXSIST = "Board {0} doesn't exsist!";
        public const string TASK_DOESNT_EXSIST = "Task {0} doesn't exsist!";

        public const string BOARS_ALREADY_EXIST = "Board {0} already exist. Choose a different name!";

        public const string PATH_TO_DATABASE = @"../.../../../../../TaskManagmentSystem.Core\DB\";

    }
}
