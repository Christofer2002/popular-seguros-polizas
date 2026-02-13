using Microsoft.EntityFrameworkCore;
using Cliente.Data;
using Comun.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<Cliente.Interfaces.IClienteService, Cliente.Servicios.ClienteService>();

builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<MiddlewareErroresGlobal>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new Comun.Filters.ValidateModelAttribute());
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();
