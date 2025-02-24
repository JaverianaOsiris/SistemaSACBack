using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
    public DbSet<Estados_Solicitudes> Estados_Solicitudes { get; set; }
    public DbSet<Tipo_Identificacion> Tipo_Identificacions { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Solicitudes>()
                .HasOne(s => s.Tipos_Solicitudes)  // Relación con Tipos_Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchas solicitudes
                .HasForeignKey(s => s.so_ts_id);

        builder.Entity<Solicitudes>()
                .HasOne(s => s.Estados_Solicitudes)  // Relación con Estados Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchos estados
                .HasForeignKey(s => s.so_es_id);

        builder.Entity<Usuarios>()
                .HasOne(s => s.Tipo_Identificacion)  // Relación con Estados Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchos estados
                .HasForeignKey(s => s.us_ti_id);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
