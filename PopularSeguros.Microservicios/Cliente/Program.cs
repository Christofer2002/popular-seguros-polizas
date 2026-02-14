using Cliente.Data;
using Cliente.Helper;
using Cliente.Interfaces;
using Cliente.Servicios;
using Comun.Filters;
using Comun.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://devbychris.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<MiddlewareErroresGlobal>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Cliente API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("swagger"));
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ClienteDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Aplicando migraciones pendientes...");
        
        // Verificar si la base de datos existe, si no, crearla
        if (!context.Database.CanConnect())
        {
            context.Database.EnsureCreated();
        }
        
        context.Database.Migrate();

        if (app.Environment.IsDevelopment())
        {
            logger.LogInformation("Ejecutando seeding inicial...");
            await ClienteDbSeeder.SeedAsync(context, logger);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocurrió un error aplicando migraciones o seeding.");
        throw;
    }
}

app.Run();
