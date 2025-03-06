namespace Core.Response;

public class ColaboradorResponse
{
    public int col_id { get; set; }
    public string col_nombre { get; set; }
    public string col_apellido { get; set; }
    public string col_identificacion { get; set; }
    public string col_telefono { get; set; }
    public string col_email { get; set; }
    public int col_tc_id { get; set; }
    public int col_tu_id { get; set; }
    public int? col_col_id_lider { get; set; }
    public bool col_activo { get; set; }
}
