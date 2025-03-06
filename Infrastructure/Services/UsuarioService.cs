using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    string relationsUser = "Tipo_Identificacion,Tipos_Usuarios";
    public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UsuarioResponse> Add(UsuarioRequest request, CancellationToken cancellationToken)
    {
        Usuarios entity = _mapper.Map<Usuarios>(request);

        await _unitOfWork.UsuarioRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result > 0)
        {
            var entityResponse = _mapper.Map<UsuarioResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.UsuarioRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAll()
    {
        IEnumerable<Usuarios?> data = await _unitOfWork.UsuarioRepository.ReadAll(includeProperties: relationsUser);
        IEnumerable<UsuarioResponse> response = _mapper.Map<IEnumerable<UsuarioResponse>>(data);
        return response;
    }

    public async Task<UsuarioResponse> GetById(int id)
    {
        Usuarios? entity = await _unitOfWork.UsuarioRepository.ReadById(x => x.us_id.Equals(id), includeProperties: relationsUser);
        UsuarioResponse response = _mapper.Map<UsuarioResponse>(entity);
        return response;
    }

    public async Task<UsuarioResponse> GetByEmail(string correo)
    {
        Usuarios? entity = await _unitOfWork.UsuarioRepository.ReadById(x => x.us_correo.Equals(correo), includeProperties: relationsUser);

        UsuarioResponse response = _mapper.Map<UsuarioResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, UsuarioRequest request, CancellationToken cancellationToken)
    {
        Usuarios entity = _mapper.Map<Usuarios>(request);
        entity.us_id = id;
        await _unitOfWork.UsuarioRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
