using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class Produto : Entity
{
    public virtual string Descricao { get; set; } = string.Empty;
}
