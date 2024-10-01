using OMG.Domain.Entities;

namespace OMG.Domain.Contracts.Service;

public interface ICorService
{
    Task<Cor> GetFromName(string nome);
}
