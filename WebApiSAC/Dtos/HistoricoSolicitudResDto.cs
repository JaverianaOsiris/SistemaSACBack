using Core.Entities;

namespace WebApiSAC.Dtos;

public class HistoricoSolicitudResDto
{
    public int hs_id { get; set; }
    public int hs_so_id { get; set; }
    public int hs_es_id { get; set; }
    public int hs_col_id { get; set; }
    public string hs_detalle { get; set; }
    public DateTime hs_fecha { get; set; }
    public Solicitudes Solicitudes { get; set; }
    public Estados_Solicitudes Estados_Solicitudes { get; set; }
    public Colaboradores Colaboradores { get; set; }
}
