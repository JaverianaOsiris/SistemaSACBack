using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Tipos_Usuarios
{
    [Key]
    public int tu_id { get; set; }
    public string tu_tipo_usuario { get; set; }
}
