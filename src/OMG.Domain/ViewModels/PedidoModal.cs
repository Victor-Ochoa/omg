namespace OMG.Domain.ViewModels
{
    public class PedidoModal
    {
        public int PedidoId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public string ClienteTelefone { get; set; } = string.Empty;
        public string ClienteEndereco { get; set; } = string.Empty;
        public IEnumerable<PedidoItemModal> PedidoItens { get; set; } = Array.Empty<PedidoItemModal>();
        public float ValorTotal { get; set; }
        public float ValorPago { get; set; }
        public float ValorReceber { get; set; }
        public bool Permuta { get; set; } = false;
        public DateOnly DataEntrega { get; set; }
    }
}
