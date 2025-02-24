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
        });

        builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
        builder.Services.AddTransient<IEventRepository, EventRepository>();

        builder.Services.AddTransient(typeof(IRepositoryEntity<>), typeof(EntityRepository<>));

        return builder;
    }
}