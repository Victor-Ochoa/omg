using FluentAssertions;
using OMG.Domain.Entities;
using OMG.Domain.Mappers;
using OMG.Domain.ViewModels;
using Xunit;

namespace OMG.Domain.Test.Mappers
{
    public class PedidoItemMapperTests
    {
        [Fact]
        public void ConvertToPedidoItemModal_Should_Map_Properties_Correctly()
        {
            // Arrange
            var pedidoItem = new PedidoItem
            {
                Id = 1,
                Quantidade = 2,
                Cor = new Cor { Nome = "Vermelho" },
                Formato = new Formato { Descricao = "Grande" },
                Produto = new Produto { Descricao = "Produto A" },
                Aroma = new Aroma { Nome = "Frutado" },
                Embalagem = new Embalagem { Descricao = "Caixa" }
            };

            // Act
            var result = pedidoItem.ConvertToPedidoItemModal();

            // Assert
            result.Should().NotBeNull();
            result.ItemId.Should().Be(pedidoItem.Id);
            result.Quantidade.Should().Be(pedidoItem.Quantidade);
            result.Cor.Should().Be(pedidoItem.Cor.Nome);
            result.Formato.Should().Be(pedidoItem.Formato.Descricao);
            result.Produto.Should().Be(pedidoItem.Produto.Descricao);
            result.Aroma.Should().Be(pedidoItem.Aroma.Nome);
            result.Embalagem.Should().Be(pedidoItem.Embalagem.Descricao);
        }
    }
}
