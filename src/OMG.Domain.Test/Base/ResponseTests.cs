using FluentAssertions;
using Xunit;
using OMG.Domain.Base;

namespace OMG.Domain.Test.Base
{
    public class ResponseTests
    {
        [Fact]
        public void Response_ShouldCreateWithDefaultValues_WhenNoArgumentsProvided()
        {
            // Act
            var response = new Response<string>();

            // Assert
            response.Code.Should().Be(200);
            response.Message.Should().Be("");
            response.Data.Should().BeNull();
            response.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void Response_ShouldSetSuccessStatus_WhenCodeIsInSuccessRange()
        {
            // Arrange
            var responseSuccess200 = new Response<string>("Data", 200, "OK");
            var responseSuccess299 = new Response<string>("Data", 299, "OK");
            var responseFailure199 = new Response<string>("Data", 199, "Before Success Range");
            var responseFailure300 = new Response<string>("Data", 300, "After Success Range");

            // Assert
            responseSuccess200.IsSuccess.Should().BeTrue();   // Código 200 (sucesso)
            responseSuccess299.IsSuccess.Should().BeTrue();   // Código 299 (sucesso)
            responseFailure199.IsSuccess.Should().BeFalse();  // Código 199 (falha)
            responseFailure300.IsSuccess.Should().BeFalse();  // Código 300 (falha)
        }


        [Fact]
        public void Response_ShouldCreateWithGivenValues()
        {
            // Arrange
            string data = "Test data";
            int code = 404;
            string message = "Not found";

            // Act
            var response = new Response<string>(data, code, message);

            // Assert
            response.Code.Should().Be(404);
            response.Message.Should().Be("Not found");
            response.Data.Should().Be("Test data");
            response.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Response_ShouldAllowChangingMessageAndCode_AfterCreation()
        {
            // Arrange
            var response = new Response<string>("Initial data");

            // Act
            response.Message = "Updated message";
            response.Code = 500;

            // Assert
            response.Message.Should().Be("Updated message");
            response.Code.Should().Be(500);
            response.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Response_Generic_ShouldWorkWithDifferentDataTypes()
        {
            // Act
            var responseInt = new Response<int>(100, 200, "OK");
            var responseObject = new Response<object>(new { Name = "Test" }, 200, "OK");

            // Assert
            responseInt.Data.Should().Be(100);
            responseObject.Data.Should().BeEquivalentTo(new { Name = "Test" });
        }
    }
}
