using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;

namespace Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    //string relationsUser = "Tipo_Identificacion,Tipos_Usuarios";

    public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ColaboradorResponse> GetByUser(LoginRequest loginRequest)
    {
        Colaboradores? entity = await _unitOfWork.ColaboradorRepository.ReadById(x => x.col_identificacion.Equals(loginRequest.col_identificacion) && x.col_activo == true);
        ColaboradorResponse response = _mapper.Map<ColaboradorResponse>(entity);

        if (response != null)
        {
            Login? login = await _unitOfWork.LoginRepository.ReadById(x => x.lo_password.Equals(loginRequest.lo_password));

            if(login == null)
                return null;
            
            return response;
        }
        else
        {
            return null;
        }
    }
}
