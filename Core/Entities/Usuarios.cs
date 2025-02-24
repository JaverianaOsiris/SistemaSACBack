using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Usuarios
{
    [Key]
    public int us_id { get; set; }
    public string us_nombre { get; set; }
    public string us_apellido { get; set; }
    public int us_ti_id { get; set; }
    public string us_identificacion { get; set; }
    public string us_telefono { get; set; }
    [EmailAddress]
    [Required]
    public string us_correo { get; set; }
    public Tipo_Identificacion Tipo_Identificacion { get; set; }

}
