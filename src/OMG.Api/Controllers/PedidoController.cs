﻿using Microsoft.AspNetCore.Mvc;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;
using OMG.Domain.Request;

namespace OMG.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoController(IRepositoryEntity<Pedido> repository, IPedidoService pedidoService) : ControllerBase
{
    private readonly IRepositoryEntity<Pedido> _repository = repository;
    private readonly IPedidoService _pedidoService = pedidoService;

    [HttpPut("ChangeStatus")]
    public async Task<IActionResult> ChangeStatus([FromBody] PedidoChangeStatusRequest request)
    {
        if (!await _repository.Exist(request.idPedido))
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
        var pedido = await _repository.Get(id);

        if (pedido == null)
        {
            return NotFound();
        }

        return pedido;
    }

    [HttpPost]
    public async Task<IActionResult> NewPedido([FromBody] NewPedidoRequest newPedidoRequest)
    {
        var pedido = await _pedidoService.CreateNewPedido(newPedidoRequest);

        return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
    }
}
