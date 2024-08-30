using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Enum;

namespace OMG.Repository.Repositories;

public class PedidoRepository(OMGDbContext context) : IPedidoRepository
{
    private readonly OMGDbContext _context = context;

    public async Task ChangePedidoStatus(int id, EPedidoStatus newStatus)
    {
        var pedido = await _context.Pedidos.FindAsync(id) ?? throw new Exception($"Pedido ({id}) não encontrado");

        pedido.Status = newStatus;

        _context.Entry(pedido).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task<EPedidoStatus> GetPedidoStatus(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id) ?? throw new Exception($"Pedido ({id}) não encontrado");

        return pedido.Status;
    }
}
