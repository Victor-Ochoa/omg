using OMG.Domain.Base;
using OMG.Domain.Request;
using OMG.Domain.ViewModels;

namespace OMG.Domain.Handler;

public interface IPedidoHandler
{
    Task<Response<IEnumerable<PedidoCard>>> GetPedidoCardList();
    Task<Response<PedidoModal>> GetPedidoModal(int Id);
    Task<Response> ChangeStatus(PedidoChangeStatusRequest request);
}
