using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;
using OMG.Domain.Events;
using OMG.Repository;
using System;
using Xunit;

namespace OMG.Repository.Test
{
    public class OMGDbContextTest
    {
        private readonly DbContextOptions<OMGDbContext> _options;

        public OMGDbContextTest()
        {
            _options = new DbContextOptionsBuilder<OMGDbContext>()
                .UseInMemoryDatabase(databaseName: $"OMGDb_{Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public void OMGDbContext_ShouldConfigureAllDbSets()
        {
            using var context = new OMGDbContext(_options);

            context.Aromas.Should().NotBeNull();
            context.Clientes.Should().NotBeNull();
            context.Cores.Should().NotBeNull();
            context.Formatos.Should().NotBeNull();
            context.Embalagens.Should().NotBeNull();
            context.Pedidos.Should().NotBeNull();
            context.PedidoItens.Should().NotBeNull();
            context.Produtos.Should().NotBeNull();
            context.EventChangeStatus.Should().NotBeNull();
        }

        [Fact]
        public void OMGDbContext_ShouldApplyEntityConfigurations()
        {
            using var context = new OMGDbContext(_options);

            // This verifies if the OnModelCreating method ran and applied configurations
            Action action = () => context.Model.GetEntityTypes();

            action.Should().NotThrow();
            context.Model.GetEntityTypes().Should().NotBeEmpty();
        }

        [Fact]
        public void CanAddAndRetrieveEntitiesFromDbSets()
        {
            using var context = new OMGDbContext(_options);

            // Arrange
            var aroma = new Aroma { Nome = "Floral" };
            var cliente = new Cliente { Nome = "Cliente Teste" };

            // Act
            context.Aromas.Add(aroma);
            context.Clientes.Add(cliente);
            context.SaveChanges();

            // Assert
            context.Aromas.Should().ContainSingle(a => a.Nome == "Floral");
            context.Clientes.Should().ContainSingle(c => c.Nome == "Cliente Teste");
        }

        [Fact]
        public void CanAddAndRetrieveEventFromEventChangeStatus()
        {
            using var context = new OMGDbContext(_options);

            // Arrange
            var eventChangeStatus = new EventChangeStatus
            {
                IdPedido = 1,
                OldStatus = Domain.Enum.EPedidoStatus.Novo,
                NewStatus = Domain.Enum.EPedidoStatus.Entregue,
                DataCriacao = DateTime.Now
            };

            // Act
            context.EventChangeStatus.Add(eventChangeStatus);
            context.SaveChanges();

            // Assert
            context.EventChangeStatus.Should().ContainSingle(e => e.IdPedido == 1);
        }

        [Fact]
        public void OMGDbContext_Dispose_ShouldDisposeResources()
        {
            // Arrange
            var context = new OMGDbContext(_options);

            // Act
            context.Dispose();

            // Assert
            Action action = () => context.Aromas.ToList();
            action.Should().Throw<ObjectDisposedException>();
        }
    }
}
