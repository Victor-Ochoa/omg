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
        public async Task<IActionResult> GetPedidoCardList() =>
           Ok((await _context.Pedidos.Where(x => x.Status != Domain.Enum.EPedidoStatus.Entregue)
            .Union(_context.Pedidos.Where(x =>
                x.Status == Domain.Enum.EPedidoStatus.Entregue && x.DataEntrega <= DateOnly.FromDateTime(DateTime.Now).AddDays(-14)))
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
}
