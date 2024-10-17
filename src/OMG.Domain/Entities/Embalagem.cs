using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class Embalagem : Entity
{
    public virtual string Descricao { get; set; } = string.Empty;
}