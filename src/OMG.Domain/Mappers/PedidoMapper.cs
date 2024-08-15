using OMG.Domain.Entities;
using OMG.Domain.ViewModels;

namespace OMG.Domain.Mappers
{
    public static class PedidoMapper
    {
        public static PedidoModal ConvertToPedidoModal(this Pedido pedido) => new()
        {
            ClienteEndereco = pedido.Cliente.Endereco,
            ClienteNome = pedido.Cliente.Nome,
            ClienteTelefone = pedido.Cliente.Telefone,
            PedidoId = pedido.Id,
            Permuta = pedido.IsPermuta,
            ValorPago = pedido.Entrada,
            ValorReceber = pedido.ValorTotal - pedido.Entrada,
            ValorTotal = pedido.ValorTotal,
            PedidoItens = pedido.PedidoItens.Select(x => x.ConvertToPedidoItemModal()),
            DataEntrega = pedido.DataEntrega
        };

        public static PedidoCard ConvertToPedidoCard(this Pedido pedido) => new()
        {
            PedidoId = pedido.Id,
            NomeCliente = pedido.Cliente.Nome,
            TotalItens = pedido.PedidoItens.Count,
            ValorTotal = pedido.ValorTotal,
            DataEntrega = pedido.DataEntrega

        };
    }
}
