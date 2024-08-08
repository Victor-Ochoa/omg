using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class Produto : Entity
{
    public string Descricao { get; set; } = string.Empty;
}
