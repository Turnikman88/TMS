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
    public class ShowCommandsTests
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
        private ITeam team;
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
            team = this.repository.CreateTeam(TEAM);
            this.board = this.repository.CreateBoard(BOARD);
            team.AddBoard(board);
            this.taskBug = this.repository.CreateTask(typeof(Bug), TASKTITLE_BUG, TASKDESCRIPTION_BUG, board, "Test Test Test");
            this.taskFeedback = this.repository.CreateTask(typeof(Feedback), TASKTITLE_FEEDBACK, TASKDESCRIPTION_FEEDBACK, board, "11");
            this.taskStory = this.repository.CreateTask(typeof(Story), TASKTITLE_STORY, TASKDESCRIPTION_STORY, board);
        }

        [TestMethod]
        public void ShowAllTasks_ShouldFilterAndDisplayCorrectInfo()
        {
            ShowAllTasks sut = new ShowAllTasks(new List<string> { TEAM, "filter", "TestTitleBug" }, this.repository);

            Assert.AreEqual(true, sut.Execute().Contains("TestTitleBug"));
        }

        [TestMethod]
        public void ShowAllTasks_ShouldSortAndDisplayCorrectInfo()
        {
            ShowAllTasks sut = new ShowAllTasks(new List<string> { TEAM, "sortby", "TestTitleFeedback" }, this.repository);

            Assert.AreEqual(true, sut.Execute().Contains("TestTitleFeedback"));
            Assert.AreEqual(true, sut.Execute().Contains("TestTitleStory"));
            Assert.AreEqual(true, sut.Execute().Contains("TestTitleBug"));
        }

        [TestMethod]
        public void ShowAllTasks_ShouldThrowException_WhenKeywordMissmatch()
        {
            ShowAllTasks sut = new ShowAllTasks(new List<string> { TEAM, "notfilter", "TestTitleBug" }, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowAllTasks_ShouldThrowException_WhenNotMemberOfTheTeamWantsAccess()
        {
            ShowAllTasks sut = new ShowAllTasks(new List<string> { TEAM, "notfilter", "TestTitleBug" }, this.repository);
            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowAllTeamMembers_ShouldListCurrentTeamMembers()
        {
            ShowAllTeamMembers sut = new ShowAllTeamMembers(new List<string> { TEAM }, this.repository);

            Assert.IsTrue(sut.Execute().Contains("Members count: 1"));
            this.team.AddMember(this.repository.CreateUser("AnotherUser", PASSWORD));
            Assert.IsTrue(sut.Execute().Contains("Members count: 2"));
        }

        [TestMethod]
        public void ShowAllTeamMembers_ShouldThrowException_WhenNotMemberOfTheTeamWantsAccess()
        {
            ShowAllTeamMembers sut = new ShowAllTeamMembers(new List<string> { TEAM }, this.repository);

            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowwAllTeams_ShouldDisplayCorrect()
        {
            ShowAllTeams sut = new ShowAllTeams(new List<string>(), this.repository);
            this.user.ChangeRole();

            Assert.IsTrue(sut.Execute().Contains("Teams count: 1"));
            this.repository.RemoveTeam(team);
            Assert.IsTrue(sut.Execute().Contains("There are no registered teams."));
        }

        [TestMethod]
        public void ShowwAllUsers_ShouldDisplayCorrect()
        {
            ShowAllUsers sut = new ShowAllUsers(new List<string>(), this.repository);
            this.user.ChangeRole();

            Assert.IsTrue(sut.Execute().Contains("Users count: 2"));
        }

        [TestMethod]
        public void ShowTeamBoards_ShouldDisplayCorrectInfo()
        {
            team.AddBoard(this.repository.CreateBoard("2ndBoard"));

            ShowTeamBoards sut = new ShowTeamBoards(new List<string> { TEAM }, this.repository);

            Assert.IsTrue(sut.Execute().Contains("Boards on this team: 2"));
            Assert.AreEqual(2, team.Boards.Count);
        }

        [TestMethod]
        public void ShowTeamBoards_ShouldThrowException_WhenNotMemberOfTeam()
        {
            ShowTeamBoards sut = new ShowTeamBoards(new List<string> { TEAM }, this.repository);
            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowUserActivity_ShouldShowCorrectEvents()
        {
            ShowUserActivity sut = new ShowUserActivity(new List<string> { TEAM, USER }, this.repository);

            Assert.IsTrue(sut.Execute().Contains("Member with ID: 1 was created!"));
        }

        [TestMethod]
        public void ShowUserActivity_ShouldThrowException_WhenNotMemberOfTeam()
        {
            ShowUserActivity sut = new ShowUserActivity(new List<string> { TEAM, USER }, this.repository);
            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowAssigneedTasks_ShouldReturnOnlyAsigneedTasks()
        {
            var task = taskBug as Bug;
            task.AddAssignee(user);
            user.AddTask(task);

            ShowAssigneedTasks sut = new ShowAssigneedTasks(new List<string> { TEAM, $"{board.Id}" }, this.repository);

            Assert.IsTrue(sut.Execute().Contains($"Bug: {taskBug.Title}"));
            Assert.IsFalse(sut.Execute().Contains($"Story: "));
            Assert.AreEqual(1, user.Tasks.Count);

            var task2 = taskStory as Story;
            task2.AddAssignee(user);
            user.AddTask(task2);

            Assert.IsTrue(sut.Execute().Contains($"Story: {taskStory.Title}"));
            Assert.AreEqual(2, user.Tasks.Count);

        }

        [TestMethod]
        public void ShowAssigneedTasks_ShouldThrowException()
        {
            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);
            ShowAssigneedTasks sut = new ShowAssigneedTasks(new List<string> { TEAM, $"{board.Id}" }, this.repository);


            Assert.ThrowsException<UserInputException>(() => sut.Execute());

        }

        [TestMethod]
        public void ShowBoardActivitty_ShouldShowEvents()
        {
            ShowBoardActivity sut = new ShowBoardActivity(new List<string> { $"{team.Id}", $"{board.Id}" }, this.repository);

            Assert.IsTrue(sut.Execute().Contains($"was pinned to board 'TestBoard'"));
            Assert.IsTrue(sut.Execute().Contains($"was created!"));
        }

        [TestMethod]
        public void ShowBoardActivitty_ShouldThrowException()
        {
            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);
            ShowBoardActivity sut = new ShowBoardActivity(new List<string> { $"{team.Id}", $"{board.Id}" }, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowTeamActivity_ShouldShowEvents()
        {
            ShowTeamActivity sut = new ShowTeamActivity(new List<string> { $"{team.Id}" }, this.repository);

            Assert.IsTrue(sut.Execute().Contains($"ID: 1 is admin of team TestTeam!"));
            Assert.IsTrue(sut.Execute().Contains($"Member with ID: 1 was created!"));
        }

        [TestMethod]
        public void ShowTeamActivity_ShouldThrowException()
        {
            this.repository.LoggedUser = this.repository.CreateUser("UserNotInTeam", PASSWORD);
            ShowTeamActivity sut = new ShowTeamActivity(new List<string> { $"{team.Id}" }, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShowTaskByType_FilterBugsByStatusActive()
        {
            ShowTaskByType sut = new ShowTaskByType(new List<string> { "Bug", "filter", "status", "active" }, this.repository);

            Assert.IsTrue(sut.Execute().Contains($"Bug: {taskBug.Title}"));
            Assert.IsTrue(sut.Execute().Contains($"Status: Active"));
        }
        [TestMethod]
        public void ShowTaskByType_FilterStoryByStatusActive()
        {
            ShowTaskByType sut = new ShowTaskByType(new List<string> { "Story", "filter", "status", "notdone" }, this.repository);

            Assert.IsTrue(sut.Execute().Contains($"Story: {taskStory.Title}"));
            Assert.IsTrue(sut.Execute().Contains($"Status: NotDone"));
        }
        [TestMethod]
        public void ShowTaskByType_FilterFeedbackByStatusActive()
        {
            ShowTaskByType sut = new ShowTaskByType(new List<string> { "feedback", "filter", "status", "New" }, this.repository);

            Assert.IsTrue(sut.Execute().Contains($"Feedback: {taskFeedback.Title}"));
            Assert.IsTrue(sut.Execute().Contains($"Status: New"));
        }

        [TestMethod]
        public void ShowTaskByType_SortByTitle()
        {
            ShowTaskByType sut = new ShowTaskByType(new List<string> { "feedback", "sort", "title" }, this.repository);


            Assert.IsTrue(sut.Execute().Contains($"Feedback: {taskFeedback.Title}"));
            Assert.IsFalse(sut.Execute().Contains($"Story: {taskStory.Title}"));
            Assert.IsFalse(sut.Execute().Contains($"Bug: {taskBug.Title}"));
        }
        //[TestMethod] TODO:Fix filter by assignee function
        //public void ShowTaskByType_FilterBugsByAssignee()
        //{
        //    ShowTaskByType sut = new ShowTaskByType(new List<string> { "Bug", /"filter", /"assignee", USER }, this.repository);
        //
        //    var task = taskBug as Bug;
        //    task.AddAssignee(user);
        //    user.AddTask(task);
        //
        //    Assert.AreEqual("asd", sut.Execute());
        //    Assert.IsTrue(sut.Execute().Contains($"Bug: TestTitleBug"));
        //    Assert.IsTrue(sut.Execute().Contains($"Status: Active"));
        //    Assert.IsTrue(sut.Execute().Contains($"Assignee: {USER}"));
        //
        //}
    }
}
