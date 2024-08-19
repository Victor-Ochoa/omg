using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class PedidoItem : Entity
{
    public virtual int PedidoId { get; set; }
    public virtual required Pedido Pedido { get; set; }

    public virtual required Produto Produto { get; set; }
    public virtual required Formato Formato { get; set; }
    public virtual required Cor Cor { get; set; }
    public virtual required Aroma Aroma { get; set; }

    public virtual int Quantidade { get; set; }
}
