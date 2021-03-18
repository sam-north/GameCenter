using Core.Mappers.Concretes;
using Core.Mappers.Interfaces;
using Core.Models.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Tests.Mappers
{
    [TestClass]
    public class UserMapperTests
    {
        IUserMapper _userMapper { get; set; }

        UserSignUpDto _userSignUpDto { get; set; }
        UserSignInDto _userSignInDto { get; set; }

        private void Arrange()
        {
            _userMapper = new UserMapper();

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
        public void MapUserSignUpDto_WithExpectedParameters_ShouldMapFields()
        {
            //arrange
            Arrange();

            //act
            var result = _userMapper.Map(_userSignUpDto);

            //assert
            Assert.AreEqual("meat@joim.com", result.Email);
            Assert.AreEqual("superC)0MPLEsxPassword", result.Password);
        }

        [TestMethod]
        public void MapUserSignUpDto_WithNullInput_ShouldThrowException()
        {
            //arrange
            Arrange();
            _userSignUpDto = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _userMapper.Map(_userSignUpDto));
        }

        [TestMethod]
        public void MapUserSignInDto_WithExpectedParameters_ShouldMapFields()
        {
            //arrange
            Arrange();

            //act
            var result = _userMapper.Map(_userSignInDto);

            //assert
            Assert.AreEqual("lwekm@django.com", result.Email);
            Assert.AreEqual("tinelwwef9839kewf9wef23l", result.Password);
        }

        [TestMethod]
        public void MapUserSignInDto_WithNullInput_ShouldThrowException()
        {
            //arrange
            Arrange();
            _userSignInDto = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _userMapper.Map(_userSignInDto));
        }
    }
}
