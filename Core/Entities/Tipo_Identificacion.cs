using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Tipo_Identificacion
{
    [Key]
    public int ti_id { get; set; }
    public string descripcion { get; set; }
}
