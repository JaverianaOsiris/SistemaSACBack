using Core.Entities;

namespace Core.Request;

public class SolicitudRequest
{
    public int so_id { get; set; }
    public int so_ts_id { get; set; }
    public string so_descripcion { get; set; }
    public int so_es_id { get; set; }
    public Usuarios Usuario { get; set; }
    

}
