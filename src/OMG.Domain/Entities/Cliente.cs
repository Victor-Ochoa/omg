using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class Cliente : Entity
{
    public virtual string Nome { get; set; } = string.Empty;
    public virtual string Telefone { get; set; } = string.Empty;
    public virtual string Endereco { get; set; } = string.Empty;
}
