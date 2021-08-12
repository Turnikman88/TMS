using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Enums;

namespace TaskManagmentSystem.Core.Commands
{
    public class RemoveComment : BaseCommand
    {
        private const int numberOfParameters = 3;

        public RemoveComment(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
            
        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            
            string teamIdentificator = CommandParameters[0];
            int itemID = ParseIntParameter(CommandParameters[1]);
            string uniqueCommentPart = CommandParameters[2];

            var user = this.Repository.LoggedUser;
            var team = this.Repository.GetTeam(teamIdentificator);
            var task = this.Repository.GetTaskById(itemID);
            var comment = task.Comments.Where(x => x.Content.Contains(uniqueCommentPart)).FirstOrDefault()
                ?? throw new UserInputException(Constants.COMMENT_NOT_FOUND);

            /*if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }*/
            if (user.Role == Role.Normal && user.Name != comment.Author)
            {
                throw new UserInputException(Constants.YOU_ARE_NOT_ALLOWED_TO_REMOVE);
            }
            task.RemoveComment(comment);

            return $"Comment was removed!";

        }
    }
}
