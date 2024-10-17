using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;
using OMG.Domain.Services;
using Xunit;


namespace OMG.Domain.Test.Services;

public class ClienteServiceTests
{
    private readonly IClienteService _clienteService;
    private readonly IRepositoryEntity<Cliente> _repository;

    public ClienteServiceTests()
    {
        _repository = Substitute.For<IRepositoryEntity<Cliente>>();
        _clienteService = new ClienteService(_repository);
    }

    [Fact]
    public async Task Get_ExistingCliente_ReturnsCliente()
    {
        // Arrange
        var id = 1;
        var expectedCliente = new Cliente { Id = id, Nome = "Cliente Teste" };
        _repository.Get(id).Returns(Task.FromResult(expectedCliente));

        // Act
        var result = await _clienteService.Get(id);

        // Assert
        result.Should().BeEquivalentTo(expectedCliente);
        await _repository.Received(1).Get(id);
    }

    [Fact]
    public async Task Get_NonExistingCliente_ReturnsNull()
    {
        // Arrange
        var id = 2;
        _repository.Get(id).Returns(Task.FromResult<Cliente>(null));

        // Act
        var result = await _clienteService.Get(id);

        // Assert
        result.Should().BeNull();
        await _repository.Received(1).Get(id);
    }
}
