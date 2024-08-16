using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.Repository.Mappings
{
    public class PedidoItemMap : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Pedido).WithMany(x => x.PedidoItens).HasForeignKey(x => x.PedidoId).IsRequired();
            builder.HasOne(x => x.Cor);
            builder.HasOne(x => x.Formato);
            builder.HasOne(x => x.Produto);
            builder.HasOne(x => x.Aroma);

            builder.Property(x => x.Quantidade);

        }
    }
}
