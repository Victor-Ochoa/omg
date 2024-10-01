using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Repository;

public interface IAromaRepository
{
    Task<Aroma> AddAroma(string nome);
    Task<Aroma> GetFromNome(string nome);
}
