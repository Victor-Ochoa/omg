using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

internal class EmbalagemService(IRepositoryEntity<Embalagem> repository) : IEmbalagemService
{
    private readonly IRepositoryEntity<Embalagem> _repository = repository;
    public async Task<Embalagem> GetFromDescricao(string descricao)
    {
        var embalagem = await _repository.Get(x => x.Descricao.ToLower().Trim() == descricao.ToLower().Trim());

        if (embalagem == null) return await _repository.Create(new Embalagem { Descricao = descricao });

        return embalagem;
    }
}
