using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;
using OMG.Domain.Mappers;
using OMG.Domain.ViewModels;
using OMG.Repository;

namespace OMG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly OMGDbContext _context;

        public ViewController(OMGDbContext context)
        {
            _context = context;
        }

        [HttpGet("Pedido/Card")]
        public async Task<ActionResult<IEnumerable<PedidoCard>>> GetPedidoCardList() =>
            await _context.Pedidos.Include(x => x.Cliente).Include(x => x.PedidoItens).Where(x => x.Status != Domain.Enum.EPedidoStatus.Entregue)
            .Union(_context.Pedidos.Include(x => x.Cliente).Include(x => x.PedidoItens).Where(x =>
                x.Status == Domain.Enum.EPedidoStatus.Entregue && x.DataEntrega <= DateOnly.FromDateTime(DateTime.Now).AddDays(-14)))
            .Select(x => x.ConvertToPedidoCard())
            .ToListAsync();



        [HttpGet("Pedido/Modal/{id}")]
        public async Task<ActionResult<PedidoModal>> GetPedidoModal(int id)
        {
            var pedido = await _context.Pedidos
                .AsTracking()
                .Where(x => x.Id == id)
                .FirstAsync();

            if (pedido == null)
                return NotFound();

            return Ok(pedido.ConvertToPedidoModal());
        }
    }
}
