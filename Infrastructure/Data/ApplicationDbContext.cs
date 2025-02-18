using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Core.Entities;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public DbSet<Tipos_Solicitudes> Tipos_Solicitudes { get; set; }
    public DbSet<Numeros_Solicitudes> Numeros_Solicitudes { get; set; }
    public DbSet<Solicitudes> Solicitudes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Solicitudes>()
                .HasOne(s => s.Tipos_Solicitudes)  // Relación con Tipos_Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchas solicitudes
                .HasForeignKey(s => s.so_ts_id);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
