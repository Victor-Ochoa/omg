using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Service;
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


    private async Task<bool> PedidoExists(int id)
    {
        return await _context.Produtos.AnyAsync(e => e.Id == id);
    }
}
