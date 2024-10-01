using OMG.Domain.Contracts.Repository;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class FormatoService(IFormatoRepository repository) : IFormatoService
{
    private readonly IFormatoRepository _repository = repository;
    public async Task<Formato> GetFromDescricao(string descricao)
    {
        var formato = await _repository.GetFromDescricao(descricao);

        if (formato == null) return await _repository.AddFormato(descricao);

        return formato;
    }
}
