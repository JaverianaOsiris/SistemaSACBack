using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Solicitudes
{
    [Key]
    public int so_id { get; set; }
    public string so_numero_solicitud { get; set; }
    public int so_ts_id { get; set; }
    public string so_descripcion { get; set; }
    public DateTime so_fecha_creacion { get; set; }
    // Navegación a la entidad Tipos_Solicitudes
    public Tipos_Solicitudes Tipos_Solicitudes { get; set; } // Relación de navegación
}
