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

namespace OMG.Domain.Test.Services;

public class AromaServiceTests
{
    private readonly IAromaService _aromaService;
    private readonly IRepositoryEntity<Aroma> _repository;

    public AromaServiceTests()
    {
        _repository = Substitute.For<IRepositoryEntity<Aroma>>();
        _aromaService = new AromaService(_repository);
    }

    [Fact]
    public async Task GetFromName_ExistingAroma_ReturnsAroma()
    {
        // Arrange
        var nome = "Lavanda";
        var aroma = new Aroma { Nome = nome };
        _repository.Get(Arg.Any<Expression<Func<Aroma, bool>>>()).Returns(Task.FromResult(aroma));

        // Act
        var result = await _aromaService.GetFromName(nome);

        // Assert
        result.Should().BeEquivalentTo(aroma, options => options.Excluding(a => a.Id));
        await _repository.Received(1).Get(Arg.Any<Expression<Func<Aroma, bool>>>());
        await _repository.DidNotReceive().Create(Arg.Any<Aroma>());
    }

    [Fact]
    public async Task GetFromName_NonExistingAroma_CreatesAndReturnsAroma()
    {
        // Arrange
        var nome = "Baunilha";
        _repository.Get(Arg.Any<Expression<Func<Aroma, bool>>>()).Returns(Task.FromResult<Aroma>(null));
        var createdAroma = new Aroma { Nome = nome };
        _repository.Create(Arg.Any<Aroma>()).Returns(Task.FromResult(createdAroma));

        // Act
        var result = await _aromaService.GetFromName(nome);

        // Assert
        result.Should().BeEquivalentTo(createdAroma, options => options.Excluding(a => a.Id));
        await _repository.Received(1).Get(Arg.Any<Expression<Func<Aroma, bool>>>());
        await _repository.Received(1).Create(Arg.Is<Aroma>(a => a.Nome == nome));
    }
}