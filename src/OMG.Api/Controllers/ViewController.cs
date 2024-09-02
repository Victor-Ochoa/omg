using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Mappers;
using OMG.Repository;

namespace OMG.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ViewController(OMGDbContext context) : ControllerBase
{
    private readonly OMGDbContext _context = context;

    [HttpGet("Pedido/Card")]
    public async Task<IActionResult> GetPedidoCardList() =>
       Ok((await _context.Pedidos.Where(x => x.Status != Domain.Enum.EPedidoStatus.Entregue)
        .Union(_context.Pedidos.Where(x =>
            x.Status == Domain.Enum.EPedidoStatus.Entregue && x.DataEntrega >= DateOnly.FromDateTime(DateTime.Now).AddDays(-14)))
        .ToListAsync())
        .Select(x => x.ConvertToPedidoCard()));



    [HttpGet("Pedido/Modal/{id}")]
    public async Task<IActionResult> GetPedidoModal(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido == null)
            return NotFound();

        return Ok(pedido.ConvertToPedidoModal());
    }
}
