using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateBoard : BaseCommand
    {
        private const int numberOfParameters = 2;

        public CreateBoard(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, this.CommandParameters.Count);

            string boardName = CommandParameters[0];
            string teamIdentificator = CommandParameters[1];

            ITeam team = this.Repository.GetTeam(teamIdentificator);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }
            if (team.Boards.Any(x => x.Name == boardName))
            {
                throw new UserInputException(Constants.NAME_MUST_BE_UNIQUE);
            }
            var board = this.Repository.CreateBoard(boardName);
            team.AddBoard(board);

            return $"Board with name {board.Name}, ID: {board.Id} was created";
        }


    }
}
