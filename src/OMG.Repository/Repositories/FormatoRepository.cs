using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;

namespace OMG.Repository.Repositories;

internal class FormatoRepository(OMGDbContext dbContext) : IFormatoRepository
{
    private readonly OMGDbContext _dbContext = dbContext;

    public async Task<Formato> AddFormato(string descricao)
    {
        var formato = new Formato { Descricao = descricao };

        await _dbContext.Formatos.AddAsync(formato);

        await _dbContext.SaveChangesAsync();

        return formato;

    }

    public async Task<Formato> GetFromDescricao(string descricao)
    => await _dbContext.Formatos.Where(x => x.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
}
