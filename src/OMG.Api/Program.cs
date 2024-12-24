
using Microsoft.EntityFrameworkCore;
using OMG.Domain;
using OMG.Repository;

namespace OMG.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddOMGServices();

        builder.AddOMGRepository();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
        });

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            await using(var serviceScope = app.Services.CreateAsyncScope())
            await using(var dbContext = serviceScope.ServiceProvider.GetRequiredService<OMGDbContext>())
            {
                await dbContext.Database.EnsureCreatedAsync();
            }
        }

        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
