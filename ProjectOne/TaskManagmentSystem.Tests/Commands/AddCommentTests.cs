using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class AddCommentTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private const string BOARD = "TestBoard";

        private readonly IList<string> parametersUser = new List<string> { USER, PASSWORD };
        private readonly IList<string> parametersBoard = new List<string> { BOARD, TEAM };
        private readonly IList<string> parametersTask = new List<string> { "Bug", BOARD, "TestTitleBug", "TaskDescriptionTest" };

        private IBoard board;
        private IMember user;
        private IBoardItem task;
        private ITeam team;

        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.user = this.repository.CreateUser(USER, PASSWORD);
            this.repository.LoggedUser = user;
            this.team = this.repository.CreateTeam(TEAM);
            this.board = this.repository.CreateBoard(BOARD);
            this.team.AddBoard(board);
            this.task = this.repository.CreateTask(typeof(Bug), "TestTitleBug", "TaskDescriptionTest", this.board);
        }

        [TestMethod]
        public void AddCommentToTask_ShouldSuccess_WhenTaskIsExisting()
        {
            //Arrange
            string expected = "Comment was added";

            //Act
            IList<string> sutParameters = new List<string>() { TEAM, "4", "This is test content" };
            AddCommentToTask sut = new AddCommentToTask(sutParameters, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());
        }

        [TestMethod]
        public void AddCommentToTask_ShouldThrowException_WhenTaskDoesntExsist()
        {
            //Arrange & Act
            IList<string> sutParameters = new List<string>() { TEAM, "111111", "This is test content", "AuthorTest" };
            AddCommentToTask sut = new AddCommentToTask(sutParameters, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void AddCommentToTask_ShouldThrowException_WhenNotEnoughParameters()
        {
            //Arrange & Act
            IList<string> sutParameters = new List<string>() { TEAM, "4"};
            AddCommentToTask sut = new AddCommentToTask(sutParameters, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void AddCommentToTask_ShouldThrowException_WhenNotMemberOfATeam()
        {
            //Arrange
            string username = "NewUser";
            var anotheruser = repository.CreateUser(username, PASSWORD);
            this.repository.LoggedUser = anotheruser;

            //Act
            IList<string> sutParameters = new List<string>() { TEAM, "4", "This is test content", "AuthorTest" };
            AddCommentToTask sut = new AddCommentToTask(sutParameters, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
    }
}
