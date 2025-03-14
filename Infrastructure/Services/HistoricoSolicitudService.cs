using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services;

public class HistoricoSolicitudService : IHistoricoSolicitudService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    string relationsUser = "Solicitudes,Estados_Solicitudes,Colaboradores";
    public HistoricoSolicitudService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<HistoricoSolicitudResponse> Add(HistoricoSolicitudRequest request, CancellationToken cancellationToken)
    {
        Historicos_Solicitudes entity = _mapper.Map<Historicos_Solicitudes>(request);

        await _unitOfWork.HistoricoSolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result > 0)
        {
            var entityResponse = _mapper.Map<HistoricoSolicitudResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.HistoricoSolicitudRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<HistoricoSolicitudResponse>> GetAll()
    {
        IEnumerable<Historicos_Solicitudes?> data = await _unitOfWork.HistoricoSolicitudRepository.ReadAll(includeProperties: relationsUser);
        IEnumerable<HistoricoSolicitudResponse> response = _mapper.Map<IEnumerable<HistoricoSolicitudResponse>>(data);
        return response;
    }

    public async Task<IEnumerable<HistoricoSolicitudResponse>> GetByHistoric(string number)
    {
        Solicitudes? entity = await _unitOfWork.SolicitudRepository.ReadById(x => x.so_numero_solicitud.Equals(number),
                                                                            includeProperties: "Estados_Solicitudes,Tipos_Solicitudes,SolicitudApelacion,Usuarios,Usuarios.Tipo_Identificacion,Usuarios.Tipos_Usuarios,Colaboradores");
        SolicitudResponse responseSO = _mapper.Map<SolicitudResponse>(entity);

        IEnumerable<Historicos_Solicitudes?> entitySo = await _unitOfWork.HistoricoSolicitudRepository.ReadAll(x => x.hs_so_id.Equals(responseSO.so_id),
                                                                            includeProperties: relationsUser);
        IEnumerable<HistoricoSolicitudResponse> response = _mapper.Map<IEnumerable<HistoricoSolicitudResponse>>(entitySo);
        return response;
    }

    public async Task<HistoricoSolicitudResponse> GetById(int id)
    {
        Historicos_Solicitudes? entity = await _unitOfWork.HistoricoSolicitudRepository.ReadById(x => x.hs_id.Equals(id), includeProperties: relationsUser);
        HistoricoSolicitudResponse response = _mapper.Map<HistoricoSolicitudResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, HistoricoSolicitudRequest request, CancellationToken cancellationToken)
    {
        Historicos_Solicitudes entity = _mapper.Map<Historicos_Solicitudes>(request);
        entity.hs_id = id;
        await _unitOfWork.HistoricoSolicitudRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
