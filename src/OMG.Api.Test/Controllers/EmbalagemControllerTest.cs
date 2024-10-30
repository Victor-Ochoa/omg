using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OMG.Api.Controllers;
using OMG.Domain.Entities;
using OMG.Domain.Base.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OMG.Api.Test.Controllers
{
    public class EmbalagemControllerTest
    {
        private readonly EmbalagemController _controller;
        private readonly IRepositoryEntity<Embalagem> _repository;

        public EmbalagemControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Embalagem>>();
            _controller = new EmbalagemController(_repository);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithEmbalagens_WhenEmbalagensExist()
        {
            // Arrange
            var embalagens = new List<Embalagem>
            {
                new Embalagem { Id = 1, Descricao = "Caixa" },
                new Embalagem { Id = 2, Descricao = "Envelope" }
            };

            _repository.GetAll(null).Returns(embalagens);

            // Act
            var result = await _controller.GetEntities();

            // Assert
            result.Result.Should().NotBeNull();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(embalagens);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnOkWithEmbalagem_WhenEmbalagemExists()
        {
            // Arrange
            var embalagem = new Embalagem { Id = 1, Descricao = "Caixa" };
            _repository.Get(1).Returns(embalagem);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result!.Value.Should().BeEquivalentTo(embalagem);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenEmbalagemDoesNotExist()
        {
            // Arrange
            _repository.Get(1).Returns((Embalagem)null);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenEmbalagemIsCreated()
        {
            // Arrange
            var newEmbalagem = new Embalagem { Descricao = "Caixa" };
            var createdEmbalagem = new Embalagem { Id = 1, Descricao = "Caixa" };
            _repository.Create(newEmbalagem).Returns(createdEmbalagem);

            // Act
            var result = await _controller.PostEntity(newEmbalagem);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.Value.Should().BeEquivalentTo(createdEmbalagem);
            createdResult.ActionName.Should().Be("GetEntity");
            createdResult.RouteValues["id"].Should().Be(createdEmbalagem.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenEmbalagemIsUpdated()
        {
            // Arrange
            var embalagem = new Embalagem { Id = 1, Descricao = "Caixa" };
            _repository.Update(embalagem).Returns(embalagem);

            // Act
            var result = await _controller.PutEntity(embalagem.Id,embalagem);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNoContent_WhenEmbalagemIsDeleted()
        {
            _repository.Exist(1).Returns(true);
            _repository.Delete(1).Returns(true);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNotFound_WhenEmbalagemDoesNotExist()
        {
            _repository.Exist(1).Returns(false);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
