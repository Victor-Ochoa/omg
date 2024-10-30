using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OMG.Api.Controllers;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;
using OMG.Domain.Request;
using OMG.Domain.Base.Contract;
using Xunit;
using OMG.Domain.Enum;

namespace OMG.Api.Test.Controllers
{
    public class PedidoControllerTest
    {
        private readonly IRepositoryEntity<Pedido> _pedidoRepository;
        private readonly IPedidoService _pedidoService;
        private readonly PedidoController _pedidoController;

        public PedidoControllerTest()
        {
            _pedidoRepository = Substitute.For<IRepositoryEntity<Pedido>>();
            _pedidoService = Substitute.For<IPedidoService>();
            _pedidoController = new PedidoController(_pedidoRepository, _pedidoService);
        }

        [Fact]
        public async Task GetPedido_ShouldReturnOkWithPedido_WhenPedidoExists()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Pedido { Id = pedidoId, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>(), Status = EPedidoStatus.Novo };
            _pedidoRepository.Get(pedidoId).Returns(pedido);

            // Act
            var result = await _pedidoController.GetPedido(pedidoId);

            // Assert
            result!.Value.Should().BeEquivalentTo(pedido);
        }

        [Fact]
        public async Task GetPedido_ShouldReturnNotFound_WhenPedidoDoesNotExist()
        {
            // Arrange
            var pedidoId = 1;
            _pedidoRepository.Get(pedidoId).Returns((Pedido)null);

            // Act
            var result = await _pedidoController.GetPedido(pedidoId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ChangeStatus_ShouldReturnNoContent_WhenPedidoExists()
        {
            // Arrange
            var pedidoId = 1;
            var newStatus = EPedidoStatus.Novo;
            var request = new PedidoChangeStatusRequest(pedidoId, newStatus);
            _pedidoRepository.Exist(pedidoId).Returns(true);

            // Act
            var result = await _pedidoController.ChangeStatus(request);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            await _pedidoService.Received(1).ChangeStatus(pedidoId, newStatus);
        }

        [Fact]
        public async Task ChangeStatus_ShouldReturnNotFound_WhenPedidoDoesNotExist()
        {
            // Arrange
            var pedidoId = 1;
            var newStatus = EPedidoStatus.Novo;
            var request = new PedidoChangeStatusRequest(pedidoId, newStatus);
            _pedidoRepository.Exist(pedidoId).Returns(false);

            // Act
            var result = await _pedidoController.ChangeStatus(request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            await _pedidoService.DidNotReceive().ChangeStatus(Arg.Any<int>(), Arg.Any<EPedidoStatus>());
        }

        [Fact]
        public async Task NewPedido_ShouldReturnCreatedWithPedido_WhenPedidoIsCreatedSuccessfully()
        {
            // Arrange
            var newPedidoRequest = new NewPedidoRequest();
            var createdPedido = new Pedido { Id = 1, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>(), Status = EPedidoStatus.Novo };
            _pedidoService.CreateNewPedido(newPedidoRequest).Returns(createdPedido);

            // Act
            var result = await _pedidoController.NewPedido(newPedidoRequest);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(createdPedido);
            createdAtActionResult.ActionName.Should().Be("GetPedido");
            createdAtActionResult.RouteValues["id"].Should().Be(createdPedido.Id);
        }
    }
}
