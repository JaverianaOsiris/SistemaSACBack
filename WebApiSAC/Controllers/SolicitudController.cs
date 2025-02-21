using AutoMapper;
using AutoMapper.Internal;
using Core.Contracts;
using Core.Entities;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using WebApiSAC.Dtos;

namespace WebApiSAC.Controllers
{
    public class SolicitudController : BaseApiController
    {
        private readonly ISolicitudService _service;
        private readonly IMapper _mapper;

        public SolicitudController(ISolicitudService service, IMapper mapper)
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<SolicitudResponse> resultResponse = await _service.GetAll();

            var result = _mapper.Map<IEnumerable<NumeroSolicitudResDto>>(resultResponse);
            return Ok(result);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SolicitudReqDto solicitudReq, CancellationToken cancellationToken)
        {
            var numReq = _mapper.Map<SolicitudRequest>(solicitudReq);
            if (numReq.so_id == 0)
            {
                    
                var solicitud = await _service.Add(numReq, cancellationToken);
                return Ok(solicitud);

            }
            else
            {
                var solicitud = await _service.Update(numReq.so_id, numReq, cancellationToken);
                return Ok(solicitud);
            }
        }

        #endregion
    }
}
