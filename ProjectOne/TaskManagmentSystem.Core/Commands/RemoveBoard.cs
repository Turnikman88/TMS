using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class RemoveBoard : BaseCommand
    {
        private const int numberOfParameters = 2;

        public RemoveBoard(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string boardIdentificator = CommandParameters[0];
            string teamIdentificator = CommandParameters[1];

            var team = this.Repository.GetTeam(teamIdentificator);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            var board = this.Repository.GetBoard(boardIdentificator);
            team.RemoveBoard(board);
            return $"Board with name {board.Name}, ID: {board.Id} was removed!";
        }
    }
}
