using Amazon;
using Amazon.S3;
using AutoMapper;
using Core.Contracts;
using Core.Interfaces;
using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

//// Configura Kestrel para escuchar en el puerto 5000
//builder.WebHost.ConfigureKestrel(options =>
//{
//    // Escucha en todas las interfaces en el puerto 5000
//    options.ListenAnyIP(5000);
//});

var corsPolicyName = "AllowAll";
var corsPolicyNameEsp = "AllowSecureOriginsWithCredentials";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });

    options.AddPolicy(corsPolicyNameEsp, policy =>
    {
        policy.WithOrigins("https://main.d3qlabcxqrorrx.amplifyapp.com/")  // Especifica los or�genes HTTPS permitidos
              .AllowAnyMethod()  // Permite cualquier m�todo HTTP
              .AllowAnyHeader()  // Permite cualquier cabecera
              .AllowCredentials();  // Permite el env�o de credenciales (cookies, tokens, etc.)
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
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICantidadSolicitudService, CantidadSolicitudService>();
builder.Services.AddScoped<IColaboradorService, ColaboradorService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IHistoricoSolicitudService, HistoricoSolicitudService>();

builder.Services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(
    builder.Configuration["AWSS3BUCKET:AccessKey"],
    builder.Configuration["AWSS3BUCKET:SecretKey"],
    RegionEndpoint.GetBySystemName(builder.Configuration["AWSS3BUCKET:Region"])
));

var app = builder.Build();

// **Aplicar migraciones autom�ticamente al iniciar la aplicaci�n**
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
app.UseCors(corsPolicyNameEsp);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
