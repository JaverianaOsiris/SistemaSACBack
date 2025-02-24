using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Estados_Solicitudes
{
    [Key]
    public int es_id { get; set; }
    public string nombre_estado { get; set; }
}
