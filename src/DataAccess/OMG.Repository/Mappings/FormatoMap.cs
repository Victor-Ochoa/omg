using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;

namespace OMG.Repository.Mappings;

public class FormatoMap : IEntityTypeConfiguration<Formato>
{
    public void Configure(EntityTypeBuilder<Formato> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao).HasMaxLength(250).IsRequired();

        builder.HasIndex(i => i.Descricao);
    }
}
