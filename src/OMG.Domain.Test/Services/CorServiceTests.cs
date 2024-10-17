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
    public class CorServiceTests
    {
        private readonly ICorService _corService;
        private readonly IRepositoryEntity<Cor> _corRepository;

        public CorServiceTests()
        {
            _corRepository = Substitute.For<IRepositoryEntity<Cor>>();
            _corService = new CorService(_corRepository);
        }

        [Fact]
        public async Task GetFromName_ExistingCor_ReturnsCor()
        {
            // Arrange
            var nome = "Azul";
            var expectedCor = new Cor { Nome = nome };
            _corRepository.Get(Arg.Any<Expression<Func<Cor, bool>>>()).Returns(Task.FromResult(expectedCor));

            // Act
            var result = await _corService.GetFromName(nome);

            // Assert
            result.Should().BeEquivalentTo(expectedCor, options => options.Excluding(c => c.Id));
            await _corRepository.Received(1).Get(Arg.Any<Expression<Func<Cor, bool>>>());
            await _corRepository.DidNotReceive().Create(Arg.Any<Cor>());
        }

        [Fact]
        public async Task GetFromName_NonExistingCor_CreatesAndReturnsCor()
        {
            // Arrange
            var nome = "Vermelho";
            _corRepository.Get(Arg.Any<Expression<Func<Cor, bool>>>()).Returns(Task.FromResult<Cor>(null));
            var createdCor = new Cor { Nome = nome };
            _corRepository.Create(Arg.Any<Cor>()).Returns(Task.FromResult(createdCor));

            // Act
            var result = await _corService.GetFromName(nome);

            // Assert
            result.Should().BeEquivalentTo(createdCor, options => options.Excluding(c => c.Id));
            await _corRepository.Received(1).Get(Arg.Any<Expression<Func<Cor, bool>>>());
            await _corRepository.Received(1).Create(Arg.Is<Cor>(c => c.Nome == nome));
        }
    }
}
