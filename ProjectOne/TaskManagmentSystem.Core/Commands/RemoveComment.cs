using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

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

            var team = this.Repository.GetTeam(teamIdentificator);
            var task = this.Repository.GetTaskById(itemID);
            var comment = task.Comments.Where(x => x.Content.Contains(uniqueCommentPart)).FirstOrDefault()
                ?? throw new UserInputException(Constants.COMMENT_NOT_FOUND);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            task.RemoveComment(comment);

            return $"Comment was removed!";

        }
    }
}
