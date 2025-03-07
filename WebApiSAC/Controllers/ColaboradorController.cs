using AutoMapper;
using Core.Contracts;
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

    
    #endregion
}
