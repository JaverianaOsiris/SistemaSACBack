using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Cantidad_Solicitudes
{
    [Key]
    public int cs_id { get; set; }
    public int cs_col_id { get; set; }
    public int cs_ts_id { get; set; }
    public int cs_es_id { get; set; }
    public int cs_cantidad { get; set; }
    public Colaboradores Colaboradores { get; set; }
    public Tipos_Solicitudes Tipos_Solicitudes { get; set; }
    public Estados_Solicitudes Estados_Solicitudes { get; set; }
}
