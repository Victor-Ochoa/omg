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

        builder.AddMySqlDbContext<OMGDbContext>("database", configureDbContextOptions: option =>
        {
            option.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            option.UseLazyLoadingProxies();
            option.UseSeeding((context, _) =>
            {
                var options = new DbContextOptionsBuilder<OMGDbContext>()
                    .UseMySql(context.Database.GetConnectionString(),new MySqlServerVersion("9.1.0"), sqlOptions => sqlOptions.EnableRetryOnFailure(0))
                    .Options;

                using var noRetryContext = new OMGDbContext(options);

                if (!noRetryContext.Set<Cliente>().Any())
                {
                    var cliente = new Cliente
                    {
                        Nome = "Victor (Marido)",
                        Telefone = "53 98416-3953",
                        Endereco = "JK de Oliveira 2200 19B 201",
                        IsDeleted = false,
                        DeletedAt = null
                    };

                    noRetryContext.Set<Cliente>().Add(cliente);
                    noRetryContext.SaveChanges();
                }
            });

            option.UseAsyncSeeding(async (context, _, ct) =>
            {
                var options = new DbContextOptionsBuilder<OMGDbContext>()
                    .UseMySql(context.Database.GetConnectionString(), new MySqlServerVersion("9.1.0"), sqlOptions => sqlOptions.EnableRetryOnFailure(0))
                    .Options;

                using var noRetryContext = new OMGDbContext(options);

                if (!await noRetryContext.Set<Cliente>().AnyAsync(ct))
                {
                    var cliente = new Cliente
                    {
                        Nome = "Victor (Marido)",
                        Telefone = "53 98416-3953",
                        Endereco = "JK de Oliveira 2200 19B 201",
                        IsDeleted = false,
                        DeletedAt = null
                    };

                    await noRetryContext.Set<Cliente>().AddAsync(cliente, ct);
                    await noRetryContext.SaveChangesAsync(ct);
                }

            });
        });

        builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
        builder.Services.AddTransient<IEventRepository, EventRepository>();

        builder.Services.AddTransient(typeof(IRepositoryEntity<>), typeof(EntityRepository<>));

        return builder;
    }
}