﻿using Microsoft.EntityFrameworkCore;
using OMG.Domain.Entities;

namespace OMG.Repository;

public class OMGDbContext : DbContext
{
    public OMGDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Aroma> Aromas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cor> Cores { get; set; }
    public DbSet<Formato> Formatos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OMGDbContext).Assembly);
    }

}