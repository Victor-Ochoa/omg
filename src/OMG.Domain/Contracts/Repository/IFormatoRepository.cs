using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Repository;

public interface IFormatoRepository
{
    Task<Formato> AddFormato(string descricao);
    Task<Formato> GetFromDescricao(string descricao);
}
