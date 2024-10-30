using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OMG.Domain.Entities;
using OMG.Domain.Enum;
using OMG.Domain.Events;
using OMG.Repository;
using OMG.Repository.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OMG.Repository.Test.Repositories
{
    public class EventRepositoryTest
    {
        private readonly OMGDbContext _context;
        private readonly EventRepository _eventRepository;

        public EventRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<OMGDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new OMGDbContext(options);
            _eventRepository = new EventRepository(_context);
        }

        [Fact]
        public async Task EventChangeStatusPedido_ShouldAddEventChangeStatusToContext()
        {
            // Arrange
            int idPedido = 1;
            EPedidoStatus oldStatus = EPedidoStatus.Novo;
            EPedidoStatus newStatus = EPedidoStatus.Entregue;

            // Act
            await _eventRepository.EventChangeStatusPedido(idPedido, oldStatus, newStatus);

            // Assert
            var eventChangeStatus = await _context.EventChangeStatus.FirstOrDefaultAsync();
            eventChangeStatus.Should().NotBeNull();
            eventChangeStatus.IdPedido.Should().Be(idPedido);
            eventChangeStatus.OldStatus.Should().Be(oldStatus);
            eventChangeStatus.NewStatus.Should().Be(newStatus);
            eventChangeStatus.DataCriacao.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task EventChangeStatusPedido_ShouldSaveChangesToContext()
        {
            // Arrange
            int idPedido = 2;
            EPedidoStatus oldStatus = EPedidoStatus.Novo;
            EPedidoStatus newStatus = EPedidoStatus.Producao;

            // Act
            await _eventRepository.EventChangeStatusPedido(idPedido, oldStatus, newStatus);

            // Assert
            _context.EventChangeStatus.CountAsync().Result.Should().Be(1);
        }
    }
}
