﻿using Core.Entities;

namespace WebApiSAC.Dtos;

public class SolicitudReqDto
{
    public int so_id { get; set; }
    public int so_ts_id { get; set; }
    public string so_descripcion { get; set; }
    public Tipos_Solicitudes Tipos_Solicitudes { get; set; }
}
