using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;

namespace OMG.Repository.Mappings;

public class CorMap : IEntityTypeConfiguration<Cor>
{
    public void Configure(EntityTypeBuilder<Cor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome).HasMaxLength(250).IsRequired();

        builder.HasIndex(i => i.Nome);
    }
}
