using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateBoardCommand : BaseCommand
    {
        private const int numberOfParameters = 2;

        public CreateBoardCommand(List<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, this.CommandParameters.Count);
            string boardName = CommandParameters[0];
            ITeam team = null;
            if (int.TryParse(CommandParameters[1], out int temaId))
            {
                team = this.Repository.FindTeamById(temaId);
            }
            else
            {
                string teamName = CommandParameters[1];
                team = this.Repository.FindTeamByName(teamName);
            }

            //ToDo: Validate if current user is member of the team 
            var board = this.Repository.CreateBoard(boardName);
            team.AddBoard(board);
            return $"Board with name {board.Name} was created";
        }
    }
}
