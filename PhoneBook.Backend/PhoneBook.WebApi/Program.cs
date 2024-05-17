using Microsoft.AspNetCore.Identity;
using PhoneBook.WebApi;
using PhoneBook.WebApi.Data;
using PhoneBook.WebApi.Helpers.Middleware;
using PhoneBook.WebApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
// app.ConfigureMiddleware(); //for an asynchronise code  

app.UseCors("CorsPolicy");

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<PhonebookDbContext>();
var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
await db.Database.EnsureCreatedAsync();
if (userManager != null) await DataSeeder.SeedAsync(userManager);
else Log.Information("Cannot get UserManager from scope");

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseMiddleware<TaskCancelledExceptionCatchMiddleware>();
// app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();