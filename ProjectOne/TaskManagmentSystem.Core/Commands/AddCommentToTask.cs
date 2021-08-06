using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    class AddCommentToTask : BaseCommand
    {
        private const int numberOfParameters = 4;
        //addcommenttotask [teamname] [id] [comment] [author]
        public AddCommentToTask(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = CommandParameters[0];
            int itemID = int.Parse(CommandParameters[1]);
            string content = CommandParameters[2];
            string author = CommandParameters[3];

            if (!this.Repository.IsTeamMember(this.Repository.GetTeam(teamName), this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }
            var task = this.Repository.FindTaskByID(itemID) ?? throw new UserInputException("Task doesn't exsist");

            task.AddComment(new Comment(content, author));
            return $"Comment was added";
        }
    }
}
