using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class Cliente : Entity
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
}
