using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Repository;

public interface ICorRepository
{
    Task<Cor> AddCor(string nome);
    Task<Cor> GetFromNome(string nome);
}
