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
    public class EmbalagemServiceTests
    {
        private readonly IEmbalagemService _embalagemService;
        private readonly IRepositoryEntity<Embalagem> _repository;

        public EmbalagemServiceTests()
        {
            _repository = Substitute.For<IRepositoryEntity<Embalagem>>();
            _embalagemService = new EmbalagemService(_repository);
        }

        [Fact]
        public async Task GetFromDescricao_ExistingEmbalagem_ReturnsEmbalagem()
        {
            // Arrange
            var descricao = "Caixa";
            var expectedEmbalagem = new Embalagem { Descricao = descricao };
            _repository.Get(Arg.Any<Expression<Func<Embalagem, bool>>>()).Returns(Task.FromResult(expectedEmbalagem));

            // Act
            var result = await _embalagemService.GetFromDescricao(descricao);

            // Assert
            result.Should().BeEquivalentTo(expectedEmbalagem, options => options.Excluding(e => e.Id));
            await _repository.Received(1).Get(Arg.Any<Expression<Func<Embalagem, bool>>>());
            await _repository.DidNotReceive().Create(Arg.Any<Embalagem>());
        }

        [Fact]
        public async Task GetFromDescricao_NonExistingEmbalagem_CreatesAndReturnsEmbalagem()
        {
            // Arrange
            var descricao = "Saco";
            _repository.Get(Arg.Any<Expression<Func<Embalagem, bool>>>()).Returns(Task.FromResult<Embalagem>(null));
            var createdEmbalagem = new Embalagem { Descricao = descricao };
            _repository.Create(Arg.Any<Embalagem>()).Returns(Task.FromResult(createdEmbalagem));

            // Act
            var result = await _embalagemService.GetFromDescricao(descricao);

            // Assert
            result.Should().BeEquivalentTo(createdEmbalagem, options => options.Excluding(e => e.Id));
            await _repository.Received(1).Get(Arg.Any<Expression<Func<Embalagem, bool>>>());
            await _repository.Received(1).Create(Arg.Is<Embalagem>(e => e.Descricao == descricao));
        }
    }
}
