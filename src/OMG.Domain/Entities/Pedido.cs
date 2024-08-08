using OMG.Domain.Base;
using OMG.Domain.Enum;

namespace OMG.Domain.Entities;

public class Pedido : Entity
{
    public required EPedidoStatus Status { get; set; } = EPedidoStatus.Novo;

    public required Cliente Cliente { get; set; }

    public required IList<PedidoItem> PedidoItens { get; set; }

    public float ValorTotal { get; set; } = 0f;
    public float Desconto { get; set; } = 0f;
    public float Entrada { get; set; } = 0f;
    public bool IsPermuta { get; set; } = false;
    public DateOnly DataEntrega { get; set; } = new DateOnly();
}
