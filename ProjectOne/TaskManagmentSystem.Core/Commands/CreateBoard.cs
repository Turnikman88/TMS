using System;
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

            ITeam team = GetTeam(teamIdentificator);
            Validator.ValidateObjectIsNotNULL(team, string.Format(Constants.TEAM_DOESNT_EXSIST, teamIdentificator));
            IsTeamMember(team, this.Repository.LoggedUser);            

            var board = this.Repository.CreateBoard(boardName);
            team.AddBoard(board);

            return $"Board with name {board.Name} was created"; //Ask Kalin
        }


    }
}
