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
    public DbSet<Tipos_Usuarios> Tipos_Usuarios { get; set; }
    public DbSet<Tipos_Colaboradores> Tipos_Colaboradores { get; set; }
    public DbSet<Colaboradores> Colaboradores { get; set; }
    public DbSet<Cantidad_Solicitudes> Cantidad_Solicitudes { get; set; }
    public DbSet<Historicos_Solicitudes> Historicos_Solicitudes { get; set; }
    public DbSet<Login> Login { get; set; }

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
        builder.Entity<Solicitudes>()
                .HasOne(s => s.Usuarios)  // Relación con Usuarios
                .WithMany()  // Un tipo de soliciutd puede tener muchos usaurios
                .HasForeignKey(s => s.so_us_id);
        builder.Entity<Solicitudes>()
                .HasOne(s => s.Colaboradores)
                .WithMany()
                .HasForeignKey(s => s.so_col_id)
                .IsRequired(false);
        builder.Entity<Solicitudes>()
                .HasOne(s => s.Colaboradores)
                .WithMany()
                .HasForeignKey(s => s.so_col_id_colaborador_modificacion)
                .IsRequired(false);
        builder.Entity<Solicitudes>()
                .HasOne(s => s.SolicitudApelacion)
                .WithMany()
                .HasForeignKey(s => s.so_so_id)
                .IsRequired(false);

        builder.Entity<Usuarios>()
                .HasOne(s => s.Tipo_Identificacion)  
                .WithMany()  
                .HasForeignKey(s => s.us_ti_id);
        builder.Entity<Usuarios>()
                .HasOne(s => s.Tipos_Usuarios)
                .WithMany()
                .HasForeignKey(s => s.us_tu_id);

        builder.Entity<Colaboradores>()
                .HasOne(s => s.Tipos_Colaboradores)  // Relación con Estados Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchos estados
                .HasForeignKey(s => s.col_tc_id);
        builder.Entity<Colaboradores>()
                .HasOne(s => s.Tipos_Usuarios)  // Relación con Estados Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchos estados
                .HasForeignKey(s => s.col_tu_id);
        builder.Entity<Colaboradores>()
                .HasOne(s => s.Colaborador_Lider)  // Relación con Estados Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchos estados
                .HasForeignKey(s => s.col_col_id_lider)
                .IsRequired(false);

        builder.Entity<Cantidad_Solicitudes>()
                .HasOne(s => s.Colaboradores)  // Relación con Estados Solicitudes
                .WithMany()  // Un tipo de solicitud puede tener muchos estados
                .HasForeignKey(s => s.cs_col_id);
        builder.Entity<Cantidad_Solicitudes>()
                .HasOne(s => s.Tipos_Solicitudes)  
                .WithMany() 
                .HasForeignKey(s => s.cs_ts_id);
        builder.Entity<Cantidad_Solicitudes>()
                .HasOne(s => s.Estados_Solicitudes)
                .WithMany()
                .HasForeignKey(s => s.cs_es_id);

        builder.Entity<Historicos_Solicitudes>()
                .HasOne(s => s.Solicitudes)
                .WithMany()
                .HasForeignKey(s => s.hs_so_id);
        builder.Entity<Historicos_Solicitudes>()
                .HasOne(s => s.Estados_Solicitudes)
                .WithMany()
                .HasForeignKey(s => s.hs_es_id);
        builder.Entity<Historicos_Solicitudes>()
                .HasOne(s => s.Colaboradores)
                .WithMany()
                .HasForeignKey(s => s.hs_col_id);

        builder.Entity<Login>()
                .HasOne(s => s.Colaboradores)
                .WithMany()
                .HasForeignKey(s => s.lo_co_id);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
