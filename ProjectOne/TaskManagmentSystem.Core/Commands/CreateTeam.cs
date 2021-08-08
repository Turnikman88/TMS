using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateTeam : BaseCommand
    {
        private const int numberOfParameters = 1;

        public CreateTeam(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = this.CommandParameters[0];
            var team = this.Repository.CreateTeam(teamName);

            return $"Team with name {team.Name}, ID: {team.Id} was created";
        }
    }
}
