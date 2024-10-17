using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;
using OMG.Domain.Services;
using Xunit;

namespace OMG.Domain.Test.Services
{
    public class ProdutoServiceTests
    {
        private readonly IProdutoService _produtoService;
        private readonly IRepositoryEntity<Produto> _repository;

        public ProdutoServiceTests()
        {
            _repository = Substitute.For<IRepositoryEntity<Produto>>();
            _produtoService = new ProdutoService(_repository);
        }

        [Fact]
        public async Task GetFromDescricao_ExistingProduto_ReturnsProduto()
        {
            // Arrange
            var descricao = "Sabonete";
            var expectedProduto = new Produto { Descricao = descricao };
            _repository.Get(Arg.Any<Expression<Func<Produto, bool>>>()).Returns(Task.FromResult(expectedProduto));

            // Act
            var result = await _produtoService.GetFromDescricao(descricao);

            // Assert
            result.Should().BeEquivalentTo(expectedProduto, options => options.Excluding(p => p.Id));
            await _repository.Received(1).Get(Arg.Any<Expression<Func<Produto, bool>>>());
            await _repository.DidNotReceive().Create(Arg.Any<Produto>());
        }

        [Fact]
        public async Task GetFromDescricao_NonExistingProduto_CreatesAndReturnsProduto()
        {
            // Arrange
            var descricao = "Sabonete Liquido";
            _repository.Get(Arg.Any<Expression<Func<Produto, bool>>>()).Returns(Task.FromResult<Produto>(null));
            var createdProduto = new Produto { Descricao = descricao };
            _repository.Create(Arg.Any<Produto>()).Returns(Task.FromResult(createdProduto));

            // Act
            var result = await _produtoService.GetFromDescricao(descricao);

            // Assert
            result.Should().BeEquivalentTo(createdProduto, options => options.Excluding(p => p.Id));
            await _repository.Received(1).Get(Arg.Any<Expression<Func<Produto, bool>>>());
            await _repository.Received(1).Create(Arg.Is<Produto>(p => p.Descricao == descricao));
        }
    }
}
