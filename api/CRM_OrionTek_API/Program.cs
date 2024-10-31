using CRM_OrionTek_API.Data;
using CRM_OrionTek_API.Services.ClientService;
using CRM_OrionTek_API.Services.LocationService;
using MeditodApi.Services.LocationService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("New Policy", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<IClient, ClientService>();
builder.Services.AddScoped<ILocation, LocationService>();

// Configure DbContext
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply CORS policy
app.UseAuthorization();
app.MapControllers();
app.UseCors("New Policy");
app.Run();
