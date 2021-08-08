using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ExportHistory : BaseCommand
    {
        private static int fileNum = 1;

        private const string nameFile = "{0} team history";
        public ExportHistory(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string teamNameOrID = CommandParameters[0];
            var team = this.Repository.GetTeam(teamNameOrID);
            string file = string.Format(nameFile, team.Name);

            if (CommandParameters.Count == 1)
            {
                using (StreamWriter writer = new StreamWriter(path + $@"\{file}-{fileNum}.txt"))
                {
                    writer.Write(team.ViewHistory());
                }
                fileNum++;
                return $"History for the team was created as text-file on your desktop under name {file}-{fileNum - 1}.txt";
            }
            else if (CommandParameters.Count == 3 && CommandParameters[1].ToLower() == "board")
            {
                string boardIdentificator = CommandParameters[2];
                var board = this.Repository.GetBoard(boardIdentificator);

                if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
                {
                    throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
                }

                using (StreamWriter writer = new StreamWriter(path + $@"\{file}-{board.Name}-{fileNum}.txt"))
                {
                    writer.Write(team.Boards.FirstOrDefault(x => x.Id == board.Id).ViewHistory());
                }

                fileNum++;
                return $"History for the team was created as text-file on your desktop under name {file}-{board.Name}-{fileNum - 1}.txt";
            }
            else if (CommandParameters.Count == 3 && CommandParameters[1].ToLower() == "user")
            {
                string userIdentificator = CommandParameters[2];
                var user = this.Repository.GetUser(userIdentificator);
                if (!this.Repository.IsTeamMember(team, user))
                {
                    throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
                }
                using (StreamWriter writer = new StreamWriter(path + $@"\{file}-{user.Name}-{fileNum}.txt"))
                {
                    writer.Write(user.ViewHistory());
                }

                fileNum++;
                return $"History for the team was created as text-file on your desktop under name {file}-{user.Name}-{fileNum - 1}.txt";
            }
            else
            {
                throw new UserInputException("Execute command needs [teamname/id] (optional type[board or user] [board/userID]) - exports history events to desktop");
            }
        }
    }
}
