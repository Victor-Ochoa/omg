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
    public class CorControllerTest
    {
        private readonly CorController _controller;
        private readonly IRepositoryEntity<Cor> _repository;

        public CorControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Cor>>();
            _controller = new CorController(_repository);
        }

        [Fact]
        public async Task GetEntities_ShouldReturnOkWithCorList()
        {
            var cores = new List<Cor> { new Cor { Nome = "Azul" }, new Cor { Nome = "Verde" } };
            _repository.GetAll(null).Returns(cores);

            var result = await _controller.GetEntities();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<Cor>>();
            ((List<Cor>)okResult!.Value).Should().HaveCount(2);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenCorDoesNotExist()
        {
            _repository.Get(Arg.Any<int>()).Returns((Cor)null);

            var result = await _controller.GetEntity(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetEntity_ShouldReturnOkWithCor_WhenCorExists()
        {
            var cor = new Cor { Id = 1, Nome = "Azul" };
            _repository.Get(1).Returns(cor);

            var result = await _controller.GetEntity(1);

            result!.Value.Should().BeEquivalentTo(cor);
        }

        [Fact]
        public async Task PostEntity_ShouldReturnCreatedAtAction_WhenCorIsCreated()
        {
            var cor = new Cor { Nome = "Azul" };
            _repository.Create(cor).Returns(cor);

            var result = await _controller.PostEntity(cor);

            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.ActionName.Should().Be("GetEntity");
            createdAtActionResult.Value.Should().BeEquivalentTo(cor);
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNoContent_WhenCorIsDeleted()
        {
            _repository.Exist(1).Returns(true);
            _repository.Delete(1).Returns(true);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteEntity_ShouldReturnNotFound_WhenCorDoesNotExist()
        {
            _repository.Exist(1).Returns(false);

            var result = await _controller.DeleteEntity(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
