using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowTaskByType : BaseCommand
    {
        private const int numberOfParameters = 5;
        //ShowTaskByType[teamid / name][typeoftask] (keyword)[1.filter / 2.sort][1.status / assignee][2.title / priority / severity / size / rating]

        public ShowTaskByType(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamIdentifier = CommandParameters[0];
            string typeOfTask = CommandParameters[1];
            string keyword = CommandParameters[2];
            string parameter1 = CommandParameters[3];
            string parameter2 = CommandParameters[4];

            var team = this.Repository.GetTeam(teamIdentifier);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            if (typeOfTask.ToLower() == "bug")
            {
                if (keyword.ToLower() == "filter")
                {

                }
                else if (keyword.ToLower() == "sort")
                {

                }
                else
                {
                    throw new UserInputException("You can only sort or filter");
                }
            }
            else if (typeOfTask.ToLower() == "story")
            {
                if (keyword.ToLower() == "filter")
                {

                }
                else if (keyword.ToLower() == "sort")
                {

                }
                else
                {
                    throw new UserInputException("You can only sort or filter");
                }
            }
            else if (typeOfTask.ToLower() == "feedback")
            {
                if (keyword.ToLower() == "filter")
                {

                }
                else if (keyword.ToLower() == "sort")
                {

                }
                else
                {
                    throw new UserInputException("You can only sort or filter");
                }
            }
            else
            {
                throw new UserInputException("You can only sort tasks Bug/Feedback/Story");
            }



            throw new NotImplementedException();
        }
    }
}
