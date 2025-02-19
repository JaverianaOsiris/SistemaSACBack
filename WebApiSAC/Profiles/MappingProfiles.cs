using AutoMapper;
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
            .ForMember(dest => dest.so_fecha_creacion, opt => opt.MapFrom(src => src.so_fecha_creacion));
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

    }
}
