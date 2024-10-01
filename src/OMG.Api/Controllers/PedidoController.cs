using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;
using OMG.Domain.Request;
using OMG.Repository;

namespace OMG.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoController(OMGDbContext context, IPedidoService pedidoService) : ControllerBase
{

    private readonly OMGDbContext _context = context;
    private readonly IPedidoService _pedidoService = pedidoService;

    [HttpPut("ChangeStatus")]
    public async Task<IActionResult> ChangeStatus([FromBody] PedidoChangeStatusRequest request)
    {
        if (!await PedidoExists(request.idPedido))
        {
            return NotFound();
        }
        await _pedidoService.ChangeStatus(request.idPedido, request.NewStatus);

        return NoContent();
    }


    // GET: api/Pedido/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> GetPedido(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido == null)
        {
            return NotFound();
        }

        return pedido;
    }

    [HttpPost]
    public async Task<IActionResult> NewPedido([FromBody] NewPedidoRequest newPedidoRequest)
    {
        var pedido = _pedidoService.CreateNewPedido(newPedidoRequest);

        return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
    }

    private async Task<bool> PedidoExists(int id)
    {
        return await _context.Pedidos.AnyAsync(e => e.Id == id);
    }
}
