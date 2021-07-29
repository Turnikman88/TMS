using ProjectOne.Commands.Contracts;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IRepository repository;
        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }
        public ICommand Create(string commandLine)
        {
            string[] arguments = commandLine.Split(); //ToDo: check why cant remove emptyentries
            string name = ExtractName(arguments);
            List<string> commandParameters = ExtractParameters(arguments);
            ICommand command = null;
            switch (name.ToLower())
            {
                case "createperson":
                    command = new CreatePersonCommand(commandParameters, repository);
                    break;
                case "showallpeople": // maybe only admin can do it
                    command = new ShowAllPeopleCommand(repository);
                    break;
                case "showpersonactivity":
                    command = new ShowPersonActivityCommand(commandParameters, repository);
                    break;
                case "createnewteam":
                    command = new CreateNewTeamCommand(commandParameters, repository);
                    break;
                case "showallteams": // maybe only admin can do it
                    command = new ShowAllTeamsCommand(repository);
                    break;
                case "showteamactivity":
                    command = new ShowTeamActivityCommand(commandParameters, repository);
                    break;
                case "addperson":  //adds person to team
                    command = new AddPersonToTeamCommand(commandParameters, repository);
                    break;
                case "showallmembers":  //shows all team members // we need validation if the user is member // one user can be member of more than one teams 
                    command = new ShowAllTeamMembersCommand(commandParameters, repository);
                    break;
                case "createboard":
                    command = new CreateBoardCommand(commandParameters, repository);
                    break;
                case "showteamboards":
                    command = new ShowTeamBoardsCommand(commandParameters, repository);
                    break;
                case "showboardactivity":
                    command = new ShowBoardActivityCommand(commandParameters, repository);
                    break;
                case "create":
                    command = new CreateCommand(commandParameters, repository);
                    break;
                case "change":
                    command = new ChangeCommand(commandParameters, repository);
                    break;
                case "assign":
                    command = new AssignCommand(commandParameters, repository);
                    break;
                case "addcomment":
                    command = new AddCommentToTask(commandParameters, repository);
                    break;

            }
            return command; //remove later
        }

        private List<string> ExtractParameters(string[] arguments)
        {
            List<string> commandParams = new List<string>();
            var list = commandParams.Skip(1).ToList();
            return list;
        }

        private string ExtractName(string[] arguments)
        {
            string nameOfCommand = arguments[0];
            return nameOfCommand;
        }
    }
}
