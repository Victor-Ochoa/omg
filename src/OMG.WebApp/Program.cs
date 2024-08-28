using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using OMG.Domain.Handler;
using OMG.Domain;
using OMG.WebApp.Handler;
using System.Globalization;

namespace OMG.WebApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddMudServices();

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        Configuracao.BackendUrl = builder.Configuration.GetSection("Configuracao").GetValue<string>("BackendUrl") ?? string.Empty;

        builder.Services.AddTransient<IPedidoHandler, PedidoHandler>();

        builder.Services.AddHttpClient(Configuracao.HttpClientNameOMGApi, opt =>
        {
            opt.BaseAddress = new Uri(Configuracao.BackendUrl);
        });

        builder.Services.AddLocalization();
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

        await builder.Build().RunAsync();
    }
}
