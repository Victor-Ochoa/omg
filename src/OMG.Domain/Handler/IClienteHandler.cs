using OMG.Domain.Base;
using OMG.Domain.Entities;

namespace OMG.Domain.Handler;

public interface IClienteHandler
{
    Task<Response<Cliente>> CreateOrUpdate(Cliente cliente);
}
