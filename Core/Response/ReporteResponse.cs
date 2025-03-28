using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class ReporteResponse
    {
        public string Fecha { get; set; }
        public List<EstadoSolicitudesResponse> EstadosSolicitudes { get; set; }
    }

}
