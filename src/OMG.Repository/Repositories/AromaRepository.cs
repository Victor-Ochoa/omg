using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;

namespace OMG.Repository.Repositories;

internal class AromaRepository(OMGDbContext dbContext) : IAromaRepository
{
    private readonly OMGDbContext _dbContext = dbContext;

    public async Task<Aroma> AddAroma(string nome)
    {
        var aroma = new Aroma { Nome = nome };

        await _dbContext.Aromas.AddAsync(aroma);

        await _dbContext.SaveChangesAsync();

        return aroma;
    }

    public async Task<Aroma> GetFromNome(string nome)
    => await _dbContext.Aromas.Where(x => x.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();

}
