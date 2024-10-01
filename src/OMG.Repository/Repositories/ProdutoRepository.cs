using Microsoft.EntityFrameworkCore;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;

namespace OMG.Repository.Repositories;

internal class ProdutoRepository(OMGDbContext dbContext) : IProdutoRepository
{
    private readonly OMGDbContext _dbContext = dbContext;

    public async Task<Produto> AddProduto(string descricao)
    {
        var produto = new Produto { Descricao = descricao };

        await _dbContext.Produtos.AddAsync(produto);

        await _dbContext.SaveChangesAsync();

        return produto;
    }

    public async Task<Produto> GetFromDescricao(string descricao)
    => await _dbContext.Produtos.Where(x => x.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
}
