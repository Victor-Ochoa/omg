using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OMG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        // GET: api/<PedidoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PedidoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            //201
            return Created();
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //204
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //204
        }
    }
}
