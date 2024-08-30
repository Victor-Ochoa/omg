using OMG.Domain.Enum;

namespace OMG.Domain.Contracts.Service;

public interface IPedidoService
{
    Task ChangeStatus(int idPedido, EPedidoStatus newStatus);

}
