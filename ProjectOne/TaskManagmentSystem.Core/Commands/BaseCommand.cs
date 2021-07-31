using ProjectOne.Commands.Contracts;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand(IRepository repository)
                    : this(new List<string>(), repository)
        {
        }

        protected BaseCommand(IList<string> commandParameters, IRepository repository)
        {
            this.CommandParameters = commandParameters;
            this.Repository = repository;
        }

        public IList<string> CommandParameters { get; }
        public IRepository Repository { get; }

        public abstract string Execute();

        protected bool IsTeamMember(ITeam team, IMember user) //ToDo: Ask Kalin
        {
            Validator.ValidateObjectIsNotNULL(team.Members
                .FirstOrDefault(x => x.Name == user.Name)
                , string.Format(Constants.MEMBER_NOT_IN_TEAM, user.Name));
            return true;
        }
        protected IMember GetUser(string userIndicator)
        {
            IMember user;
            if (int.TryParse(userIndicator, out int userId))
            {
                user = this.Repository.FindUserById(userId);
            }
            else
            {
                user = this.Repository.FindUserByName(userIndicator);
            }

            return user;
        }

        protected ITeam GetTeam(string teamIdentificator)
        {
            ITeam team;
            if (int.TryParse(teamIdentificator, out int temaId))
            {
                team = this.Repository.FindTeamById(temaId);
            }
            else
            {
                team = this.Repository.FindTeamByName(teamIdentificator);
            }

            return team;
        }
        //maybe some Enum parser
    }
}
