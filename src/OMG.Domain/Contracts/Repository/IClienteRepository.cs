using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Repository;

public interface IClienteRepository
{
    Task<Cliente> Get(int id);
}
