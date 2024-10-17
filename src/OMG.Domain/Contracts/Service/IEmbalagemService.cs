using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Service;

public interface IEmbalagemService
{
    Task<Embalagem> GetFromDescricao(string descricao);
}
