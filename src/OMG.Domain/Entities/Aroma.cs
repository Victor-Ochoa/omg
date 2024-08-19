using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class Aroma : Entity
{
    public virtual string Nome { get; set; } = string.Empty;
}
