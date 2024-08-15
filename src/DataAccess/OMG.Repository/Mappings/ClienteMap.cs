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
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Telefone).HasMaxLength(50);
            builder.Property(x => x.Endereco).HasMaxLength(300);

            builder.HasIndex(x => x.Nome);
        }
    }
}
