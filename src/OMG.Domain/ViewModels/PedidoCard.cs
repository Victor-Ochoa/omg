namespace OMG.Domain.ViewModels
{
    public class PedidoCard
    {
        public int ProdutoId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public int TotalItens { get; set; }
        public DateOnly DataEntrega { get; set; }= new DateOnly();
        public float ValorTotal { get; set; }
    }
}
