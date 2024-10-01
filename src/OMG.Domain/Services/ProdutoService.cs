using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class ProdutoService(IProdutoRepository repository) : IProdutoService
{
    private readonly IProdutoRepository _repository = repository;
    public async Task<Produto> GetFromDescricao(string descricao)
    {
        var produto = await _repository.GetFromDescricao(descricao);

        if (produto == null) return await _repository.AddProduto(descricao);

        return produto;
    }
}
