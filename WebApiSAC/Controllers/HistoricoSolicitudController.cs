using AutoMapper;
using Core.Contracts;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using WebApiSAC.Dtos;

namespace WebApiSAC.Controllers
{
    public class HistoricoSolicitudController : BaseApiController
    {
        private readonly IHistoricoSolicitudService _service;
        private readonly IMapper _mapper;

        public HistoricoSolicitudController(IHistoricoSolicitudService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("HistoricoNumero")]
        public async Task<IActionResult> HistoricoNumero(string number)
        {
            IEnumerable<HistoricoSolicitudResponse> resultResponse = await _service.GetByHistoric(number);

            var result = _mapper.Map<IEnumerable<HistoricoSolicitudResDto>>(resultResponse);
            return Ok(result);
        }
    }
}
