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
    public class FormatoControllerTest
    {
        private readonly FormatoController _controller;
        private readonly IRepositoryEntity<Formato> _repository;

        public FormatoControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Formato>>();
            _controller = new FormatoController(_repository);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithEmbalagens_WhenEmbalagensExist()
        {
            // Arrange
            var embalagens = new List<Formato>
            {
                new Formato { Id = 1, Descricao = "Caixa" },
                new Formato { Id = 2, Descricao = "Envelope" }
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
        public async Task GetEntity_ShouldReturnOkWithFormato_WhenFormatoExists()
        {
            // Arrange
            var formato = new Formato { Id = 1, Descricao = "Caixa" };
            _repository.Get(1).Returns(formato);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result!.Value.Should().BeEquivalentTo(formato);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenFormatoDoesNotExist()
        {
            // Arrange
            _repository.Get(1).Returns((Formato)null);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenFormatoIsCreated()
        {
            // Arrange
            var newFormato = new Formato { Descricao = "Caixa" };
            var createdFormato = new Formato { Id = 1, Descricao = "Caixa" };
            _repository.Create(newFormato).Returns(createdFormato);

            // Act
            var result = await _controller.PostEntity(newFormato);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.Value.Should().BeEquivalentTo(createdFormato);
            createdResult.ActionName.Should().Be("GetEntity");
            createdResult.RouteValues["id"].Should().Be(createdFormato.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenFormatoIsUpdated()
        {
            // Arrange
            var formato = new Formato { Id = 1, Descricao = "Caixa" };
            _repository.Update(formato).Returns(formato);

            // Act
            var result = await _controller.PutEntity(formato.Id,formato);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNoContent_WhenFormatoIsDeleted()
        {
            _repository.Exist(1).Returns(true);
            _repository.Delete(1).Returns(true);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNotFound_WhenFormatoDoesNotExist()
        {
            _repository.Exist(1).Returns(false);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
