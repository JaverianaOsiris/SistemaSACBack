using Core.Entities;

namespace WebApiSAC.Dtos;

public class SolicitudResDto
{
    public int so_id { get; set; }
    public string so_numero_solicitud { get; set; }
    public int so_ts_id { get; set; }
    public string so_descripcion { get; set; }
    public DateTime so_fecha_creacion { get; set; }
    public int so_es_id { get; set; }
    public int so_us_id { get; set; }
    public string url_image { get; set; }
    public Usuarios Usuarios { get; set; }
    public Tipos_Solicitudes Tipos_Solicitudes { get; set; } 
    public Estados_Solicitudes Estados_Solicitudes { get; set; }
}

