using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
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
        private const string BOARD = "TestBoard";
        private const string TASK_TITLE = "Task Title";
        private const string TASK_DESC = "Task Description";
        private const string COMMENT_CONTENT = "Some content";
        private readonly IList<string> parametersUser = new List<string> { USER, PASSWORD };

        private IMember user;
        private ITeam team;
        private IBoard board;
        IBoardItem bugTask;
        private IRepository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.user = this.repository.CreateUser(USER, PASSWORD);
            this.repository.LoggedUser = user;
            this.team = this.repository.CreateTeam(TEAM);
            this.board = this.repository.CreateBoard(BOARD);
            this.bugTask = this.repository.CreateTask(typeof(Bug), TASK_TITLE, TASK_DESC, board, new string[0]);

        }

        [TestMethod]
        public void RemoveTeam_ShouldRemoveTeam()
        {
            string teamToRemovename = "RemoveTest";
            var teamtoremove = this.repository.CreateTeam(teamToRemovename);
            string expected = $"Team with name {teamtoremove.Name}, ID: {teamtoremove.Id} was removed";

            RemoveTeam sut = new RemoveTeam(new List<string> { teamToRemovename }, this.repository);
            user.ChangeRole();
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

            this.user.ChangeRole();

            RemoveUser sut = new RemoveUser(new List<string> { usernametoremove }, this.repository);

            Assert.AreEqual(3, this.repository.Users.Count);
            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(2, this.repository.Users.Count);
        }
        [TestMethod]
        public void RemoveUser_ShouldThrowException_WhenNotOwner()
        {
            string usernametoremove = "UserToRemove";
            var userToRemove = this.repository.CreateUser("UserToRemove", PASSWORD);
                       
            RemoveUser sut = new RemoveUser(new List<string> { usernametoremove }, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
        [TestMethod]
        public void RemoveBoard_ShouldRemoveBoard()
        {
            string expected = $"Board with name {board.Name}, ID: {board.Id} was removed!";

            var sut = new RemoveBoard(new List<string> {BOARD, TEAM}, this.repository);
            team.AddBoard(board);
            Assert.AreEqual(1, team.Boards.Count);
            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(0, team.Boards.Count);
        }
        [TestMethod]
        public void RemoveBoard_ShouldThrowWhenUserIsNotMember()
        {
            this.repository.LoggedUser = repository.FindUserById(0);
            var sut = new RemoveBoard(new List<string> { BOARD, TEAM }, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
        [TestMethod]
        public void RemoveBoard_ShouldThrowWhenTeamNotFound()
        {
            var sut = new RemoveBoard(new List<string> { BOARD, "team" }, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
        [TestMethod]
        public void RemoveBoard_ShouldThrowWhenBoardNotFound()
        {
            var sut = new RemoveBoard(new List<string> { "board", TEAM }, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
        [TestMethod]
        public void RemoveTask_ShouldRemoveTask()
        {
            string expected = $"{bugTask.GetType().Name} {bugTask.Title}, ID: {bugTask.Id} was removed!";
            team.AddBoard(board);

            var sut = new RemoveTask(new List<string> { BOARD, bugTask.Id.ToString() }, this.repository);
            Assert.AreEqual(1, board.Tasks.Count);
            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(0, board.Tasks.Count);
        }
        [TestMethod]
        public void RemoveTask_ShouldRemoveTaskFromAssigned()
        {
            string expected = $"{bugTask.GetType().Name} {bugTask.Title}, ID: {bugTask.Id} was removed!";
            team.AddBoard(board);
            user.AddTask(bugTask);

            var sut = new RemoveTask(new List<string> { BOARD, bugTask.Id.ToString() }, this.repository);

            Assert.AreEqual(1, user.Tasks.Count);
            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(0, user.Tasks.Count);
        }
        [TestMethod]
        public void RemoveComment_ShouldRemoveComment()
        {
            string expected = $"Comment was removed!";
            team.AddBoard(board);
            bugTask.AddComment(new Comment(COMMENT_CONTENT, user.Name));

            var sut = new RemoveComment(new List<string> { TEAM, bugTask.Id.ToString(), COMMENT_CONTENT }, repository);

            Assert.AreEqual(1, bugTask.Comments.Count);
            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(0, bugTask.Comments.Count);
        }
        [TestMethod]
        public void RemoveComment_ShouldThrowException_WhenNotAllowed()
        {
            var user = this.repository.CreateUser("newUserName", PASSWORD);
            team.AddBoard(board);
            bugTask.AddComment(new Comment(COMMENT_CONTENT, this.repository.LoggedUser.Name));
            var sut = new RemoveComment(new List<string> { TEAM, bugTask.Id.ToString(), COMMENT_CONTENT }, repository);
            this.repository.LoggedUser = user;

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
    }
}
