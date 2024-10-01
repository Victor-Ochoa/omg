using OMG.Domain.Base;
using OMG.Domain.Enum;

namespace OMG.Domain.Entities;

public class Pedido : Entity
{
    public virtual required EPedidoStatus Status { get; set; } = EPedidoStatus.Novo;

    public virtual required Cliente Cliente { get; set; }

    public virtual required IList<PedidoItem> PedidoItens { get; set; }

    public virtual decimal ValorTotal { get; set; } = 0m;
    public virtual decimal Desconto { get; set; } = 0m;
    public virtual decimal Entrada { get; set; } = 0m;
    public virtual bool IsPermuta { get; set; } = false;
    public virtual DateOnly DataEntrega { get; set; } = new DateOnly();
}
