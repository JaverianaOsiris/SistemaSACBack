using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;

namespace Infrastructure.Services;

public class NumeroSolicitudService : INumeroSolicitudService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NumeroSolicitudService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Add(NumeroSolicitudRequest request, CancellationToken cancellationToken)
    {
        Numeros_Solicitudes entity = _mapper.Map<Numeros_Solicitudes>(request);
        await _unitOfWork.NumeroSolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.NumeroSolicitudRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<NumeroSolicitudResponse>> GetAll()
    {
        IEnumerable<Numeros_Solicitudes?> data = await _unitOfWork.NumeroSolicitudRepository.ReadAll();
        IEnumerable<NumeroSolicitudResponse> response = _mapper.Map<IEnumerable<NumeroSolicitudResponse>>(data);
        return response;
    }

    public async Task<NumeroSolicitudResponse> GetById(int id)
    {
        Numeros_Solicitudes? entity = await _unitOfWork.NumeroSolicitudRepository.ReadById(x => x.ns_id.Equals(id), includeProperties: string.Empty);
        NumeroSolicitudResponse response = _mapper.Map<NumeroSolicitudResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, NumeroSolicitudRequest request, CancellationToken cancellationToken)
    {
        Numeros_Solicitudes entity = _mapper.Map<Numeros_Solicitudes>(request);
        await _unitOfWork.NumeroSolicitudRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
