﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMG.Domain.Entities;

namespace OMG.Repository.Mappings;

public class PedidoMap : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.PedidoItens).WithOne(x => x.Pedido);
        builder.Property(x => x.Status).IsRequired();
        builder.HasOne(x => x.Cliente);
        builder.Property(x => x.DataEntrega).IsRequired();
        builder.Property(x => x.Entrada).HasDefaultValue(0.0f);
        builder.Property(x => x.ValorTotal).HasDefaultValue(0.0f).IsRequired();
        builder.Property(x => x.Desconto).HasDefaultValue(0.0f);
        builder.Property(x => x.IsPermuta).HasDefaultValue(false);

    }
}