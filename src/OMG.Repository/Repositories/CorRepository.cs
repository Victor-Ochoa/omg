using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;

namespace OMG.Repository.Repositories;

internal class CorRepository(OMGDbContext dbContext) : ICorRepository
{
    private readonly OMGDbContext _dbContext = dbContext;

    public async Task<Cor> AddCor(string nome)
    {
        var cor = new Cor { Nome = nome };

        await _dbContext.Cores.AddAsync(cor);

        await _dbContext.SaveChangesAsync();

        return cor;
    }

    public async Task<Cor> GetFromNome(string nome)
    => await _dbContext.Cores.Where(x => x.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
}
