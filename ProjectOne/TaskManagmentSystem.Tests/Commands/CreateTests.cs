using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Tests.Commands
{
    [TestClass]
    public class CreateTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";
        private const string TEAM = "TestTeam";
        private const string BOARD = "TestBoard";

        private readonly IList<string> PARAMETERS_USER = new List<string> { USER, PASSWORD };

        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.repository.CreateUser(USER, PASSWORD);
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
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var sut = new CreateTeam(parameters, this.repository);
            Assert.AreEqual(result, sut.Execute());
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
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var sut1 = new CreateTeam(parameters, this.repository);
            sut1.Execute();

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
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

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
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var sut = new CreateTeam(parameters, this.repository);
            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldCreateBoard_WhenCorrectInput()
        {
            //Arrange
            string result = $"Board with name {BOARD}, ID: 3 was created";

            IList<string> parameters = new List<string> { TEAM };
            IList<string> parametersBoard = new List<string> { BOARD, TEAM };

            //Act and Assert
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var team = new CreateTeam(parameters, this.repository);
            team.Execute();

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.AreEqual(result, sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenNameIsTooShort()
        {
            //Arrange
            string boardname = "Bord";

            IList<string> parameters = new List<string> { TEAM };
            IList<string> parametersBoard = new List<string> { boardname, TEAM };

            //Act and Assert
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var team = new CreateTeam(parameters, this.repository);
            team.Execute();

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenNameIsTooLong()
        {
            //Arrange
            string boardname = "BordNameIsTooLongForThisTest";

            IList<string> parameters = new List<string> { TEAM };
            IList<string> parametersBoard = new List<string> { boardname, TEAM };

            //Act and Assert
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var team = new CreateTeam(parameters, this.repository);
            team.Execute();

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void CreateBoard_ShouldThrowError_WhenNameIsNotUnique()
        {
            //Arrange

            IList<string> parameters = new List<string> { TEAM };
            IList<string> parametersBoard = new List<string> { BOARD, TEAM };

            //Act and Assert
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var team = new CreateTeam(parameters, this.repository);
            team.Execute();

            var testboard = new CreateBoard(parametersBoard, this.repository);
            testboard.Execute();

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
            LogIn login = new LogIn(PARAMETERS_USER, this.repository);
            login.Execute();

            var team = new CreateTeam(parameters, this.repository);
            team.Execute();

            LogOut logout = new LogOut(PARAMETERS_USER, this.repository);
            logout.Execute();

            this.repository.CreateUser(anotherUser, password);
            LogIn loginanother = new LogIn(parametersAnother, this.repository);
            loginanother.Execute();

            var sut = new CreateBoard(parametersBoard, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }
    }
}
