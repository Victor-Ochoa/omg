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
    public class ProdutoControllerTest
    {
        private readonly ProdutoController _controller;
        private readonly IRepositoryEntity<Produto> _repository;

        public ProdutoControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Produto>>();
            _controller = new ProdutoController(_repository);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithEmbalagens_WhenEmbalagensExist()
        {
            // Arrange
            var embalagens = new List<Produto>
            {
                new Produto { Id = 1, Descricao = "Caixa" },
                new Produto { Id = 2, Descricao = "Envelope" }
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
        public async Task GetEntity_ShouldReturnOkWithProduto_WhenProdutoExists()
        {
            // Arrange
            var produto = new Produto { Id = 1, Descricao = "Caixa" };
            _repository.Get(1).Returns(produto);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result!.Value.Should().BeEquivalentTo(produto);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenProdutoDoesNotExist()
        {
            // Arrange
            _repository.Get(1).Returns((Produto)null);

            // Act
            var result = await _controller.GetEntity(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenProdutoIsCreated()
        {
            // Arrange
            var newProduto = new Produto { Descricao = "Caixa" };
            var createdProduto = new Produto { Id = 1, Descricao = "Caixa" };
            _repository.Create(newProduto).Returns(createdProduto);

            // Act
            var result = await _controller.PostEntity(newProduto);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.Value.Should().BeEquivalentTo(createdProduto);
            createdResult.ActionName.Should().Be("GetEntity");
            createdResult.RouteValues["id"].Should().Be(createdProduto.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenProdutoIsUpdated()
        {
            // Arrange
            var produto = new Produto { Id = 1, Descricao = "Caixa" };
            _repository.Update(produto).Returns(produto);

            // Act
            var result = await _controller.PutEntity(produto.Id, produto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNoContent_WhenProdutoIsDeleted()
        {
            _repository.Exist(1).Returns(true);
            _repository.Delete(1).Returns(true);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNotFound_WhenProdutoDoesNotExist()
        {
            _repository.Exist(1).Returns(false);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
