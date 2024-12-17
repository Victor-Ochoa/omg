using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Repository;
using OMG.Domain.Entities;
using OMG.Repository.Repositories;

namespace OMG.Repository;

public static class RepositoryDI
{
    public static IHostApplicationBuilder AddOMGRepository(this IHostApplicationBuilder builder)
    {

        builder.AddSqlServerDbContext<OMGDbContext>("database", configureDbContextOptions: option =>
        {
            option.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            option.UseLazyLoadingProxies();
            option.UseSeeding((context, _) =>
            {
                var cliente = new Cliente { 
                    Nome = "Victor (Marido)",
                    Telefone = "53 98416-3953",
                    Endereco = "JK de Oliveira 2200 19B 201",
                    IsDeleted = false,
                    DeletedAt = null };
                if (!context.Set<Cliente>().Any())
                {
                    context.Set<Cliente>().Add(cliente);
                    context.SaveChanges();
                }
            });

        });


        builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
        builder.Services.AddTransient<IEventRepository, EventRepository>();

        builder.Services.AddTransient(typeof(IRepositoryEntity<>), typeof(EntityRepository<>));

        return builder;
    }
}
