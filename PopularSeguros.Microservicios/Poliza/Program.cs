using Comun.Filters;
using Comun.Middlewares;
using Microsoft.EntityFrameworkCore;
using Poliza.Data;
using Poliza.Helper;
using Poliza.Interfaces;
using Poliza.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPolizaService, PolizaService>();
builder.Services.AddScoped<ICatalogoService, CatalogoService>();

builder.Services.AddHttpClient<IPolizaService, PolizaService>();

builder.Services.AddDbContext<PolizaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CatalogoDbContext>(options =>
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
    var polizaContext = services.GetRequiredService<PolizaDbContext>();
    var catalogoContext = services.GetRequiredService<CatalogoDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Aplicando migraciones pendientes...");
        
        // Verificar conexión antes de migrar
        if (polizaContext.Database.CanConnect())
        {
            polizaContext.Database.Migrate();
        }
        
        if (catalogoContext.Database.CanConnect())
        {
            catalogoContext.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            logger.LogInformation("Ejecutando seeding inicial...");
            await PolizaDbSeeder.SeedAsync(polizaContext, logger);
            await CatalogoDbSeeder.SeedAsync(catalogoContext, logger);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocurrió un error aplicando migraciones o seeding.");
        throw;
    }
}

app.Run();
