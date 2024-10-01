using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Service;

public interface IProdutoService
{
    Task<Produto> GetFromDescricao(string descricao);
}
