namespace WebApiSAC.Dtos;

public class SolicitudResDto
{
    public int so_id { get; set; }
    public string so_numero_solicitud { get; set; }
    public int so_ts_id { get; set; }
    public string so_descripcion { get; set; }
    public DateTime so_fecha_creacion { get; set; }
}
