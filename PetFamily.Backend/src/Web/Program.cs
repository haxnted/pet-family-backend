using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Accounts.Presentation;
using PetFamily.Species.Infrastructure;
using PetFamily.Species.Presentation;
using PetFamily.VolunteerManagement.Infrastructure;
using PetFamily.VolunteerManagement.Presentation;
using Serilog;
using SwaggerThemes;
using Web;
using Web.Extensions;
using Web.Middlewares;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.AddConfigureLogging();

builder.Services
    .AddSpeciesModule(builder.Configuration)
    .AddVolunteerModule(builder.Configuration)
    .AddAccountsModule(builder.Configuration)
    .AddWeb();

var app = builder.Build();

app.UseExceptionMiddleware();
var accountSeeder = app.Services.GetRequiredService<AccountsSeeder>();
await accountSeeder.SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerThemes(Theme.NordDark);
    app.UseSwaggerUI();
    await app.ApplyMigrations<SpeciesWriteDbContext>();
    await app.ApplyMigrations<VolunteersWriteDbContext>();
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
