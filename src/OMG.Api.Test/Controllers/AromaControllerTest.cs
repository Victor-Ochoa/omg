using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OMG.Api.Controllers;
using OMG.Domain.Base.Contract;
using OMG.Domain.Entities;
using Xunit;

namespace OMG.Api.Test.Controllers
{
    public class AromaControllerTest
    {
        private readonly AromaController _controller;
        private readonly IRepositoryEntity<Aroma> _repository;

        public AromaControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Aroma>>();
            _controller = new AromaController(_repository);
        }

        [Fact]
        public async Task GetEntities_ShouldReturnOkWithAromaList()
        {
            // Arrange
            var aromas = new List<Aroma> { new Aroma { Nome = "Lavanda" }, new Aroma { Nome = "Baunilha" } };
            _repository.GetAll(null).Returns(aromas);

            // Act
            var result = await _controller.GetEntities();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<Aroma>>();
            ((List<Aroma>)okResult!.Value).Should().HaveCount(2);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenAromaDoesNotExist()
        {
            // Arrange
            _repository.Get(Arg.Any<int>()).Returns((Aroma)null);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetEntity_ShouldReturnOkWithAroma_WhenAromaExists()
        {
            // Arrange
            var aroma = new Aroma { Id = 1, Nome = "Lavanda" };
            _repository.Get(1).Returns(aroma);  // Configura o repositório para retornar um Aroma com Id = 1

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            var okResult = result as ActionResult<Aroma>;
            okResult.Should().NotBeNull();  // Verifica se o retorno foi OkObjectResult
            okResult!.Value.Should().BeEquivalentTo(aroma);  // Compara o valor retornado com o objeto esperado
        }

        [Fact]
        public async Task PostEntity_ShouldReturnCreatedAtAction_WhenAromaIsCreated()
        {
            // Arrange
            var aroma = new Aroma { Nome = "Lavanda" };
            _repository.Create(aroma).Returns(aroma);

            // Act
            var result = await _controller.PostEntity(aroma);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.ActionName.Should().Be("GetEntity");
            createdAtActionResult.Value.Should().BeEquivalentTo(aroma);
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNoContent_WhenAromaIsDeleted()
        {
            // Arrange
            _repository.Exist(1).Returns(true);
            _repository.Delete(1).Returns(true);

            // Act
            var result = await _controller.DeleteEntity(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNotFound_WhenAromaDoesNotExist()
        {
            // Arrange
            _repository.Exist(1).Returns(false);

            // Act
            var result = await _controller.DeleteEntity(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
