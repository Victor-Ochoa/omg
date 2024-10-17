using FluentAssertions;
using OMG.Domain.Entities;
using OMG.Domain.Enum;
using OMG.Domain.Mappers;
using OMG.Domain.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace OMG.Domain.Test.Mappers
{
    public class PedidoMapperTests
    {
        [Fact]
        public void ConvertToPedidoModal_ShouldReturnNull_WhenPedidoIsNull()
        {
            // Arrange
            Pedido pedido = null;

            // Act
            var result = pedido.ConvertToPedidoModal();

            // Assert
            result.Should().BeNull(); // Verifica se o retorno é null
        }

        [Fact]
        public void ConvertToPedidoModal_Should_Map_Properties_Correctly()
        {
            // Arrange
            var pedido = new Pedido
            {
                Id = 1,
                IsPermuta = false,
                Entrada = 100,
                ValorTotal = 300,
                Desconto = 50,
                DataEntrega = new DateOnly(2024, 12, 31),
                Cliente = new Cliente { Nome = "João", Telefone = "123456789", Endereco = "Rua A, 123" },
                PedidoItens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        Quantidade = 2,
                        Cor = new Cor { Nome = "Vermelho" },
                        Formato = new Formato { Descricao = "Grande" },
                        Produto = new Produto { Descricao = "Produto A" },
                        Aroma = new Aroma { Nome = "Frutado" },
                        Embalagem = new Embalagem { Descricao = "Caixa" }
                    },
                    new PedidoItem
                    {
                        Quantidade = 3,
                        Cor = new Cor { Nome = "Azul" },
                        Formato = new Formato { Descricao = "Médio" },
                        Produto = new Produto { Descricao = "Produto B" },
                        Aroma = new Aroma { Nome = "Cítrico" },
                        Embalagem = new Embalagem { Descricao = "Pacote" }
                    }
                },
                Status = EPedidoStatus.Novo
            };

            // Act
            var result = pedido.ConvertToPedidoModal();

            // Assert
            result.Should().NotBeNull();
            result.ClienteNome.Should().Be(pedido.Cliente.Nome);
            result.ClienteTelefone.Should().Be(pedido.Cliente.Telefone);
            result.ClienteEndereco.Should().Be(pedido.Cliente.Endereco);
            result.PedidoId.Should().Be(pedido.Id);
            result.Permuta.Should().Be(pedido.IsPermuta);
            result.ValorPago.Should().Be(pedido.Entrada);
            result.ValorReceber.Should().Be(pedido.ValorTotal - pedido.Entrada - pedido.Desconto);
            result.ValorTotal.Should().Be(pedido.ValorTotal - pedido.Desconto);
            result.DataEntrega.Should().Be(pedido.DataEntrega);
            result.PedidoItens.Should().HaveCount(pedido.PedidoItens.Count);
        }

        [Fact]
        public void ConvertToPedidoCard_Should_Map_Properties_Correctly()
        {
            // Arrange
            var pedido = new Pedido
            {
                Id = 2,
                Status = EPedidoStatus.Novo,
                Cliente = new Cliente { Nome = "Maria" },
                Desconto = 20,
                ValorTotal = 200,
                DataEntrega = new DateOnly(2024, 12, 31),
                PedidoItens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        Quantidade = 2,
                        Cor = new Cor { Nome = "Vermelho" },
                        Formato = new Formato { Descricao = "Pequeno" },
                        Produto = new Produto { Descricao = "Produto C" },
                        Aroma = new Aroma { Nome = "Doce" },
                        Embalagem = new Embalagem { Descricao = "Saco" }
                    },
                    new PedidoItem
                    {
                        Quantidade = 5,
                        Cor = new Cor { Nome = "Verde" },
                        Formato = new Formato { Descricao = "Médio" },
                        Produto = new Produto { Descricao = "Produto D" },
                        Aroma = new Aroma { Nome = "Picante" },
                        Embalagem = new Embalagem { Descricao = "Caixa" }
                    }
                }
            };

            // Act
            var result = pedido.ConvertToPedidoCard();

            // Assert
            result.Should().NotBeNull();
            result.PedidoId.Should().Be(pedido.Id);
            result.Status.Should().Be(pedido.Status);
            result.NomeCliente.Should().Be(pedido.Cliente.Nome);
            result.TotalItens.Should().Be(7); // 2 + 5
            result.ValorTotal.Should().Be(pedido.ValorTotal - pedido.Desconto);
            result.DataEntrega.Should().Be(pedido.DataEntrega);
        }
        [Fact]
        public void ConvertToPedidoModal_ShouldReturnCorrectModal_WhenIsPermutaIsTrue()
        {
            // Arrange
            var pedido = new Pedido
            {
                Cliente = new Cliente { Endereco = "Rua A", Nome = "Cliente A", Telefone = "123456789" },
                Id = 1,
                IsPermuta = true, // Caso IsPermuta seja true
                Entrada = 100m,
                ValorTotal = 1000m,
                Desconto = 50m,
                PedidoItens = new List<PedidoItem>
        {
            new PedidoItem { Produto = new Produto { Descricao = "Produto A" }, Quantidade = 2, Aroma = new Aroma { Nome = "Aroma A" }, Cor = new Cor { Nome = "Cor A" }, Formato = new Formato { Descricao = "Formato A" }, Embalagem = new Embalagem { Descricao = "Embalagem A" } }
        },
                DataEntrega = DateOnly.FromDateTime(DateTime.Now),
                Status = EPedidoStatus.Novo
            };

            // Act
            var result = pedido.ConvertToPedidoModal();

            // Assert
            result.Should().NotBeNull();
            result.Permuta.Should().BeTrue();
            result.ValorReceber.Should().Be(0); // Quando IsPermuta é true, ValorReceber deve ser 0
        }

        [Fact]
        public void ConvertToPedidoModal_ShouldReturnCorrectModal_WhenIsPermutaIsFalse()
        {
            // Arrange
            var pedido = new Pedido
            {
                Cliente = new Cliente { Endereco = "Rua A", Nome = "Cliente A", Telefone = "123456789" },
                Id = 1,
                IsPermuta = false, // Caso IsPermuta seja false
                Entrada = 100m,
                ValorTotal = 1000m,
                Desconto = 50m,
                PedidoItens = new List<PedidoItem>
        {
            new PedidoItem { Produto = new Produto { Descricao = "Produto A" }, Quantidade = 2, Aroma = new Aroma { Nome = "Aroma A" }, Cor = new Cor { Nome = "Cor A" }, Formato = new Formato { Descricao = "Formato A" }, Embalagem = new Embalagem { Descricao = "Embalagem A" } }
        },
                DataEntrega = DateOnly.FromDateTime(DateTime.Now),
                Status = EPedidoStatus.Novo
            };

            // Act
            var result = pedido.ConvertToPedidoModal();

            // Assert
            result.Should().NotBeNull();
            result.Permuta.Should().BeFalse();
            result.ValorReceber.Should().Be(850m); // Quando IsPermuta é false, deve ser calculado
        }

    }
}
