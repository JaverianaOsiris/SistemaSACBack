using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ColaboradorService : IColaboradorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    string relationsUsers = "Estados_Solicitudes,Tipos_Solicitudes,SolicitudApelacion,Usuarios,Usuarios.Tipo_Identificacion,Usuarios.Tipos_Usuarios,Colaboradores";

    public ColaboradorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ColaboradorResponse> Add(ColaboradorRequest request, CancellationToken cancellationToken)
    {
        Colaboradores entity = _mapper.Map<Colaboradores>(request);

        await _unitOfWork.ColaboradorRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result > 0)
        {
            var entityResponse = _mapper.Map<ColaboradorResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.ColaboradorRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<ColaboradorResponse>> GetAll()
    {
        IEnumerable<Colaboradores?> data = await _unitOfWork.ColaboradorRepository.ReadAll();
        IEnumerable<ColaboradorResponse> response = _mapper.Map<IEnumerable<ColaboradorResponse>>(data);
        return response;
    }

    public async Task<ColaboradorResponse> GetById(int id)
    {
        Colaboradores? entity = await _unitOfWork.ColaboradorRepository.ReadById(x => x.col_id.Equals(id));
        ColaboradorResponse response = _mapper.Map<ColaboradorResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, ColaboradorRequest request, CancellationToken cancellationToken)
    {
        Colaboradores entity = _mapper.Map<Colaboradores>(request);
        entity.col_id = id;
        await _unitOfWork.ColaboradorRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<DesempenoResponse>> GetDesempeno(DesempenoRequest desempenoRequest)
    {
        DateTime? fechaInicio = new DateTime();
        DateTime? fechaFin = new DateTime();
        var estados = await _unitOfWork.EstadoSolicitudRepository.ReadAll(); 
        var estadosList = estados.ToList();

        var solicitudes = await _unitOfWork.SolicitudRepository.ReadAll(includeProperties: relationsUsers);
        var solicitudesList = solicitudes.ToList();

        // Lista de filtros dinámicos
        var filters = new List<Func<Solicitudes, bool>>();

        // Filtros condicionales basados en lo que llega en el request
        if (desempenoRequest.so_col_id != null)
            filters.Add(s => s.so_col_id == desempenoRequest.so_col_id);

        if (desempenoRequest.so_es_id != null)
            filters.Add(s => s.so_es_id == desempenoRequest.so_es_id);

        if (desempenoRequest.so_ts_id != null)
            filters.Add(s => s.so_ts_id == desempenoRequest.so_ts_id);

        // Aplicar los filtros dinámicos
        foreach (var filter in filters)
        {
            solicitudesList = solicitudesList.Where(filter).ToList();
        }

        if (desempenoRequest.so_fec_ini != null) 
        {
            fechaInicio = Convert.ToDateTime(desempenoRequest.so_fec_ini);
        }

        if (desempenoRequest.so_fec_final != null)
        {
            fechaFin = Convert.ToDateTime(desempenoRequest.so_fec_ini + " 23:59:59");
        }

        // Filtro para el rango de fechas
        if (desempenoRequest.so_fec_ini != null && desempenoRequest.so_fec_final != null)
        {
            solicitudesList = solicitudesList.Where(s =>
                s.so_fecha_creacion >= fechaInicio &&
                s.so_fecha_creacion <= fechaFin).ToList();
        }
        else if (desempenoRequest.so_fec_ini != null)
        {
            // Si solo tenemos la fecha inicial
            solicitudesList = solicitudesList.Where(s => s.so_fecha_creacion >= fechaInicio).ToList();
        }
        else if (desempenoRequest.so_fec_final != null)
        {
            // Si solo tenemos la fecha final
            solicitudesList = solicitudesList.Where(s => s.so_fecha_creacion <= fechaFin).ToList();
        }

        var solicitudesPorEstado = (from solicitud in solicitudesList // Trabajamos con la lista materializada
                                    join estado in estadosList
                                    on solicitud.so_es_id equals estado.es_id
                                    group solicitud by new { estado.es_nombre_estado, solicitud.so_col_id } into solicitudGroup
                                    select new
                                    {
                                        ColaboradorId = solicitudGroup.Key.so_col_id, // El ID del colaborador
                                        Estado = solicitudGroup.Key.es_nombre_estado, // Nombre del estado
                                        Cantidad = solicitudGroup.Count() // Contamos la cantidad de solicitudes
                                    }).ToList();

        // Reestructurar la respuesta para agrupar los estados por colaborador
        var groupedResult = solicitudesPorEstado
            .GroupBy(s => s.ColaboradorId) // Agrupamos por colaborador
            .Select(g => new DesempenoResponse
            {
                ColaboradorId = g.Key, // El ID del colaborador
                EstadosSolicitudes = g.Select(s => new EstadoSolicitudesResponse
                {
                    Estado = s.Estado, // Nombre del estado
                    Cantidad = s.Cantidad // Cantidad de solicitudes en ese estado
                }).ToList() // Convertimos los estados a lista
            }).ToList();

        return groupedResult;


    }
}
