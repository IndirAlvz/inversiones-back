

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var key = jwtSettings["Key"];
    if (string.IsNullOrEmpty(key))
        throw new InvalidOperationException("JWT Key is not configured in appsettings.");
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key!))
    };
});

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
    builder.Services.AddScoped<investment_service.Repositories.IRefreshTokenRepository, investment_service.Repositories.RefreshTokenRepository>();
    builder.Services.AddScoped<investment_service.Services.RefreshTokenService>();


var app = builder.Build();

// Middleware de manejo de errores global
app.UseMiddleware<investment_service.Middleware.ErrorHandlingMiddleware>();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
