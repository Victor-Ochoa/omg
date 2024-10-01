using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class AromaService(IAromaRepository repository) : IAromaService
{
    private readonly IAromaRepository _repository = repository;
    public async Task<Aroma> GetFromName(string nome)
    {
        var aroma = await _repository.GetFromNome(nome);

        if (aroma == null) return await _repository.AddAroma(nome);

        return aroma;
    }
}
