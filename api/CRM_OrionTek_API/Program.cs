using CRM_OrionTek.Infrastructure.Data; // Updated from CRM_OrionTek_API.Data
using CRM_OrionTek.Domain.Entities; // Assuming you have models in Domain now

// Services
using CRM_OrionTek.Application.Services.PermissionService;
using CRM_OrionTek.Application.Services.UserService;
using CRM_OrionTek.Application.Services.ClientService;
using CRM_OrionTek.Application.Services.LocationService;
using CRM_OrionTek.Application.Services.UnitService;
//Interfaces
using CRM_OrionTek.Domain.Interfaces.IClientRepository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using CRM_OrionTek.Application.Services.ClientService;
using CRM_OrionTek.Application.Services.LocationService;
using CRM_OrionTek.Application.Services.UnitService;
using System.Net;
using System.Security;
using CRM_OrionTek.Domain.Interfaces;
using CRM_OrionTek.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); // Testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CRM ORIONTEK API", Version = "v1" });

    // Define the OAuth2.0 scheme that's in use (i.e., Implicit Flow)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Register application services
builder.Services.AddScoped<IAuthRepository, AuthService>();
builder.Services.AddScoped<IPermissionRepository, PermissionService>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IUnitRepository, UnitService>();
builder.Services.AddScoped<ILocationRepository, LocationService>();
builder.Services.AddScoped<IClientRepository, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Configure DbContext
builder.Services.AddDbContext<CRM_OrionTekDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
});

var app = builder.Build();

// Swagger setup for both Development and Production environments
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAny");

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
