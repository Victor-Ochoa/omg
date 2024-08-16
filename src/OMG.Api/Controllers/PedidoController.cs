using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OMG.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    // GET: api/<PedidoController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new string[] { "value1", "value2" });
    }

    // GET api/<PedidoController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok("value");
    }

    // POST api/<PedidoController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        var id = 1;
        return Created($"/api/Pedido/{id}", "value");
    }

    // PUT api/<PedidoController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] string value)
    {
        return NoContent();
    }

    // DELETE api/<PedidoController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}
