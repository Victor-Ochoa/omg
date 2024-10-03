using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;
using OMG.Domain.Enum;

namespace OMG.Repository.Repositories;

internal class PedidoRepository(OMGDbContext context) : IPedidoRepository
{
    private readonly OMGDbContext _context = context;

    public async Task ChangePedidoStatus(int id, EPedidoStatus newStatus)
    {
        var pedido = await _context.Pedidos.FindAsync(id) ?? throw new Exception($"Pedido ({id}) não encontrado");

        pedido.Status = newStatus;

        _context.Entry(pedido).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task Create(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);

        await _context.SaveChangesAsync();
    }

    public async Task<EPedidoStatus> GetPedidoStatus(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id) ?? throw new Exception($"Pedido ({id}) não encontrado");

        return pedido.Status;
    }


    public async Task<IList<Pedido>> GetPedidosViewHome(int diasExcluirProntos = 14) => await PedidoViewHomeQuery(diasExcluirProntos).ToListAsync();
    private IQueryable<Pedido> PedidoViewHomeQuery(int diasExcluirProntos = 14) 
        => _context.Pedidos.Where(x => x.Status != EPedidoStatus.Entregue)
                           .Union(_context.Pedidos.Where(x =>
                            x.Status == EPedidoStatus.Entregue && x.DataEntrega >= DateOnly.FromDateTime(DateTime.Now).AddDays(-diasExcluirProntos)));
}
