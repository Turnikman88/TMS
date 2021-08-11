using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Tests
{
    [TestClass]
    public class LoginTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";

        private IMember user;
        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.user = this.repository.CreateUser(USER, PASSWORD);
        }

        [TestMethod]
        public void ShouldCreateUserSuccessfuly_WhenCorrectParametersInput()
        {
            //Arrange
            //Act
            string expected = $"User {USER} successfully logged in!";

            IList<string> parameters = new List<string> { USER, PASSWORD };
            LogIn login = new LogIn(parameters, this.repository);

            //Assert
            Assert.AreEqual(expected, login.Execute());
        }

        [TestMethod]
        public void ShouldThrowException_WhenPasswordIsWrong()
        {
            //Arrange
            string wrongPassword = "WrongPassword";

            IList<string> parameters = new List<string> { USER, wrongPassword };
            LogIn login = new LogIn(parameters, this.repository);

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => login.Execute());
        }

        [TestMethod]
        public void ShouldThrowException_WhenUsernameIsWrong()
        {
            //Arrange
            string wrongUsername = "WrongUser";

            IList<string> parameters = new List<string> { wrongUsername, PASSWORD };
            LogIn login = new LogIn(parameters, this.repository);

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => login.Execute());
        }

        [TestMethod]
        public void ShouldThrowException_WhenSameUsernameAlreadyLogged()
        {
            //Arrange
            IList<string> parameters = new List<string> { USER, PASSWORD };
            LogIn login = new LogIn(parameters, this.repository);
            login.Execute();

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => login.Execute());
        }

        [TestMethod]
        public void ShouldThrowException_WhenAnotherUsernameAlreadyLogged()
        {
            //Arrange
            string username = "NewUser";
            var anotheruser = repository.CreateUser(username, PASSWORD);
            this.repository.LoggedUser = user;

            //Act and Assert
            IList<string> parameters2 = new List<string> { username, PASSWORD };
            LogIn sut = new LogIn(parameters2, this.repository);

            Assert.ThrowsException<UserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void ShouldThrowException_WhenAlreadyLogedOut()
        {
            //Arrange

            this.repository.LoggedUser = user;

            LogOut logout = new LogOut(new List<string>(), this.repository);
            logout.Execute();

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => logout.Execute());
        }
    }
}

