using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class RemoveTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
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
        public void RemoveTeam_ShouldRemoveTeam()
        {
            string teamToRemovename = "RemoveTest";
            var teamtoremove = this.repository.CreateTeam(teamToRemovename);
            string expected = $"Team with name {teamtoremove.Name}, ID: {teamtoremove.Id} was removed";

            RemoveTeam sut = new RemoveTeam(new List<string> { teamToRemovename }, this.repository);

            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(1, this.repository.Teams.Count);
        }

        [TestMethod]
        public void RemoveTeam_ShouldThrowException_WhenNotOwner()
        {
            LogOut logout = new LogOut(new List<string> { }, this.repository);
            logout.Execute();
            string userNotInTeam = "NotInTeam";
            this.repository.CreateUser(userNotInTeam, PASSWORD);
            LogIn userNotInTeamLogin = new LogIn(new List<string> { userNotInTeam, PASSWORD }, this.repository);
            userNotInTeamLogin.Execute();

            IList<string> parametersTeam = new List<string> { TEAM };
            RemoveTeam sut = new RemoveTeam(parametersTeam, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void RemoveUser_ShouldRemoveUser()
        {
            string usernametoremove = "UserToRemove";
            var userToRemove = this.repository.CreateUser("UserToRemove", PASSWORD);

            string expected = $"User with username {usernametoremove}, ID: {userToRemove.Id} was removed";

            this.user.ChangeRole("root");

            RemoveUser sut = new RemoveUser(new List<string> { usernametoremove }, this.repository);

            Assert.AreEqual(3, this.repository.Users.Count);
            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(2, this.repository.Users.Count);
        }

    }
}
