using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Repository;
using OMG.Repository.Repositories;

namespace OMG.Repository;

public static class RepositoryDI
{
    public static IServiceCollection AddOMGRepository(this IServiceCollection services, string connectionString)
    {

        services.AddDbContext<OMGDbContext>(option =>
        {
            option.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            option.UseLazyLoadingProxies();
            option.UseSqlServer(connectionString);
        });


        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<IAromaRepository, AromaRepository>();
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<ICorRepository, CorRepository>();
        services.AddTransient<IFormatoRepository, FormatoRepository>();
        services.AddTransient<IProdutoRepository, ProdutoRepository>();

        services.AddTransient(typeof(IRepositoryEntity<>), typeof(EntityRepository<>));

        return services;
    }
}
