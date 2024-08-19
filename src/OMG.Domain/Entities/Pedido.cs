using OMG.Domain.Base;
using OMG.Domain.Enum;

namespace OMG.Domain.Entities;

public class Pedido : Entity
{
    public virtual required EPedidoStatus Status { get; set; } = EPedidoStatus.Novo;

    public virtual required Cliente Cliente { get; set; }

    public virtual required IList<PedidoItem> PedidoItens { get; set; }

    public virtual float ValorTotal { get; set; } = 0f;
    public virtual float Desconto { get; set; } = 0f;
    public virtual float Entrada { get; set; } = 0f;
    public virtual bool IsPermuta { get; set; } = false;
    public virtual DateOnly DataEntrega { get; set; } = new DateOnly();
}
