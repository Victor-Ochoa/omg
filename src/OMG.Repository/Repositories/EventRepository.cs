using OMG.Domain.Contracts.Repository;
using OMG.Domain.Enum;
using OMG.Domain.Events;

namespace OMG.Repository.Repositories;

internal class EventRepository(OMGDbContext context) : IEventRepository
{
    private readonly OMGDbContext _context = context;

    public async Task EventChangeStatusPedido(int idPedido, EPedidoStatus oldStatus, EPedidoStatus newStatus)
    {
        await _context.EventChangeStatus.AddAsync(new EventChangeStatus
        {
            IdPedido = idPedido,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            DataCriacao = DateTime.Now
        });

        await _context.SaveChangesAsync();
    }
}
