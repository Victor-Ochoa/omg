using Microsoft.Extensions.DependencyInjection;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Services;

namespace OMG.Domain;

public static class DomainDI
{
    public static IServiceCollection AddOMGServices(this IServiceCollection services)
    {
        services.AddTransient<IPedidoService, PedidoService>();

        return services;
    }
}
