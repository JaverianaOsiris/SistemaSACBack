namespace Core.Request;

public class HistoricoSolicitudRequest
{
    public int hs_so_id { get; set; }
    public int hs_es_id { get; set; }
    public int? hs_col_id { get; set; }
    public string hs_detalle { get; set; }
    public DateTime hs_fecha { get; set; }
}
