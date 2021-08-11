using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class TeamTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private const string adminName = "superuser";
        private const string adminPass = "th1$i$4dmiN";
        private readonly IList<string> parametersUser = new List<string> { USER, PASSWORD };

        private IMember user;
        private ITeam team;
        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.user = this.repository.CreateUser(USER, PASSWORD);
            this.repository.LoggedUser = user;
            this.team = this.repository.CreateTeam(TEAM);
        }

        [TestMethod]
        public void JoinTeamShouldThrowException_WhenCreatorOfTheTeamWantsToJoinTheTeam()
        {
            IList<string> parametersTeam = new List<string> { USER, TEAM };
            TeamJoin sut = new TeamJoin(parametersTeam, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void JoinTeam_ShouldSuccess_WhenNewMemberJoinTeam()
        {
            string newUser = "Pesho";
            string expected = $"User with name {newUser} was successfully added to team {TEAM}";
            this.repository.CreateUser(newUser, PASSWORD);

            IList<string> parametersTeam = new List<string> { newUser, TEAM };
            TeamJoin sut = new TeamJoin(parametersTeam, this.repository);

            Assert.AreEqual(expected, sut.Execute());
        }

        [TestMethod]
        public void JoinTeam_ShouldThrowException_WhenLoggedUserIsNotMember()
        {
            this.repository.LoggedUser = this.repository.CreateUser("NewUser", PASSWORD);
            var newteam = this.repository.CreateTeam("NewTestTeam");

            IList<string> parametersTeam = new List<string> { USER, TEAM };
            TeamJoin sut = new TeamJoin(parametersTeam, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void LeaveTeam_ShouldSuccess_WhenMemberRequests()
        {
            string expected = $"User with name {USER} successfully left team {TEAM}";

            IList<string> parametersTeam = new List<string> { TEAM };
            TeamLeave sut = new TeamLeave(parametersTeam, this.repository);

            Assert.AreEqual(expected, sut.Execute());
        }

        [TestMethod]
        public void LeaveTeam_ShouldThrowException_WhenLoggedUserIsNotMember()
        {
            IList<string> parametersTeam = new List<string> { TEAM };
            TeamLeave sut = new TeamLeave(parametersTeam, this.repository);
            sut.Execute();

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

    }
}
