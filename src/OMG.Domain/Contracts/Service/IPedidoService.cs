using OMG.Domain.Entities;
using OMG.Domain.Enum;
using OMG.Domain.Request;

namespace OMG.Domain.Contracts.Service;

public interface IPedidoService
{
    Task ChangeStatus(int idPedido, EPedidoStatus newStatus);
    Task<Pedido> CreateNewPedido(NewPedidoRequest newPedidoRequest);

}
