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

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerThemes(Theme.NordDark);
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
