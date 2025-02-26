using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;
using System.Collections.Generic;

namespace Infrastructure.Services;

public class SolicitudService : ISolicitudService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INumeroSolicitudService _numeroSolicitudService;
    private readonly IUsuarioService _usuarioService;
    string relationsUsers = "Estados_Solicitudes,Tipos_Solicitudes,Usuarios,Usuarios.Tipo_Identificacion";

    public SolicitudService(IUnitOfWork unitOfWork, IMapper mapper, INumeroSolicitudService numeroSolicitudService, IUsuarioService usuarioService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _numeroSolicitudService = numeroSolicitudService;
        _usuarioService = usuarioService;
    }

    public async Task<SolicitudResponse> Add(SolicitudRequest request, CancellationToken cancellationToken)
    {
        Solicitudes entity = _mapper.Map<Solicitudes>(request);

        var usuario = await _usuarioService.GetByEmail(request.Usuario.us_correo) ;

        if(usuario is not null)
        {
            entity.so_us_id = usuario.us_id;
        }
        else
        {
            UsuarioRequest User = _mapper.Map<UsuarioRequest>(request.Usuario);
            var newUser = await _usuarioService.Add(User, cancellationToken);


            if (newUser is not null)
            {
                entity.so_us_id = newUser.us_id;
            }
            else
            {
                return null;
            }
        }

        var numeroSolicitud = await _numeroSolicitudService.Add(cancellationToken);

        entity.so_numero_solicitud = numeroSolicitud.ns_numero;

        await _unitOfWork.SolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if(result > 0)
        {
            var entidadNueva = await GetById(entity.so_id);

            var entityResponse = _mapper.Map<SolicitudResponse>(entidadNueva);
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
        IEnumerable<Solicitudes?> data = await _unitOfWork.SolicitudRepository.ReadAll(includeProperties: relationsUsers);
        IEnumerable<SolicitudResponse> response = _mapper.Map<IEnumerable<SolicitudResponse>>(data);
        return response;
    }

    public async Task<SolicitudResponse> GetById(int id)
    {
        Solicitudes? entity = await _unitOfWork.SolicitudRepository.ReadById(x => x.so_id.Equals(id),
                                                                            includeProperties: relationsUsers);
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

    public async Task<IEnumerable<SolicitudResponse>> GetByNumber(string number)
    {
        IEnumerable<Solicitudes?> entity = await _unitOfWork.SolicitudRepository.ReadAll(x => x.so_numero_solicitud.Contains(number),
                                                                            includeProperties: relationsUsers);
        IEnumerable<SolicitudResponse> response = _mapper.Map<IEnumerable<SolicitudResponse>>(entity);
        return response;
    }
    public async Task<IEnumerable<SolicitudResponse>> GetByEmail(string Email)
    {
        var usuario = await _usuarioService.GetByEmail(Email);

        if (usuario is null)
            return null;

        IEnumerable<Solicitudes?> entity = await _unitOfWork.SolicitudRepository.ReadAll(x => x.so_us_id.Equals(usuario.us_id),
                                                                            includeProperties: relationsUsers);
        IEnumerable<SolicitudResponse> response = _mapper.Map <IEnumerable<SolicitudResponse>>(entity);
        return response;
    }
}
