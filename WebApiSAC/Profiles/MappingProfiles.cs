﻿using AutoMapper;
using Core.Entities;
using Core.Request;
using Core.Response;
using WebApiSAC.Dtos;

namespace WebApiSAC.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<NumeroSolicitudRequest, NumeroSolicitudReqDto>()
            .ReverseMap();

        CreateMap<NumeroSolicitudResponse, NumeroSolicitudResDto>()
            .ReverseMap();

        CreateMap<Numeros_Solicitudes, NumeroSolicitudResponse>()
            .ForMember(dest => dest.ns_id, opt => opt.MapFrom(src => src.ns_id))
            .ForMember(dest => dest.ns_numero, opt => opt.MapFrom(src => src.ns_numero))
            .ForMember(dest => dest.ns_fecha_creacion, opt => opt.MapFrom(src => src.ns_fecha_creacion));

        CreateMap<IEnumerable<Numeros_Solicitudes>, IEnumerable<NumeroSolicitudResponse>>()
            .ConvertUsing((src, dest, context) => src.Select(x => context.Mapper.Map<NumeroSolicitudResponse>(x)).ToList());

        CreateMap<Numeros_Solicitudes, NumeroSolicitudRequest>()
            .ForMember(dest => dest.ns_id, opt => opt.MapFrom(src => src.ns_id))
            .ForMember(dest => dest.ns_numero, opt => opt.MapFrom(src => src.ns_numero))
            .ForMember(dest => dest.ns_fecha_creacion, opt => opt.MapFrom(src => src.ns_fecha_creacion));

        CreateMap<NumeroSolicitudRequest, Numeros_Solicitudes>()
            .ForMember(dest => dest.ns_id, opt => opt.MapFrom(src => src.ns_id))
            .ForMember(dest => dest.ns_numero, opt => opt.MapFrom(src => src.ns_numero))
            .ForMember(dest => dest.ns_fecha_creacion, opt => opt.MapFrom(src => src.ns_fecha_creacion));

        CreateMap<SolicitudRequest, SolicitudReqDto>()
            .ReverseMap();

        CreateMap<SolicitudResponse, SolicitudResDto>()
            .ReverseMap();

        CreateMap<Solicitudes, SolicitudResponse>()
            .ForMember(dest => dest.so_id, opt => opt.MapFrom(src => src.so_id))
            .ForMember(dest => dest.so_numero_solicitud, opt => opt.MapFrom(src => src.so_numero_solicitud))
            .ForMember(dest => dest.so_ts_id, opt => opt.MapFrom(src => src.so_ts_id))
            .ForMember(dest => dest.so_fecha_creacion, opt => opt.MapFrom(src => src.so_fecha_creacion))
            .ForMember(dest => dest.so_es_id, opt => opt.MapFrom(src => src.so_es_id))
            .ForMember(dest => dest.so_us_id, opt => opt.MapFrom(src => src.so_us_id))
            .ForMember(dest => dest.Usuarios, opt => opt.MapFrom(src => src.Usuarios))
            .ForMember(dest => dest.Tipos_Solicitudes, opt => opt.MapFrom(src => src.Tipos_Solicitudes))
            .ForMember(dest => dest.Estados_Solicitudes, opt => opt.MapFrom(src => src.Estados_Solicitudes));

        CreateMap<IEnumerable<Solicitudes>, IEnumerable<SolicitudResponse>>()
            .ConvertUsing((src, dest, context) => src.Select(x => context.Mapper.Map<SolicitudResponse>(x)).ToList());

        CreateMap<Solicitudes, SolicitudRequest>()
            .ForMember(dest => dest.so_id, opt => opt.MapFrom(src => src.so_id))
            .ForMember(dest => dest.so_descripcion, opt => opt.MapFrom(src => src.so_descripcion))
            .ForMember(dest => dest.so_ts_id, opt => opt.MapFrom(src => src.so_ts_id));

        CreateMap<SolicitudRequest, Solicitudes>()
            .ForMember(dest => dest.so_id, opt => opt.MapFrom(src => src.so_id))
            .ForMember(dest => dest.so_descripcion, opt => opt.MapFrom(src => src.so_descripcion))
            .ForMember(dest => dest.so_ts_id, opt => opt.MapFrom(src => src.so_ts_id));

        CreateMap<Usuarios, UsuarioResponse>().ForMember(dest => dest.us_id, opt => opt.MapFrom(src => src.us_id))
            .ForMember(dest => dest.us_apellido, opt => opt.MapFrom(src => src.us_apellido))
            .ForMember(dest => dest.us_correo, opt => opt.MapFrom(src => src.us_correo))
            .ForMember(dest => dest.us_identificacion, opt => opt.MapFrom(src => src.us_identificacion))
            .ForMember(dest => dest.us_nombre, opt => opt.MapFrom(src => src.us_nombre))
            .ForMember(dest => dest.us_telefono, opt => opt.MapFrom(src => src.us_telefono))
            .ForMember(dest => dest.us_ti_id, opt => opt.MapFrom(src => src.us_ti_id))
            .ForMember(dest => dest.Tipo_Identificacion, opt => opt.MapFrom(src => src.Tipo_Identificacion));

        CreateMap<UsuarioResponse, Usuarios>()
            .ForMember(dest => dest.us_id, opt => opt.MapFrom(src => src.us_id))
            .ForMember(dest => dest.us_apellido, opt => opt.MapFrom(src => src.us_apellido))
            .ForMember(dest => dest.us_correo, opt => opt.MapFrom(src => src.us_correo))
            .ForMember(dest => dest.us_identificacion, opt => opt.MapFrom(src => src.us_identificacion))
            .ForMember(dest => dest.us_nombre, opt => opt.MapFrom(src => src.us_nombre))
            .ForMember(dest => dest.us_telefono, opt => opt.MapFrom(src => src.us_telefono))
            .ForMember(dest => dest.us_ti_id, opt => opt.MapFrom(src => src.us_ti_id))
            .ForMember(dest => dest.Tipo_Identificacion, opt => opt.MapFrom(src => src.Tipo_Identificacion));

        CreateMap<Usuarios, UsuarioRequest>()
            .ForMember(dest => dest.us_id, opt => opt.MapFrom(src => src.us_id))
            .ForMember(dest => dest.us_apellido, opt => opt.MapFrom(src => src.us_apellido))
            .ForMember(dest => dest.us_correo, opt => opt.MapFrom(src => src.us_correo))
            .ForMember(dest => dest.us_identificacion, opt => opt.MapFrom(src => src.us_identificacion))
            .ForMember(dest => dest.us_nombre, opt => opt.MapFrom(src => src.us_nombre))
            .ForMember(dest => dest.us_telefono, opt => opt.MapFrom(src => src.us_telefono))
            .ForMember(dest => dest.us_ti_id, opt => opt.MapFrom(src => src.us_ti_id)); 

        CreateMap<UsuarioRequest, Usuarios>()
            .ForMember(dest => dest.us_id, opt => opt.MapFrom(src => src.us_id))
            .ForMember(dest => dest.us_apellido, opt => opt.MapFrom(src => src.us_apellido))
            .ForMember(dest => dest.us_correo, opt => opt.MapFrom(src => src.us_correo))
            .ForMember(dest => dest.us_identificacion, opt => opt.MapFrom(src => src.us_identificacion))
            .ForMember(dest => dest.us_nombre, opt => opt.MapFrom(src => src.us_nombre))
            .ForMember(dest => dest.us_telefono, opt => opt.MapFrom(src => src.us_telefono))
            .ForMember(dest => dest.us_ti_id, opt => opt.MapFrom(src => src.us_ti_id));

        CreateMap<IEnumerable<Tipo_Identificacion>, IEnumerable<TipoIdentificacionResponse>>()
            .ConvertUsing((src, dest, context) => src.Select(x => context.Mapper.Map<TipoIdentificacionResponse>(x)).ToList());

        CreateMap<Tipo_Identificacion, TipoIdentificacionResponse>()
            .ForMember(dest => dest.ti_id, opt => opt.MapFrom(src => src.ti_id))
            .ForMember(dest => dest.ti_descripcion, opt => opt.MapFrom(src => src.ti_descripcion));

        CreateMap<TipoIdentificacionResponse, Tipo_Identificacion>()
            .ForMember(dest => dest.ti_id, opt => opt.MapFrom(src => src.ti_id))
            .ForMember(dest => dest.ti_descripcion, opt => opt.MapFrom(src => src.ti_descripcion));

        CreateMap<TipoIdentificacionResponse, TipoIdentificacionResDto>()
            .ReverseMap();

    }
}
