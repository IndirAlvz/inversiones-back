
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicio JWT
builder.Services.AddScoped<investment_service.Services.JwtService>();


// --- Servicios de Framework ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Servicios de Infraestructura ---
builder.Services.AddDbContext<investment_service.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Servicios de Aplicación ---
builder.Services.AddScoped<investment_service.Repositories.ISecUsuarioRepository, investment_service.Repositories.SecUsuarioRepository>();
builder.Services.AddScoped<investment_service.Services.SecUsuarioService>();


var app = builder.Build();

// Middleware de manejo de errores global
app.UseMiddleware<investment_service.Middleware.ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
