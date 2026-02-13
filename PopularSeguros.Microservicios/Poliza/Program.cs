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

app.UseMiddleware<MiddlewareErroresGlobal>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();

app.Run();
