using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;
using OMG.Domain.Enum;
using OMG.Repository;
using OMG.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMG.Repository.Test.Repositories
{
    public class PedidoRepositoryTest
    {
        private readonly OMGDbContext _context;
        private readonly IRepositoryEntity<Pedido> _repository;
        private readonly PedidoRepository _pedidoRepository;

        public PedidoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<OMGDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new OMGDbContext(options);
            _repository = Substitute.For<IRepositoryEntity<Pedido>>();
            _pedidoRepository = new PedidoRepository(_context, _repository);
        }

        [Fact]
        public async Task ChangePedidoStatus_ShouldUpdateStatus_WhenPedidoExists()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, Status = EPedidoStatus.Novo, PedidoItens = Array.Empty<PedidoItem>(), Cliente = new Cliente()};
            _repository.Exist(1).Returns(true);
            _repository.Get(1).Returns(Task.FromResult(pedido));

            // Act
            await _pedidoRepository.ChangePedidoStatus(1, EPedidoStatus.Entregue);

            // Assert
            pedido.Status.Should().Be(EPedidoStatus.Entregue);
            await _repository.Received(1).Update(pedido);
        }

        [Fact]
        public async Task ChangePedidoStatus_ShouldThrowException_WhenPedidoDoesNotExist()
        {
            // Arrange
            _repository.Exist(1).Returns(false);

            // Act
            Func<Task> act = async () => await _pedidoRepository.ChangePedidoStatus(1, EPedidoStatus.Entregue);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Pedido (1) não encontrado");
        }

        [Fact]
        public async Task Create_ShouldCallRepositoryCreate()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, Status = EPedidoStatus.Novo, PedidoItens = Array.Empty<PedidoItem>(), Cliente = new Cliente() };

            // Act
            await _pedidoRepository.Create(pedido);

            // Assert
            await _repository.Received(1).Create(pedido);
        }

        [Fact]
        public async Task GetPedidoStatus_ShouldReturnStatus_WhenPedidoExists()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, Status = EPedidoStatus.Producao, PedidoItens = Array.Empty<PedidoItem>(), Cliente = new Cliente() };
            _repository.Exist(1).Returns(true);
            _repository.Get(1).Returns(Task.FromResult(pedido));

            // Act
            var result = await _pedidoRepository.GetPedidoStatus(1);

            // Assert
            result.Should().Be(EPedidoStatus.Producao);
        }

        [Fact]
        public async Task GetPedidoStatus_ShouldThrowException_WhenPedidoDoesNotExist()
        {
            // Arrange
            _repository.Exist(1).Returns(false);

            // Act
            Func<Task> act = async () => await _pedidoRepository.GetPedidoStatus(1);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Pedido (1) não encontrado");
        }

        [Fact]
        public async Task GetPedidosViewHome_ShouldReturnPedidosList()
        {
            // Arrange
            var diasExcluirProntos = 14;
            var pedido1 = new Pedido { Id = 1, Status = EPedidoStatus.Novo, PedidoItens = Array.Empty<PedidoItem>(), Cliente = new Cliente() };
            var pedido2 = new Pedido { Id = 2, Status = EPedidoStatus.Entregue, DataEntrega = DateOnly.FromDateTime(DateTime.Now).AddDays(-10), PedidoItens = Array.Empty<PedidoItem>(), Cliente = new Cliente() };
            _context.Pedidos.AddRange(pedido1, pedido2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _pedidoRepository.GetPedidosViewHome(diasExcluirProntos);

            // Assert
            result.Should().ContainSingle(p => p.Id == 2);
        }
    }
}
