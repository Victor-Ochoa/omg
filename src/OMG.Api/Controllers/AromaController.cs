using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;
using OMG.Repository;

namespace OMG.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AromaController(OMGDbContext context) : ControllerBase
{
    private readonly OMGDbContext _context = context;

    // GET: api/Aroma
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aroma>>> GetAromas()
    {
        return await _context.Aromas.ToListAsync();
    }

    // GET: api/Aroma/search/aaa
    [HttpGet("search/{key}")]
    public async Task<IActionResult> GetSearchAromas(string key)
    {
        return Ok(await _context.Aromas.Where(x => x.Nome.Contains(key)).ToListAsync());
    }

    // GET: api/Aroma/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Aroma>> GetAroma(int id)
    {
        var aroma = await _context.Aromas.FindAsync(id);

        if (aroma == null)
        {
            return NotFound();
        }

        return aroma;
    }

    // PUT: api/Aroma/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAroma(int id, Aroma aroma)
    {
        if (id != aroma.Id)
        {
            return BadRequest();
        }

        _context.Entry(aroma).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AromaExists(id))
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

    // POST: api/Aroma
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Aroma>> PostAroma(Aroma aroma)
    {
        _context.Aromas.Add(aroma);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAroma", new { id = aroma.Id }, aroma);
    }

    // DELETE: api/Aroma/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAroma(int id)
    {
        var aroma = await _context.Aromas.FindAsync(id);
        if (aroma == null)
        {
            return NotFound();
        }

        _context.Aromas.Remove(aroma);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AromaExists(int id)
    {
        return _context.Aromas.Any(e => e.Id == id);
    }
}
