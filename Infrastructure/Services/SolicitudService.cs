using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;

namespace Infrastructure.Services;

public class SolicitudService : ISolicitudService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INumeroSolicitudService _numeroSolicitudService;

    public SolicitudService(IUnitOfWork unitOfWork, IMapper mapper, INumeroSolicitudService numeroSolicitudService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _numeroSolicitudService = numeroSolicitudService;
    }

    public async Task<SolicitudResponse> Add(SolicitudRequest request, CancellationToken cancellationToken)
    {
        Solicitudes entity = _mapper.Map<Solicitudes>(request);

        var numeroSolicitud = await _numeroSolicitudService.Add(cancellationToken);

        entity.so_numero_solicitud = numeroSolicitud.ns_numero;

        await _unitOfWork.SolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if(result > 0)
        {
            var entityResponse = _mapper.Map<SolicitudResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.SolicitudRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<SolicitudResponse>> GetAll()
    {
        IEnumerable<Solicitudes?> data = await _unitOfWork.SolicitudRepository.ReadAll();
        IEnumerable<SolicitudResponse> response = _mapper.Map<IEnumerable<SolicitudResponse>>(data);
        return response;
    }

    public async Task<SolicitudResponse> GetById(int id)
    {
        Solicitudes? entity = await _unitOfWork.SolicitudRepository.ReadById(x => x.so_id.Equals(id), includeProperties: string.Empty);
        SolicitudResponse response = _mapper.Map<SolicitudResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, SolicitudRequest request, CancellationToken cancellationToken)
    {
        Solicitudes entity = _mapper.Map<Solicitudes>(request);
        await _unitOfWork.SolicitudRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

   
}
