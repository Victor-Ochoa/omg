using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class ProdutoService(IRepositoryEntity<Produto> repository) : IProdutoService
{
    private readonly IRepositoryEntity<Produto> _repository = repository;
    public async Task<Produto> GetFromDescricao(string descricao)
    {
        var produto = await _repository.Get(x => x.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase));

        if (produto == null) return await _repository.Create(new Produto { Descricao = descricao });

        return produto;
    }
}
