using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Repository;

public interface IProdutoRepository
{
    Task<Produto> AddProduto(string descricao);
    Task<Produto> GetFromDescricao(string descricao);
}
