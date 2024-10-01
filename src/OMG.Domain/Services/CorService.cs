using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class CorService(ICorRepository repository) : ICorService
{
    private readonly ICorRepository _corRepository = repository;
    public async Task<Cor> GetFromName(string nome)
    {
        var cor = await _corRepository.GetFromNome(nome);

        if (cor == null) return await _corRepository.AddCor(nome);

        return cor;
    }
}
