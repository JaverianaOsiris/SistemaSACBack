using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Tipos_Solicitudes
{
    [Key]
    public int ts_id { get; set; }
    [Required]
    public string ts_nombre { get; set; }
    public string ts_descripcion { get; set; }
    public int ts_prioridad { get; set; }

}