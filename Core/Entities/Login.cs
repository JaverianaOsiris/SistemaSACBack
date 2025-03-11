using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Login
{
    [Key]
    public int lo_id { get; set; }

    public int lo_co_id { get; set; }
    public string lo_password { get; set; }
    public bool lo_bloqueo { get; set; }
    public DateTime lo_fechaIngreso { get; set; }
    public Colaboradores Colaboradores { get; set; }
}

