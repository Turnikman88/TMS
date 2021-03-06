namespace TaskManagmentSystem.Models.Common
{
    public class Constants
    {

        //==================ERRORS===================//

        public const string CANNOT_CHANGE_OWN_ROLE_ERR = "You cannot change your own role";
        public const string STRING_LENGHT_ERR = "{0} must be between {1} and {2} symbols long!";
        public const string ITEM_NULL_ERR = "{0} cannot be null or empty!";
        public const string NAME_UNIQUE_ERR = "The name {0} already exists!";
        public const string GIVEN_TYPE_ERR = "Given {0} type doesn't exsits!";
        public const string GIVEN_STATUS_TYPE_ERR = "Status of type {0} doesn't exist!";
        public const string MEMBER_FIRST_TASK_NULL_ERR = "Member's Task cannot be NULL or empty. Members should have at least one task!";
        public static string RATING_OUTOFRANGE_ERR = $"Rating must be between {RATING_MIN_VALUE} and {RATING_MAX_VALUE}";
        public const string STATUS_ADVANCE_ERROR = "Status cannot be Advanced! Current status {0}!";
        public const string STATUS_REVERT_ERROR = "Status cannot be Reverted! Current status {0}!";
        public const string PARSE_INT_ERR = "Invalid value for {0}. Should be an integer number!";
        public const string NUM_OF_PARAMETERS_ERR = "Invalid number of arguments. Expected: {0}, Received: {1}!";
        public const string TASK_TYPE_ERR = "Task of type {0} doesnt exist!";
        public const string INVALID_COMMAND_ERR = "Command with name: {0} doesn't exist!";
        public const string EMPTY_COMMAND_ERR = "Command cannot be empty!";
        public const string PASSWORD_CHANGE_ERR = "Password doesn't match!";
        public const string PASSWORD_PATTERN_ERR = "Password must be at least 8 characters long and containse at least one upper-case, special symbol and digit!";
        public const string FEEDBACKS_CANNOT_BE_ASSIGNED_ERR = "Feedbacks cannot be assigned";
        public const string EXPORT_COMMAND_ERR = "Export command needs [teamname/id] (optional type[board or user] [board/userID]) - exports history events to desktop";
        public const string SHOWALLTASK_COMMAND_ERR = "showalltasks command needs four keywords [teamname/id] ([filter] [title] or [sortby] title)";

        //==================OTHERS===================//

        public const string CORE_ASSEMBLY_KEY = "Core";
        public const string MODELS_ASSEMBLY_KEY = "Models";
        public const string TerminationCommand = "exit";

        //==================SEPARATORS===================//

        public const string PRINT_INFO_SEPARATOR = "--------------";

        //==================RANGE===================//

        public const int TEAM_NAME_MIN_SYMBOLS = 5;
        public const int TEAM_NAME_MAX_SYMBOLS = 15;

        public const int MEMBER_NAME_MIN_SYMBOLS = 5;
        public const int MEMBER_NAME_MAX_SYMBOLS = 15;

        public const int BOARD_NAME_MIN_SYMBOLS = 5;
        public const int BOARD_NAME_MAX_SYMBOLS = 10;

        public const int TITLE_MIN_SYMBOLS = 10;
        public const int TITLE_MAX_SYMBOLS = 50;

        public const int DESCRIPTION_MIN_SYMBOLS = 10;
        public const int DESCRIPTION_MAX_SYMBOLS = 500;

        public const int RATING_MIN_VALUE = 0;
        public const int RATING_MAX_VALUE = 100;

        //==================PATTERNS===================//

        public const string PASSWORD_PATTERN = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";

        //==================COMMON===================//

        public const string HISTORY_FILE_CREATED = "History for the team was created as text-file on your desktop under name {0}-{1}.txt";
        public const string DATABASE_HISTORY_DELETED = "Database history was deleted successfully";
        public const string EVENT_WAS_CREATED = "{0} event with ID: {1} was created!";
        public const string MEMBER_WAS_CREATED = "{0} with ID: {1} was created!";
        public const string NAME_MUST_BE_UNIQUE = "This name already exists! Please, choose another one!";
        public const string PASSWORD_CHANGED_SUCC = "Password succsessfully changed!";
        public const string WRONG_PASSWORD = "Wrong password!";
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
        public const string MEMBER_ALREADY_IN_TEAM = "User {0} is already member of the team!";
        public const string MEMBER_NOT_ALLOWED_JOINING = "You are not allowed to join!";
        public const string NON_EMPTY_DESCRIPTION = "Please provide a non-empty description";
        public const string YOU_ARE_NOT_ALLOWED_TO_REMOVE = "You are not allowed to remove it!";
        public const string TEAM_DOESNT_EXSIST = "Team {0} doesn't exsist!";
        public const string BOARD_DOESNT_EXSIST = "Board {0} doesn't exsist!";
        public const string TASK_DOESNT_EXSIST = "Task {0} doesn't exsist!";
        public const string TASK_NOT_ASSIGNED = "Task is not assigned to {0}!";
        public const string BOARS_ALREADY_EXIST = "Board {0} already exist. Choose a different name!";
        public const string COMMENT_NOT_FOUND = "Comment not found!";

        //==================PATHS===================//

        public const string PATH_TO_DATABASE = @"../.../../../../../TaskManagmentSystem.Core\DB\";
    }
}
