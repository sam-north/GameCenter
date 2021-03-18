using Core.Framework.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Framework.Models
{
    [TestClass]
    public class ResponseTests
    {
        IResponse<string> _response { get; set; }
        private void Arrange()
        {
            _response = new Response<string>();
        }

        [TestMethod]
        public void IsValid_WithNoErrors_ShouldBeTrue()
        {
            //arrange
            Arrange();

            //act
            var result = _response.IsValid;

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_WithErrors_ShouldBeFalse()
        {
            //arrange
            Arrange();
            _response.Errors.Add("This is an error");

            //act
            var result = _response.IsValid;

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WithNullErrors_ShouldBeTrue()
        {
            //arrange
            Arrange();
            _response.Errors = null;

            //act
            var result = _response.IsValid;

            //assert
            Assert.IsTrue(result);
        }
    }
}
