using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class ChangeCommandTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private const string BOARD = "TestBoard";
        private const string TASKTYPE = "Bug";
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

        private readonly IList<string> parametersUser = new List<string> { USER, PASSWORD };
        private readonly IList<string> parametersBoard = new List<string> { BOARD, TEAM };
        //private readonly IList<string> parametersTask = new List<string> { TASKTYPE, BOARD, TASKTITLE, TASKDESCRIPTION };

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
        public void ShouldChangeStatusSuccessfullyForBugTasks()
        {
            //Arrange
            string expected = "status of item Bug ID: 4 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "4", "Status" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }

        [TestMethod]
        public void ShouldChangePrioritySuccessfullyForBugTasks()
        {
            //Arrange
            string expected = "priority of item Bug ID: 4 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "4", "Priority" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }

        [TestMethod]
        public void ShouldChangeSeveritySuccessfullyForBugTasks()
        {
            //Arrange
            string expected = "severity of item Bug ID: 4 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "4", "Severity" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }

        [TestMethod]
        public void ShouldChangeStatusSuccessfullyForFeedbackTasks()
        {
            //Arrange
            string expected = "status of item Feedback ID: 5 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "5", "Status" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }
        //[TestMethod] TODO: change rating?
        //public void ShouldChangeRatingSuccessfullyForFeedbackTasks()
        //{
        //    //Arrange
        //    string expected = "rating of item Feedback ID: 5 was changed!";
        //
        //    //Act
        //    Change sut = new Change(new List<string> { TEAM, "5", "Rating", "25" }, this.repository);
        //
        //    //Assert
        //    Assert.AreEqual(expected, sut.Execute());
        //
        //}

        [TestMethod]
        public void ShouldChangeStatusSuccessfullyForStoryTasks()
        {
            //Arrange
            string expected = "status of item Story ID: 6 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "6", "Status" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }
        [TestMethod]
        public void ShouldChangeSizeSuccessfullyForStoryTasks()
        {
            //Arrange
            string expected = "size of item Story ID: 6 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "6", "Size" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }
        [TestMethod]
        public void ShouldChangePrioritySuccessfullyForStoryTasks()
        {
            //Arrange
            string expected = "priority of item Story ID: 6 was changed!";

            //Act
            Change sut = new Change(new List<string> { TEAM, "6", "Priority" }, this.repository);

            //Assert
            Assert.AreEqual(expected, sut.Execute());

        }
    }
}
