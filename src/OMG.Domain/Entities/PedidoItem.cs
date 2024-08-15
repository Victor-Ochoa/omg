using OMG.Domain.Base;

namespace OMG.Domain.Entities;

public class PedidoItem : Entity
{
    public int PedidoId { get; set; }
    public required Pedido Pedido { get; set; }

    public required Produto Produto { get; set; }
    public required Formato Formato { get; set; }
    public required Cor Cor { get; set; }
    public required Aroma Aroma { get; set; }

    public int Quantidade { get; set; }
}
