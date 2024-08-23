using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using OMG.Domain;
using OMG.Domain.Handler;
using OMG.WebApp.Client.Handler;
using OMG.WebApp.Client.Pages;
using OMG.WebApp.Components;
using System.Globalization;

namespace OMG.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            Configuracao.BackendUrl = builder.Configuration.GetSection("Configuracao").GetValue<string>("BackendUrl") ?? string.Empty;

            builder.Services.AddTransient<IPedidoHandler, PedidoHandler>();

            builder.Services.AddHttpClient(Configuracao.HttpClientNameOMGApi, opt =>
            {
                opt.BaseAddress = new Uri(Configuracao.BackendUrl);
            });

            builder.Services.AddLocalization();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
