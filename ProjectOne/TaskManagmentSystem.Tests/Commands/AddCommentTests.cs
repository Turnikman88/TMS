using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;

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

        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            CreateUser testUser = new CreateUser(parametersUser, this.repository);
            testUser.Execute();
            LogIn login = new LogIn(parametersUser, this.repository);
            login.Execute();
            this.repository.CreateTeam(TEAM);
            CreateBoard testBoard = new CreateBoard(parametersBoard, this.repository);
            testBoard.Execute();
            CreateTask testTask = new CreateTask(parametersTask, this.repository);
            testTask.Execute();
        }

        [TestMethod]
        public void AddCommentToTask_ShouldSuccess_WhenTaskIsExisting()
        {
            //Arrange
            string expected = "Comment was added";

            //Act
            IList<string> sutParameters = new List<string>() { TEAM, "4", "This is test content", "AuthorTest" };
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
            IList<string> sutParameters = new List<string>() { TEAM, "4", "This is test content" };
            AddCommentToTask sut = new AddCommentToTask(sutParameters, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void AddCommentToTask_ShouldThrowException_WhenNotMemberOfATeam()
        {
            //Arrange
            string username = "NewUser";
            repository.CreateUser(username, PASSWORD);

            //Act
            LogOut logout = new LogOut(new List<string>(), this.repository);
            logout.Execute();
            LogIn login = new LogIn(new List<string>() { username, PASSWORD }, this.repository);
            login.Execute();
            IList<string> sutParameters = new List<string>() { TEAM, "4", "This is test content", "AuthorTest" };
            AddCommentToTask sut = new AddCommentToTask(sutParameters, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
    }
}
