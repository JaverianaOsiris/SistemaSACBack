using AutoMapper;
using Core.Contracts;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using WebApiSAC.Dtos;

namespace WebApiSAC.Controllers;

public class ColaboradorController : BaseApiController
{
    private readonly IColaboradorService _service;
    private readonly IMapper _mapper;

    public ColaboradorController(IColaboradorService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    #region API
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Escuchando");
    }

    [HttpPost("ConsultaDesempeno")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConsultaDesempeno(DesempenoReqDto desempenoReqDto, CancellationToken cancellationToken)
    {

        var desempeno = _mapper.Map<DesempenoRequest>(desempenoReqDto);

        var solicitudResuelta = await _service.GetDesempeno(desempeno);
        return Ok(solicitudResuelta);

    }

    #endregion
}
