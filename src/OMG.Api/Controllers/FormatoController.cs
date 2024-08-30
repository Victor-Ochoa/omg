using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;
using OMG.Repository;

namespace OMG.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatoController(OMGDbContext context) : ControllerBase
{
    private readonly OMGDbContext _context = context;

    // GET: api/Formato
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Formato>>> GetFormatos()
    {
        return await _context.Formatos.ToListAsync();
    }

    // GET: api/Formato/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Formato>> GetFormato(int id)
    {
        var formato = await _context.Formatos.FindAsync(id);

        if (formato == null)
        {
            return NotFound();
        }

        return formato;
    }

    // PUT: api/Formato/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormato(int id, Formato formato)
    {
        if (id != formato.Id)
        {
            return BadRequest();
        }

        _context.Entry(formato).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FormatoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Formato
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Formato>> PostFormato(Formato formato)
    {
        _context.Formatos.Add(formato);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFormato", new { id = formato.Id }, formato);
    }

    // DELETE: api/Formato/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormato(int id)
    {
        var formato = await _context.Formatos.FindAsync(id);
        if (formato == null)
        {
            return NotFound();
        }

        _context.Formatos.Remove(formato);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FormatoExists(int id)
    {
        return _context.Formatos.Any(e => e.Id == id);
    }
}
