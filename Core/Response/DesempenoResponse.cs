using System.Reflection.Metadata.Ecma335;

namespace Core.Response;

public class DesempenoResponse
{
    public int? ColaboradorId { get; set; } // El ID del colaborador (so_col_id)
    public List<EstadoSolicitudesResponse> EstadosSolicitudes { get; set; }
}

public class EstadoSolicitudesResponse
{
    public string Estado { get; set; } // Nombre del estado de la solicitud
    public int Cantidad { get; set; } // La cantidad de solicitudes en ese estado y colaborador
}
