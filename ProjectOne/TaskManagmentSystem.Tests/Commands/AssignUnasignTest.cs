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
    public class AssignUnassignTest
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private const string BOARD = "TestBoard";
        private const string TASKTYPE = "Bug";
        private const string TASKTITLE = "TestTitleBug";
        private const string TASKDESCRIPTION = "TaskDescriptionTest";

        private IBoard board;
        private IMember user;
        private IBoardItem task;
        private readonly IList<string> parametersUser = new List<string> { USER, PASSWORD };
        private readonly IList<string> parametersBoard = new List<string> { BOARD, TEAM };
        private readonly IList<string> parametersTask = new List<string> { TASKTYPE, BOARD, TASKTITLE, TASKDESCRIPTION };

        private Repository repository = new Repository();

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.user = this.repository.CreateUser(USER, PASSWORD);
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            this.board = this.repository.CreateBoard(BOARD);
            team.AddBoard(board);
            this.task = this.repository.CreateTask(typeof(Bug), TASKTITLE, TASKDESCRIPTION, board);
        }

        [TestMethod]
        public void AssignTask_ShouldSuccessfulAddAssignee_WhenCorrectParametersAreSet()
        {
            //Arrange
            string expected = $"User {USER} was assigned to {TASKTYPE} with ID: 4";

            //Act
            Assign sut = new Assign(new List<string> { TEAM, USER, "4" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());
        }

        [TestMethod]
        public void AssignTask_ShouldThrowException_WhenFeedbackTypeOfTask()
        {
            //Arrange
            this.repository.CreateTask(typeof(Feedback), TASKTITLE, TASKDESCRIPTION, this.board, "11");

            //Act
            Assign sut = new Assign(new List<string> { TEAM, USER, "5" }, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void AssignTask_ShouldThrowException_WhenLoggedUserIsNotMemberOfTeam()
        {
            //Arrange
            string username = "NewUser";
            var user = repository.CreateUser(username, PASSWORD);
            this.repository.LoggedUser = user;
            //Act

            Assign sut = new Assign(new List<string> { TEAM, USER, "5" }, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void AssignTask_ShouldThrowException_WhenMemberToAssignIsNotInTeam()
        {
            //Arrange
            string username = "NewUser";
            repository.CreateUser(username, PASSWORD);

            //Act
            Assign sut = new Assign(new List<string> { TEAM, username, "5" }, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void UnassignTask_ShouldUnassignMember_FromGivenTaskSuccessful()
        {
            //Arrange & Act
            string expect = $"User {USER} was unassigned from {TASKTYPE} with ID: 4";
            this.user.AddTask(this.task);

            UnAssign sut = new UnAssign(new List<string> { TEAM, "4" }, this.repository);

            //Assert
            Assert.AreEqual(expect, sut.Execute());
        }

        [TestMethod]
        public void UnassignTask_ShouldThrowException_WhenTaskIsNotAssigned()
        {
            //Arrange & Act
            var testTask = this.repository.CreateTask(typeof(Bug), "BugTestTitleFeedback", "BugTaskDescriptionTest", board);

            UnAssign sut = new UnAssign(new List<string> { TEAM, "5" }, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void UnassignTask_ShouldThrowException_WhenNotMemberOfTheTeam()
        {
            //Arrange
            string username = "NewUser";
            this.user.AddTask(task);
            //Act           
            var testTeam = repository.CreateTeam("NewUserTeam");
            var userTest = repository.CreateUser(username, PASSWORD);
            this.repository.LoggedUser = userTest;
            UnAssign sut = new UnAssign(new List<string> { TEAM, "4" }, this.repository);

            //Assert
            Assert.ThrowsException<UserInputException>(() => sut.Execute());

        }
    }
}
