using OMG.Domain.Entities;
using OMG.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.Domain.Mappers
{
    public static class PedidoItemMapper
    {
        public static PedidoItemModal ConvertToPedidoItemModal(this PedidoItem item) => new()
        {
            ItemId = item.Id,
            Cor = item.Cor.Nome,
            Formato = item.Formato.Descricao,
            Produto = item.Produto.Descricao,
            Quantidade = item.Quantidade
        };
    }
}
