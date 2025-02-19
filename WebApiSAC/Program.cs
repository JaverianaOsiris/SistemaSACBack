using AutoMapper;
using Core.Contracts;
using Core.Interfaces;
using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using WebApiSAC.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
                                                opciones.UseSqlServer("name=DefaultConnection"));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
