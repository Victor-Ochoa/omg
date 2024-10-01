using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Service;

public interface IFormatoService
{
    Task<Formato> GetFromDescricao(string descricao);
}
