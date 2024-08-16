using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;

namespace OMG.Repository.Mappings;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao).HasMaxLength(250).IsRequired();

        builder.HasIndex(i => i.Descricao);
    }
}
