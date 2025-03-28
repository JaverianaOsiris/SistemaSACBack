namespace WebApiSAC.Dtos;

public class ColaboradorReqDto
{
    public string col_nombre { get; set; }
    public string col_apellido { get; set; }
    public string col_identificacion { get; set; }
    public string col_telefono { get; set; }
    public string col_email { get; set; }
    public int col_tc_id { get; set; }
    public int col_tu_id { get; set; }
    public int col_activo { get; set; }
    public int col_col_id_lider { get; set; }
    public int lo_bloqueo { get; set; }
}
