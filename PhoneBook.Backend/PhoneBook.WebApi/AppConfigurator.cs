using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.Data;
using Serilog;

namespace PhoneBook.WebApi;

/// <summary>
/// Класс, который предлагает методы настройки Program.cs
/// </summary>
public static class AppConfigurator
{
    /// <summary>
    /// Метод расширения, который отвечает за настройку IServiceCollection у WebApplicationBuilder
    /// </summary>
    /// <param name="builder"></param>
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Host.UseSerilog((context, logConf) => logConf
            .ReadFrom.Configuration(builder.Configuration));

        builder.Services.AddDbContext<PhonebookDbContext>(opts =>
        {
            opts.UseNpgsql(builder.Configuration.GetConnectionString("pgConnect"));
        });
    }

    /// <summary>
    /// Метод расширения, который отвечает за настроку middleware-pipeline
    /// </summary>
    /// <param name="app"></param>
    public static void ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}