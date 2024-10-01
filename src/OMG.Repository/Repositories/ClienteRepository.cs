using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;

namespace OMG.Repository.Repositories;

internal class ClienteRepository(OMGDbContext dbContext) : IClienteRepository
{
    private readonly OMGDbContext dbContext = dbContext;

    public async Task<Cliente> Get(int id)
    => await dbContext.Clientes.FindAsync(id);
}
