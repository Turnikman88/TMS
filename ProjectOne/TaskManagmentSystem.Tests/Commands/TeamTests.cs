using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class TeamTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private readonly IList<string> parametersUser = new List<string> { USER, PASSWORD };

        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.repository.CreateUser(USER, PASSWORD);
            LogIn login = new LogIn(parametersUser, this.repository);
            login.Execute();
            this.repository.CreateTeam(TEAM);
        }

        [TestMethod]
        public void JoinTeamShouldThrowException_WhenCreatorOfTheTeamWantsToJoinTheTeam()
        {
            string expected = $"User with name {USER} was successfully added to team {TEAM}";

            IList<string> parametersTeam = new List<string> { USER, TEAM };
            TeamJoin team = new TeamJoin(parametersTeam, this.repository);

            Assert.ThrowsException<UserInputException>(() => team.Execute());
        }

        [TestMethod]
        public void JoinTeam_ShouldSuccess_WhenNewMemberJoinTeam()
        {
            string expected = $"User with name Pesho was successfully added to team {TEAM}";
            this.repository.CreateUser("Pesho", PASSWORD);

            IList<string> parametersTeam = new List<string> { "Pesho", TEAM };
            TeamJoin team = new TeamJoin(parametersTeam, this.repository);

            Assert.AreEqual(expected, team.Execute());
        }
    }
}
