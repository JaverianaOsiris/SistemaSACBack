using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Estados_Solicitudes
{
    [Key]
    public int es_id { get; set; }
    public string es_nombre_estado { get; set; }
}
