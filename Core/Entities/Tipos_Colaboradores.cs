using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Tipos_Colaboradores
{
    [Key]
    public int tc_id { get; set; }
    public string tc_cargo { get; set; }
    public string tc_descripcion { get; set; }
}
