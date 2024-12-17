using MudBlazor.Services;
using OMG.Domain;
using OMG.Domain.Handler;
using OMG.WebApp.Components;
using OMG.WebApp.Handler;
using System.Globalization;

namespace OMG.WebApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add MudBlazor services
        builder.Services.AddMudServices();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddTransient<IPedidoHandler, PedidoHandler>();
        builder.Services.AddTransient<IClienteHandler, ClienteHandler>();
        builder.Services.AddTransient(typeof(IBaseSearchHandler<>), typeof(BaseSearchHandler<>));

        builder.Services.AddHttpClient(Configuracao.HttpClientNameOMGApi, opt =>
        {
            opt.BaseAddress = new Uri("https://omg-api");
        }); 

        builder.Services.AddLocalization();
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
