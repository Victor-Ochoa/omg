using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OMG.Api.Controllers;
using OMG.Domain.Base.Contract;
using OMG.Domain.Entities;
using Xunit;

namespace OMG.Api.Test.Controllers
{
    public class ClienteControllerTest
    {
        private readonly ClienteController _controller;
        private readonly IRepositoryEntity<Cliente> _repository;

        public ClienteControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Cliente>>();
            _controller = new ClienteController(_repository);
        }

        [Fact]
        public async Task GetEntities_ShouldReturnOkWithClienteList()
        {
            var clientes = new List<Cliente> { new Cliente { Nome = "João" }, new Cliente { Nome = "Maria" } };
            _repository.GetAll(null).Returns(clientes);

            var result = await _controller.GetEntities();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<Cliente>>();
            ((List<Cliente>)okResult!.Value).Should().HaveCount(2);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenClienteDoesNotExist()
        {
            _repository.Get(Arg.Any<int>()).Returns((Cliente)null);

            var result = await _controller.GetEntity(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetEntity_ShouldReturnOkWithCliente_WhenClienteExists()
        {
            var cliente = new Cliente { Id = 1, Nome = "João" };
            _repository.Get(1).Returns(cliente);

            var result = await _controller.GetEntity(1);

            result!.Value.Should().BeEquivalentTo(cliente);
        }

        [Fact]
        public async Task PostEntity_ShouldReturnCreatedAtAction_WhenClienteIsCreated()
        {
            var cliente = new Cliente { Nome = "João" };
            _repository.Create(cliente).Returns(cliente);

            var result = await _controller.PostEntity(cliente);

            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.ActionName.Should().Be("GetEntity");
            createdAtActionResult.Value.Should().BeEquivalentTo(cliente);
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNoContent_WhenClienteIsDeleted()
        {
            _repository.Exist(1).Returns(true);
            _repository.Delete(1).Returns(true);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNotFound_WhenClienteDoesNotExist()
        {
            _repository.Exist(1).Returns(false);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
