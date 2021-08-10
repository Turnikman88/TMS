using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Tests
{
    [TestClass]
    public class LoginTests
    {
        private const string USER = "TestUser";
        private const string PASSWORD = "S7r0nGP@$$Word";

        private Repository repository;

        [TestInitialize]
        public void Prepare()
        {
            this.repository = new Repository();
            this.repository.CreateUser(USER, PASSWORD);
        }

        [TestMethod]
        public void ShouldCreateUserSuccessfuly_WhenCorrectParametersInput()
        {
            //Arrange
            string expected = $"User {USER} successfully logged in!";

            IList<string> parameters = new List<string> { USER, PASSWORD };
            LogIn login = new LogIn(parameters, this.repository);

            //Act
            string result = login.Execute();

            //Assert
            Assert.AreEqual(expected, result);
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
            repository.CreateUser(username, PASSWORD);

            IList<string> parameters = new List<string> { USER, PASSWORD };
            LogIn login = new LogIn(parameters, this.repository);
            login.Execute();

            IList<string> parameters2 = new List<string> { username, PASSWORD };
            LogIn login2 = new LogIn(parameters2, this.repository);

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => login2.Execute());
        }

        [TestMethod]
        public void ShouldThrowException_WhenAlreadyLogedOut()
        {
            //Arrange
            IList<string> parameters = new List<string> { USER, PASSWORD };
            LogIn login = new LogIn(parameters, this.repository);
            login.Execute();

            LogOut logout = new LogOut(new List<string>(), this.repository);
            logout.Execute();

            //Act and Assert
            Assert.ThrowsException<UserInputException>(() => logout.Execute());
        }
    }
}

