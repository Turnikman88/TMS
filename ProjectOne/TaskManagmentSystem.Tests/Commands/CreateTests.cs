using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class CreateTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private const string BOARD = "TestBoard";
        private const string TASK_TITLE = "TaskTitleee";
        private const string TASK_DESCRIPTION = "TaskDescription";

        private readonly IList<string> PARAMETERS_USER = new List<string> { USER, PASSWORD };
        private IMember user;


        private Repository repository;


        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            user = this.repository.CreateUser(USER, PASSWORD);
        }

        [TestMethod]
        public void CreateUser_ShouldCreateUser()
        {
            //Arrange
            string sutUser = "newUsername";
            string expected = $"User with username {sutUser}, ID: 2 was created";

            //Act and Assert
            CreateUser sut = new CreateUser(new List<string> { sutUser, PASSWORD }, this.repository);

            Assert.AreEqual(expected, sut.Execute());
            Assert.AreEqual(3, this.repository.Users.Count);

        }

        [TestMethod]
        public void CreateUser_ShouldThrowException_WhenUsernameAlreadyexists()
        {
            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => this.repository.CreateUser(USER, PASSWORD));
        }

        [TestMethod]
        public void CreateUser_ShouldThrowException_WhenUsernameIsShort()
        {
            //Arrange
            string username = "Test";

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => this.repository.CreateUser(username, PASSWORD));
        }

        [TestMethod]
        public void CreateUser_ShouldThrowException_WhenUsernameIsTooLong()
        {
            //Arrange
            string username = "TestTestTestTestTest";

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => this.repository.CreateUser(username, PASSWORD));
        }

        [TestMethod]
        public void CreateUser_ShouldThrowException_WhenPasswordDoesNotMatchPattern()
        {
            //Arrange
            string password = "SimplePassword";

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => this.repository.CreateUser(USER, password));
        }
        [TestMethod]
        public void CreateUser_ShouldThrowException_WhenPasswordDoesNotMatchLenght()
        {
            //Arrange
            string password = "pas";

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => this.repository.CreateUser(USER, password));
        }

        [TestMethod]
        public void CreateTeam_ShouldCreateTeam()
        {
            //Arrange
            var result = $"Team with name {TEAM}, ID: 2 was created";
            IList<string> parameters = new List<string> { TEAM };

            //Act and Assert
            this.repository.LoggedUser = user;

            var sut = new CreateTeam(parameters, this.repository);
            Assert.AreEqual(result, sut.Execute());
            Assert.AreEqual(1, repository.Teams.Count);
        }

        [TestMethod]
        public void CreateTeam_ShouldThrowError_WhenNotLoggedIn()
        {
            //Arrange
            IList<string> parameters = new List<string> { TEAM };

            //Act and Assert
            var sut = new CreateTeam(parameters, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateTeam_ShouldThrowError_WhenTeamAlreadyExists()
        {
            //Arrange
            IList<string> parameters = new List<string> { TEAM };

            //Act and Assert
            this.repository.LoggedUser = user;
            this.repository.CreateTeam(TEAM);

            var sut = new CreateTeam(parameters, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateTeam_ShouldThrowError_WhenTeamNameTooLong()
        {
            //Arrange
            string team = "TeamNameIsTooLongToTest";
            IList<string> parameters = new List<string> { team };

            //Act and Assert
            this.repository.LoggedUser = user;

            var sut = new CreateTeam(parameters, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateTeam_ShouldThrowError_WhenTeamNameTooShort()
        {
            //Arrange
            string team = "Tim";

            IList<string> parameters = new List<string> { team };

            //Act and Assert
            this.repository.LoggedUser = user;

            var sut = new CreateTeam(parameters, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldCreateBoard_WhenCorrectInput()
        {
            //Arrange
            string result = $"Board with name {BOARD}, ID: 3 was created!";

            IList<string> parametersBoard = new List<string> { BOARD, TEAM };

            //Act and Assert
            this.repository.LoggedUser = user;
            this.repository.CreateTeam(TEAM);

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.AreEqual(result, sut.Execute());
            Assert.AreEqual(1, this.repository.GetTeam(TEAM).Boards.Count);
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenNameIsTooShort()
        {
            //Arrange
            string boardname = "Bord";

            IList<string> parametersBoard = new List<string> { boardname, TEAM };

            //Act and Assert
            this.repository.LoggedUser = user;
            this.repository.CreateTeam(TEAM);


            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenNameIsTooLong()
        {
            //Arrange
            string boardname = "BordNameIsTooLongForThisTest";

            IList<string> parametersBoard = new List<string> { boardname, TEAM };

            //Act and Assert
            this.repository.LoggedUser = user;
            this.repository.CreateTeam(TEAM);

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenNameIsNotUnique()
        {
            //Arrange
            IList<string> parametersBoard = new List<string> { BOARD, TEAM };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);

            var board = this.repository.CreateBoard(BOARD);
            team.AddBoard(board);

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenMemberNotInTeam()
        {
            //Arrange
            string anotherUser = "AnotherUsr";
            string password = "4n0Th3r@";

            IList<string> parameters = new List<string> { TEAM };
            IList<string> parametersBoard = new List<string> { BOARD, TEAM };
            IList<string> parametersAnother = new List<string> { anotherUser, password };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);

            var anotheruser = this.repository.CreateUser(anotherUser, password);
            this.repository.LoggedUser = anotheruser;

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateTask_ShouldCreateBug()
        {
            var result = $"Bug {TASK_TITLE}, ID: 4 was created!";

            //Arrange            

            IList<string> parametersTask = new List<string> { "bug", BOARD, TASK_TITLE, TASK_DESCRIPTION, "step1", "step2" };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);
            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.AreEqual(result, sut.Execute());
            Assert.AreEqual(1, repository.GetBoard(BOARD).Tasks.Count);
        }
        [TestMethod]
        public void CreateTask_ShouldCreateStory()
        {
            //Arrange            
            var result = $"Story {TASK_TITLE}, ID: 4 was created!";

            //Act and Assert
            IList<string> parametersTask = new List<string> { "story", BOARD, TASK_TITLE, TASK_DESCRIPTION };
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.AreEqual(result, sut.Execute());
            Assert.AreEqual(1, repository.GetBoard(BOARD).Tasks.Count);
        }
        [TestMethod]
        public void CreateTask_ShouldCreateFeedback()
        {
            //Arrange            
            var result = $"Feedback {TASK_TITLE}, ID: 4 was created!";

            //Act and Assert
            IList<string> parametersTask = new List<string> { "feedback", BOARD, TASK_TITLE, TASK_DESCRIPTION, "100" };
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.AreEqual(result, sut.Execute());
            Assert.AreEqual(1, repository.GetBoard(BOARD).Tasks.Count);
        }
        [TestMethod]
        public void CreateTask_ShouldThrowError_WhenTypeIsWrong()
        {
            //Arrange            
            IList<string> parametersTask = new List<string> { "type", BOARD, TASK_TITLE, TASK_DESCRIPTION };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
        [TestMethod]
        public void CreateTask__ShouldThrowError_WhenNumOfParamsIsWrong()
        {

            //Arrange            
            IList<string> parametersTask = new List<string> { "type", BOARD, TASK_TITLE };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
        [TestMethod]
        public void CreateTask__ShouldThrowError_WhenTitleIsShort()
        {
            var result = $"Story {TASK_TITLE}, ID: 4 was created!";

            //Arrange            
            IList<string> parametersTask = new List<string> { "story", BOARD, "t", TASK_DESCRIPTION };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<TargetInvocationException>(() => sut.Execute());
        }
        [TestMethod]
        public void CreateTask__ShouldThrowError_WhenDescriptionIsShort()
        {
            //Arrange            

            IList<string> parametersTask = new List<string> { "story", BOARD, TASK_TITLE, "d" };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<TargetInvocationException>(() => sut.Execute());
        }
        [TestMethod]
        public void CreateTask__ShouldThrowError_WhenRatingIsBelowMinimum()
        {
            //Arrange            

            IList<string> parametersTask = new List<string> { "feedback", BOARD, TASK_TITLE, TASK_DESCRIPTION, "-1" };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<TargetInvocationException>(() => sut.Execute());
        }
        [TestMethod]
        public void CreateTask__ShouldThrowError_WhenRatingIsAboveMaximum()
        {
            //Arrange            

            IList<string> parametersTask = new List<string> { "feedback", BOARD, TASK_TITLE, TASK_DESCRIPTION, "101" };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<TargetInvocationException>(() => sut.Execute());
        }
        [TestMethod]
        public void CreateTask__ShouldThrowError_WhenRatingIsNotNumber()
        {
            //Arrange            

            IList<string> parametersTask = new List<string> { "feedback", BOARD, TASK_TITLE, TASK_DESCRIPTION, "h" };

            //Act and Assert
            this.repository.LoggedUser = user;
            var team = this.repository.CreateTeam(TEAM);
            var board = this.repository.CreateBoard(BOARD);

            team.AddBoard(board);

            var sut = new CreateTask(parametersTask, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
    }
}
