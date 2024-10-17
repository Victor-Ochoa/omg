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
    public class FormatoServiceTests
    {
        private readonly IFormatoService _formatoService;
        private readonly IRepositoryEntity<Formato> _repository;

        public FormatoServiceTests()
        {
            _repository = Substitute.For<IRepositoryEntity<Formato>>();
            _formatoService = new FormatoService(_repository);
        }

        [Fact]
        public async Task GetFromDescricao_ExistingFormato_ReturnsFormato()
        {
            // Arrange
            var descricao = "Cilíndrico";
            var expectedFormato = new Formato { Descricao = descricao };
            _repository.Get(Arg.Any<Expression<Func<Formato, bool>>>()).Returns(Task.FromResult(expectedFormato));

            // Act
            var result = await _formatoService.GetFromDescricao(descricao);

            // Assert
            result.Should().BeEquivalentTo(expectedFormato, options => options.Excluding(f => f.Id));
            await _repository.Received(1).Get(Arg.Any<Expression<Func<Formato, bool>>>());
            await _repository.DidNotReceive().Create(Arg.Any<Formato>());
        }

        [Fact]
        public async Task GetFromDescricao_NonExistingFormato_CreatesAndReturnsFormato()
        {
            // Arrange
            var descricao = "Esférico";
            _repository.Get(Arg.Any<Expression<Func<Formato, bool>>>()).Returns(Task.FromResult<Formato>(null));
            var createdFormato = new Formato { Descricao = descricao };
            _repository.Create(Arg.Any<Formato>()).Returns(Task.FromResult(createdFormato));

            // Act
            var result = await _formatoService.GetFromDescricao(descricao);

            // Assert
            result.Should().BeEquivalentTo(createdFormato, options => options.Excluding(f => f.Id));
            await _repository.Received(1).Get(Arg.Any<Expression<Func<Formato, bool>>>());
            await _repository.Received(1).Create(Arg.Is<Formato>(f => f.Descricao == descricao));
        }
    }
}
