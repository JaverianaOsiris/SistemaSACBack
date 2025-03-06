using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CantidadSolicitudService: ICantidadSolicitudService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CantidadSolicitudService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CantidadSolicitudResponse> Add(CantidadSolicitudRequest request, CancellationToken cancellationToken)
    {
        Cantidad_Solicitudes entity = _mapper.Map<Cantidad_Solicitudes>(request);

        await _unitOfWork.CantidadSolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result > 0)
        {
            var entityResponse = _mapper.Map<CantidadSolicitudResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async  Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.CantidadSolicitudRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<CantidadSolicitudResponse>> GetAll()
    {
        IEnumerable<Cantidad_Solicitudes?> data = await _unitOfWork.CantidadSolicitudRepository.ReadAll();
        IEnumerable<CantidadSolicitudResponse> response = _mapper.Map<IEnumerable<CantidadSolicitudResponse>>(data);
        return response;        
    }
    public async Task<CantidadSolicitudResponse> GetById(int id)
    {
        Cantidad_Solicitudes? entity = await _unitOfWork.CantidadSolicitudRepository.ReadById(x => x.cs_id.Equals(id));
        CantidadSolicitudResponse response = _mapper.Map<CantidadSolicitudResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, CantidadSolicitudRequest request, CancellationToken cancellationToken)
    {
        Cantidad_Solicitudes entity = _mapper.Map<Cantidad_Solicitudes>(request);
        entity.cs_id = id;
        await _unitOfWork.CantidadSolicitudRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
