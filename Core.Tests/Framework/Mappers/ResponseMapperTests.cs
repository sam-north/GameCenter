using Core.Framework.Mappers;
using Core.Framework.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Tests.Framework.Mappers
{
    [TestClass]
    public class ResponseMapperTests
    {
        IResponse<string> _sourceResponse { get; set; }
        private void Arrange()
        {
            _sourceResponse = new Response<string>();
            _sourceResponse.Errors.Add("this is an error");
            _sourceResponse.Messages.Add("this is a message");
            _sourceResponse.Errors.Add("this is another error");
            _sourceResponse.Messages.Add("this is another message");
            _sourceResponse.Errors.Add("this is a third error");
        }

        [TestMethod]
        public void MapMetadata_WithNonNullData_ShouldMapErrorsAndMessages()
        {
            //arrange
            Arrange();

            //act
            var result = ResponseMapper.MapMetadata<string>(_sourceResponse);

            //assert
            Assert.AreEqual(3, result.Errors.Count);
            Assert.AreEqual("this is an error", result.Errors.ElementAt(0));
            Assert.AreEqual("this is another error", result.Errors.ElementAt(1));
            Assert.AreEqual("this is a third error", result.Errors.ElementAt(2));
            Assert.AreEqual(2, result.Messages.Count);
            Assert.AreEqual("this is a message", result.Messages.ElementAt(0));
            Assert.AreEqual("this is another message", result.Messages.ElementAt(1));
        }
    }
}
