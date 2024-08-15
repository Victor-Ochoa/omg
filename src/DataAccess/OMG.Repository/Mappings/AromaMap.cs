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
    public class AromaMap : IEntityTypeConfiguration<Aroma>
    {
        public void Configure(EntityTypeBuilder<Aroma> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).HasMaxLength(250).IsRequired();

            builder.HasIndex(i => i.Nome);
        }
    }
}
