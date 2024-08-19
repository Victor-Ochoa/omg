using OMG.Domain.Enum;

namespace OMG.Domain.ViewModels;

public class PedidoCard
{
    public int PedidoId { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public int TotalItens { get; set; }
    public DateOnly DataEntrega { get; set; }= new DateOnly();
    public float ValorTotal { get; set; }
    public EPedidoStatus Status { get; set; }
}
