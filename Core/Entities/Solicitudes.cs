﻿using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Solicitudes
{
    [Key]
    public int so_id { get; set; }
    public string so_numero_solicitud { get; set; }
    public int so_ts_id { get; set; }
    public string so_descripcion { get; set; }
    public DateTime so_fecha_creacion { get; set; } = DateTime.Now;
    public int so_es_id { get; set; }
    public int so_us_id { get; set; }
    public string so_url_image { get; set; }
    // Navegación a la entidad Tipos_Solicitudes
    public Tipos_Solicitudes Tipos_Solicitudes { get; set; } // Relación de navegación
    public Estados_Solicitudes Estados_Solicitudes { get; set; }
    public Usuarios Usuarios { get; set; }
}
