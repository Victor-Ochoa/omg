using OMG.Domain.Entities;
using OMG.Domain.Enum;

namespace OMG.Domain.Contracts.Repository;

public interface IPedidoRepository
{
    Task ChangePedidoStatus(int id, EPedidoStatus newStatus);
    Task<EPedidoStatus> GetPedidoStatus(int id);
    Task Create(Pedido pedido);
}
