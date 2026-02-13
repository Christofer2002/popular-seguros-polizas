using Comun.Filters;
using Comun.Middlewares;
using Microsoft.EntityFrameworkCore;
using Poliza.Data;
using Poliza.Interfaces;
using Poliza.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPolizaService, PolizaService>();

builder.Services.AddDbContext<PolizaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UsePathBase("/popular-seguros-polizas/api/clientes");

app.UseMiddleware<MiddlewareErroresGlobal>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Cliente API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("swagger"));
app.MapControllers();

app.Run();
