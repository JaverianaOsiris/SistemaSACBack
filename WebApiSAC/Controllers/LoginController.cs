using AutoMapper;
using Core.Contracts;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiSAC.Dtos;

namespace WebApiSAC.Controllers;

public class LoginController : BaseApiController
{
    private readonly ILoginService _service;
    private readonly IMapper _mapper;

    public LoginController(ILoginService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    #region API
    
    [HttpPost("Login")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginReqDto loginReqDto, CancellationToken cancellationToken)
    {

        var login = _mapper.Map<LoginRequest>(loginReqDto);

        var solicitudResuelta = await _service.GetByUser(login);
        return Ok(solicitudResuelta);

    }
    #endregion
}
