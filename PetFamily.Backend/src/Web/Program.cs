using PetFamily.Accounts.Infrastructure.Seeding;
using Serilog;
using SwaggerThemes;
using Web;
using Web.Extensions;
using Web.Middlewares;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.AddConfigureLogging();

builder.Services.AddProgramDependency(builder.Configuration);

var app = builder.Build();
app.Services.RunMigrations();

var accountSeeder = app.Services.GetRequiredService<AccountsSeeder>();
await accountSeeder.SeedAsync();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerThemes(Theme.NordDark);
    app.UseSwaggerUI();
}

app.UseCors(config =>
{
    config.WithOrigins("http://localhost:5173")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpLogging();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
