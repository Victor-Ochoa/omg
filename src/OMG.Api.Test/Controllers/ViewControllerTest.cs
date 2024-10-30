using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OMG.Api.Controllers;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;
using OMG.Domain.Mappers;
using OMG.Domain.ViewModels;
using Xunit;

namespace OMG.Api.Test.Controllers
{
    public class ViewControllerTest
    {
        private readonly IRepositoryEntity<Pedido> _repository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ViewController _viewController;

        public ViewControllerTest()
        {
            _repository = Substitute.For<IRepositoryEntity<Pedido>>();
            _pedidoRepository = Substitute.For<IPedidoRepository>();
            _viewController = new ViewController(_repository, _pedidoRepository);
        }

        [Fact]
        public async Task GetPedidoCardList_ShouldReturnOkWithPedidoCards_WhenPedidosExist()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, Cliente = new Cliente { Nome = "Cliente 1" }, PedidoItens = new List<PedidoItem>(), Status = Domain.Enum.EPedidoStatus.Novo },
                new Pedido { Id = 2, Cliente = new Cliente { Nome = "Cliente 2" }, PedidoItens = new List<PedidoItem>(), Status = Domain.Enum.EPedidoStatus.Novo }
            };
            _pedidoRepository.GetPedidosViewHome().Returns(pedidos);

            // Act
            var result = await _viewController.GetPedidoCardList();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var pedidoCards = okResult!.Value as IEnumerable<PedidoCard>;
            pedidoCards.Should().NotBeNull();
            pedidoCards.Should().HaveCount(pedidos.Count);
            pedidoCards.Select(x => x.NomeCliente).Should().Contain(new[] { "Cliente 1", "Cliente 2" });
        }

        [Fact]
        public async Task GetPedidoCardList_ShouldReturnOkWithEmptyList_WhenNoPedidosExist()
        {
            // Arrange
            _pedidoRepository.GetPedidosViewHome().Returns(new List<Pedido>());

            // Act
            var result = await _viewController.GetPedidoCardList();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var pedidoCards = okResult!.Value as IEnumerable<PedidoCard>;
            pedidoCards.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPedidoModal_ShouldReturnOkWithPedidoModal_WhenPedidoExists()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Pedido { Id = pedidoId, Cliente = new Cliente { Nome = "Cliente 1" }, PedidoItens = new List<PedidoItem>(), Status = Domain.Enum.EPedidoStatus.Novo };
            _repository.Get(pedidoId).Returns(pedido);

            // Act
            var result = await _viewController.GetPedidoModal(pedidoId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var pedidoModal = okResult!.Value as PedidoModal;
            pedidoModal.Should().NotBeNull();
            pedidoModal!.ClienteNome.Should().Be("Cliente 1");
            pedidoModal.PedidoId.Should().Be(pedidoId);
        }

        [Fact]
        public async Task GetPedidoModal_ShouldReturnNotFound_WhenPedidoDoesNotExist()
        {
            // Arrange
            var pedidoId = 1;
            _repository.Get(pedidoId).Returns((Pedido)null);

            // Act
            var result = await _viewController.GetPedidoModal(pedidoId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
