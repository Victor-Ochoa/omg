using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Enum;

namespace OMG.Domain.Services;

public class PedidoService(IPedidoRepository pedidoRepository, IEventRepository eventRepository) : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task ChangeStatus(int idPedido, EPedidoStatus newStatus)
    {
        var oldstatus = await _pedidoRepository.GetPedidoStatus(idPedido);

        await _pedidoRepository.ChangePedidoStatus(idPedido, newStatus);

        await _eventRepository.EventChangeStatusPedido(idPedido, oldstatus, newStatus);
    }
}
