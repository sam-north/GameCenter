using Core.Models.Dtos;
using Core.Validators.Concretes;
using Core.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Tests.Validators
{
    [TestClass]
    public class UserValidatorTests
    {
        IUserValidator _userValidator { get; set; }

        UserSignUpDto _userSignUpDto { get; set; }
        UserSignInDto _userSignInDto { get; set; }

        private void Arrange()
        {
            _userValidator = new UserValidator();

            _userSignUpDto = new UserSignUpDto
            {
                Email = "meat@joim.com",
                Password = "superC)0MPLEsxPassword",
                ConfirmPassword = "superC)0MPLEsxPassword",
            };

            _userSignInDto = new UserSignInDto
            {
                Email = "lwekm@django.com",
                Password = "tinelwwef9839kewf9wef23l"
            };
        }

        [TestMethod]
        public void ValidateUserSignUp_WithExpectedParameters_ShouldReturnNoErrors()
        {
            //arrange
            Arrange();

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsFalse(result.Errors.Any());
        }

        [TestMethod]
        public void ValidateUserSignUp_WithInvalidEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Email = "wei";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email must be an email address."));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithNullEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Email = null;

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email is required"));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithEmptyEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Email = "";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email is required"));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithWhiteSpaceEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Email = "        ";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email is required"));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithNullPassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Password = null;

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password is required"));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithEmptyPassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Password = "";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password is required"));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithWhiteSpacePassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Password = "        ";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password is required"));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithInvalidLengthPassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Password = "2525sdg";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password must be at least 8 characters in length."));
        }

        [TestMethod]
        public void ValidateUserSignUp_WithNotMatchingPasswords_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignUpDto.Password = "2525sdgeeee";
            _userSignUpDto.ConfirmPassword = "2525sdgeeer";

            //act
            var result = _userValidator.Validate(_userSignUpDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password and confirm password must match."));
        }

        [TestMethod]
        public void ValidateUserSignIn_WithExpectedParameters_ShouldReturnNoErrors()
        {
            //arrange
            Arrange();

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsFalse(result.Errors.Any());
        }

        [TestMethod]
        public void ValidateUserSignIn_WithNullEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignInDto.Email = null;

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email is required"));
        }

        [TestMethod]
        public void ValidateUserSignIn_WithEmptyEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignInDto.Email = "";

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email is required"));
        }

        [TestMethod]
        public void ValidateUserSignIn_WithWhiteSpaceEmail_ShouldReturnEmailError()
        {
            //arrange
            Arrange();
            _userSignInDto.Email = "        ";

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Email is required"));
        }

        [TestMethod]
        public void ValidateUserSignIn_WithNullPassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignInDto.Password = null;

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password is required"));
        }

        [TestMethod]
        public void ValidateUserSignIn_WithEmptyPassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignInDto.Password = "";

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password is required"));
        }

        [TestMethod]
        public void ValidateUserSignIn_WithWhiteSpacePassword_ShouldReturnPasswordError()
        {
            //arrange
            Arrange();
            _userSignInDto.Password = "        ";

            //act
            var result = _userValidator.Validate(_userSignInDto);

            //assert
            Assert.IsTrue(result.Errors.Any(x => x == "Password is required"));
        }
    }
}
