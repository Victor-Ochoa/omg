using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using OMG.Domain.Entities;
using OMG.Domain.Enum;
using OMG.Domain.Queries;

namespace OMG.Domain.Test.Queries
{
    public class PedidoQueryTests
    {
        [Fact]
        public void GetPedidoExcludePedidoStatus_ShouldReturnCorrectExpression()
        {
            // Arrange
            EPedidoStatus excludeStatus = EPedidoStatus.Entregue;

            // Act
            Expression<Func<Pedido, bool>> expression = PedidoQuery.GetPedidoExcludePedidoStatus(excludeStatus);
            var compiledExpression = expression.Compile();

            // Test Case 1: Pedido com Status != Entregue
            var pedido1 = new Pedido { Status = EPedidoStatus.Producao, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>() };
            bool result1 = compiledExpression(pedido1);

            // Test Case 2: Pedido com Status == Entregue
            var pedido2 = new Pedido { Status = EPedidoStatus.Entregue, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>() };
            bool result2 = compiledExpression(pedido2);

            // Assert
            Assert.True(result1);  // Deve retornar true porque Status é diferente de Entregue
            Assert.False(result2); // Deve retornar false porque Status é Entregue
        }

        [Fact]
        public void GetPedidoWherePedidoStatusEqualAndDataEntregaMenorQue_ShouldReturnCorrectExpression()
        {
            // Arrange
            EPedidoStatus status = EPedidoStatus.Entregue;
            int diasExcluirProntos = 14;
            DateOnly dataEntregaValida = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
            DateOnly dataEntregaInvalida = DateOnly.FromDateTime(DateTime.Now.AddDays(-20));

            // Act
            Expression<Func<Pedido, bool>> expression = PedidoQuery.GetPedidoWherePedidoStatusEqualAndDataEntregaMenorQue(status, diasExcluirProntos);
            var compiledExpression = expression.Compile();

            // Test Case 1: Pedido com Status == Entregue e DataEntrega dentro dos últimos 14 dias
            var pedido1 = new Pedido { Status = status, DataEntrega = dataEntregaValida, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>() };
            bool result1 = compiledExpression(pedido1);

            // Test Case 2: Pedido com Status == Entregue e DataEntrega fora dos últimos 14 dias
            var pedido2 = new Pedido { Status = status, DataEntrega = dataEntregaInvalida, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>() };
            bool result2 = compiledExpression(pedido2);

            // Test Case 3: Pedido com Status diferente de Entregue
            var pedido3 = new Pedido { Status = EPedidoStatus.Producao, DataEntrega = dataEntregaValida, Cliente = new Cliente(), PedidoItens = Array.Empty<PedidoItem>() };
            bool result3 = compiledExpression(pedido3);

            // Assert
            Assert.True(result1);  // Deve retornar true porque o Status é Entregue e a DataEntrega está dentro dos últimos 14 dias
            Assert.False(result2); // Deve retornar false porque a DataEntrega está fora dos últimos 14 dias
            Assert.False(result3); // Deve retornar false porque o Status é diferente de Entregue
        }
    }
}
