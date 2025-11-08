using Microsoft.EntityFrameworkCore;
using MiniShop.Infrastructure.Persistence;
using System;

var builder = WebApplication.CreateBuilder(args);

// EF Core SQLite (DbContext en Infrastructure)
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var cs = builder.Configuration.GetConnectionString("Default");
    opt.UseSqlite(cs);
});

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servir archivos estáticos (wwwroot/images)
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// Migrar/crear DB al iniciar (útil en pruebas locales)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar static files para images
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

// Endpoint de salud mínimo
app.MapGet("/health", () => Results.Ok(new { ok = true, utc = DateTime.UtcNow }));

app.Run();
