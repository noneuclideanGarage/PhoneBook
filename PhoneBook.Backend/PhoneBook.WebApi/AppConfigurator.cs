using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneBook.WebApi.Data;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Helpers.Middleware;
using PhoneBook.WebApi.Models;
using PhoneBook.WebApi.Repositories;
using PhoneBook.WebApi.Services;
using Serilog;
using Enc = System.Text.Encoding;

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

        builder.Services.AddScoped<IPhonebookRepository, PhonebookRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<SyncService>();

        builder.Services.AddDbContext<PhonebookDbContext>(opt =>
        {
            opt.UseNpgsql(builder.Configuration.GetConnectionString("db_conn"));
        });

        builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 8;
            })
            // .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<PhonebookDbContext>();

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultScheme =
                opt.DefaultAuthenticateScheme =
                    opt.DefaultChallengeScheme =
                        opt.DefaultSignOutScheme =
                            opt.DefaultForbidScheme =
                                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Enc.UTF8.GetBytes(
                    builder.Configuration["JWT:SigningKey"]
                    ?? throw new ArgumentException("Cannot get signing key"))
                )
            };
        });

        //Хранение токена для тестированя api
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
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
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<PhonebookDbContext>();
            db?.Database.EnsureCreated();
        }

        app.UseMiddleware<TaskCancelledExceptionCatchMiddleware>();
        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseCors(policyBuilder => policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            //.WithOrigins(https ip-address)
            .SetIsOriginAllowed(origin => true)
        );
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}