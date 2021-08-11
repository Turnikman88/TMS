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
        private const string TASKTYPE_BUG = "Bug";
        private const string TASKTYPE_STORY = "Story";
        private const string TASKTITLE_BUG = "TestTitleBug";
        private const string TASKDESCRIPTION_BUG = "TaskDescriptionTestBug";
        private const string TASKTITLE_FEEDBACK = "TestTitleFeedback";
        private const string TASKDESCRIPTION_FEEDBACK = "TaskDescriptionTestFeedback";
        private const string TASKTITLE_STORY = "TestTitleStory";
        private const string TASKDESCRIPTION_STORY = "TaskDescriptionTestStory";

        private IBoard board;
        private IMember user;
        private IBoardItem taskBug;
        private IBoardItem taskFeedback;
        private IBoardItem taskStory;

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
            this.taskBug = this.repository.CreateTask(typeof(Bug), TASKTITLE_BUG, TASKDESCRIPTION_BUG, board);
            this.taskFeedback = this.repository.CreateTask(typeof(Feedback), TASKTITLE_FEEDBACK, TASKDESCRIPTION_FEEDBACK, board, "11");
            this.taskStory = this.repository.CreateTask(typeof(Story), TASKTITLE_STORY, TASKDESCRIPTION_STORY, board);
        }

        [TestMethod]
        public void AssignTask_ShouldSuccessfulAddAssignee_WhenCorrectParametersAreSetForBug()
        {
            //Arrange
            string expected = $"User {USER} was assigned to {TASKTYPE_BUG} with ID: 4";

            //Act
            Assign sut = new Assign(new List<string> { TEAM, USER, "4" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());
        }

        [TestMethod]
        public void AssignTask_ShouldSuccessfulAddAssignee_WhenCorrectParametersAreSetForStory()
        {
            //Arrange
            string expected = $"User {USER} was assigned to {TASKTYPE_STORY} with ID: 6";

            //Act
            Assign sut = new Assign(new List<string> { TEAM, USER, "6" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());
        }

        [TestMethod]
        public void AssignTask_ShouldThrowException_WhenFeedbackTypeOfTaskAlreadyExsists()
        {
            //Arrange
            this.repository.CreateTask(typeof(Feedback), TASKTITLE_FEEDBACK, TASKDESCRIPTION_FEEDBACK, this.board, "11");

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
        public void UnassignTask_ShouldUnassignMember_FromGivenBugTaskSuccessful()
        {
            //Arrange & Act
            string expect = $"User {USER} was unassigned from {TASKTYPE_BUG} with ID: 4";
            this.user.AddTask(this.taskBug);

            UnAssign sut = new UnAssign(new List<string> { TEAM, "4" }, this.repository);

            //Assert
            Assert.AreEqual(expect, sut.Execute());
        }

        [TestMethod]
        public void UnassignTask_ShouldUnassignMember_FromGivenStoryTaskSuccessful()
        {
            //Arrange & Act
            string expect = $"User {USER} was unassigned from {TASKTYPE_STORY} with ID: 6";
            this.user.AddTask(this.taskStory);

            UnAssign sut = new UnAssign(new List<string> { TEAM, "6" }, this.repository);

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
            this.user.AddTask(taskBug);
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
