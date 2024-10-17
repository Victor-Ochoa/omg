using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;
using OMG.Domain.Enum;
using OMG.Domain.Request;
using OMG.Domain.Services;
using Xunit;

namespace OMG.Domain.Test.Services
{
    public class PedidoServiceTests
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IClienteService _clienteService;
        private readonly ICorService _corService;
        private readonly IAromaService _aromaService;
        private readonly IProdutoService _produtoService;
        private readonly IFormatoService _formatoService;
        private readonly IEmbalagemService _embalagemService;

        public PedidoServiceTests()
        {
            _pedidoRepository = Substitute.For<IPedidoRepository>();
            _eventRepository = Substitute.For<IEventRepository>();
            _clienteService = Substitute.For<IClienteService>();
            _corService = Substitute.For<ICorService>();
            _aromaService = Substitute.For<IAromaService>();
            _produtoService = Substitute.For<IProdutoService>();
            _formatoService = Substitute.For<IFormatoService>();
            _embalagemService = Substitute.For<IEmbalagemService>();

            _pedidoService = new PedidoService(
                _pedidoRepository,
                _eventRepository,
                _clienteService,
                _corService,
                _aromaService,
                _produtoService,
                _formatoService,
                _embalagemService);
        }

        [Fact]
        public async Task ChangeStatus_Should_ChangePedidoStatus_And_RecordEvent()
        {
            // Arrange
            var idPedido = 1;
            var oldStatus = EPedidoStatus.Novo;
            var newStatus = EPedidoStatus.Entregue;
            _pedidoRepository.GetPedidoStatus(idPedido).Returns(Task.FromResult(oldStatus));

            // Act
            await _pedidoService.ChangeStatus(idPedido, newStatus);

            // Assert
            await _pedidoRepository.Received(1).ChangePedidoStatus(idPedido, newStatus);
            await _eventRepository.Received(1).EventChangeStatusPedido(idPedido, oldStatus, newStatus);
        }

        [Fact]
        public async Task CreateNewPedido_Should_CreatePedido_WithCorrectDetails()
        {
            // Arrange
            var newPedidoRequest = new NewPedidoRequest
            {
                ClienteId = 1,
                ValorTotal = 1000m,
                ValorDesconto = 50m,
                ValorEntrada = 200m,
                IsPermuta = false,
                DataEntrega = DateTime.Today.AddDays(5),
                Itens = new List<NewPedidoItemRequest>
                {
                    new NewPedidoItemRequest(1, "Produto A", "Formato A", "Cor A", "Aroma A", "Embalagem A"),
                    new NewPedidoItemRequest(2, "Produto B", "Formato B", "Cor B", "Aroma B", "Embalagem B")
                }
            };

            var cliente = new Cliente { Id = 1 };
            var produtoA = new Produto { Descricao = "Produto A" };
            var produtoB = new Produto { Descricao = "Produto B" };
            var formatoA = new Formato { Descricao = "Formato A" };
            var formatoB = new Formato { Descricao = "Formato B" };
            var corA = new Cor { Nome = "Cor A" };
            var corB = new Cor { Nome = "Cor B" };
            var aromaA = new Aroma { Nome = "Aroma A" };
            var aromaB = new Aroma { Nome = "Aroma B" };
            var embalagemA = new Embalagem { Descricao = "Embalagem A" };
            var embalagemB = new Embalagem { Descricao = "Embalagem B" };

            _clienteService.Get(newPedidoRequest.ClienteId).Returns(Task.FromResult(cliente));
            _produtoService.GetFromDescricao("Produto A").Returns(Task.FromResult(produtoA));
            _produtoService.GetFromDescricao("Produto B").Returns(Task.FromResult(produtoB));
            _formatoService.GetFromDescricao("Formato A").Returns(Task.FromResult(formatoA));
            _formatoService.GetFromDescricao("Formato B").Returns(Task.FromResult(formatoB));
            _corService.GetFromName("Cor A").Returns(Task.FromResult(corA));
            _corService.GetFromName("Cor B").Returns(Task.FromResult(corB));
            _aromaService.GetFromName("Aroma A").Returns(Task.FromResult(aromaA));
            _aromaService.GetFromName("Aroma B").Returns(Task.FromResult(aromaB));
            _embalagemService.GetFromDescricao("Embalagem A").Returns(Task.FromResult(embalagemA));
            _embalagemService.GetFromDescricao("Embalagem B").Returns(Task.FromResult(embalagemB));

            // Act
            var result = await _pedidoService.CreateNewPedido(newPedidoRequest);

            // Assert
            result.Cliente.Should().Be(cliente);
            result.ValorTotal.Should().Be(1000m);
            result.Desconto.Should().Be(50m);
            result.Entrada.Should().Be(200m);
            result.DataEntrega.Should().Be(DateOnly.FromDateTime(DateTime.Today.AddDays(5)));
            result.PedidoItens.Should().HaveCount(2);

            result.PedidoItens[0].Produto.Should().Be(produtoA);
            result.PedidoItens[0].Formato.Should().Be(formatoA);
            result.PedidoItens[0].Cor.Should().Be(corA);
            result.PedidoItens[0].Aroma.Should().Be(aromaA);
            result.PedidoItens[0].Embalagem.Should().Be(embalagemA);

            result.PedidoItens[1].Produto.Should().Be(produtoB);
            result.PedidoItens[1].Formato.Should().Be(formatoB);
            result.PedidoItens[1].Cor.Should().Be(corB);
            result.PedidoItens[1].Aroma.Should().Be(aromaB);
            result.PedidoItens[1].Embalagem.Should().Be(embalagemB);

            await _pedidoRepository.Received(1).Create(result);
        }
    }
}
