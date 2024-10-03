using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class FormatoService(IRepositoryEntity<Formato> repository) : IFormatoService
{
    private readonly IRepositoryEntity<Formato> _repository = repository;
    public async Task<Formato> GetFromDescricao(string descricao)
    {
        var formato = await _repository.Get(x => x.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase));

        if (formato == null) return await _repository.Create(new Formato{ Descricao = descricao });

        return formato;
    }
}
