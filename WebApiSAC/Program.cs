using AutoMapper;
using Core.Contracts;
using Core.Interfaces;
using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using WebApiSAC.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
});

// Configura Kestrel para escuchar en el puerto 5000
builder.WebHost.ConfigureKestrel(options =>
{
    // Escucha en todas las interfaces en el puerto 5000
    options.ListenAnyIP(5000);
});

var corsPolicyName = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfiles());
});
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppConfig, AppConfig>();
builder.Services.AddScoped<INumeroSolicitudService, NumeroSolicitudService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();


var app = builder.Build();

// **Aplicar migraciones automáticamente al iniciar la aplicación**
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Aplica las migraciones pendientes si es necesario
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
