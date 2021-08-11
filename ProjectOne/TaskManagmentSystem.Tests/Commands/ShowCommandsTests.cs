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

    }
}
