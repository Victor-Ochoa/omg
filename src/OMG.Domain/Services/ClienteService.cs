using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class ClienteService(IClienteRepository clienteRepository) : IClienteService
{
    private readonly IClienteRepository repository = clienteRepository;

    public async Task<Cliente> Get(int id)
    => await repository.Get(id);
}
