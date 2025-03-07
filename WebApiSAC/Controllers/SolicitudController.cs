using AutoMapper;
using AutoMapper.Internal;
using Core.Contracts;
using Core.Entities;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiSAC.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var result = _mapper.Map<IEnumerable<SolicitudResDto>>(resultResponse);
            return Ok(result);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert([FromForm] string solicitudReqText, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            SolicitudReqDto solicitudReq = JsonConvert.DeserializeObject<SolicitudReqDto>(solicitudReqText);
            var numReq = _mapper.Map<SolicitudRequest>(solicitudReq);
            if (numReq.so_id == 0)
            {

                var solicitud = await _service.Add(numReq, file, cancellationToken);
                return Ok(solicitud);

            }
            else
            {
                var solicitud = await _service.Update(numReq.so_id, numReq, cancellationToken);
                return Ok(solicitud);
            }
        }

        [HttpGet("SolicitudNumber")]
        public async Task<IActionResult> SolicitudNumber(string number)
        {
            IEnumerable<SolicitudResponse> resultResponse = await _service.GetByNumber(number);

            var result = _mapper.Map< IEnumerable<SolicitudResDto>>(resultResponse);
            return Ok(result);
        }

        [HttpGet("SolicitudEmail")]
        public async Task<IActionResult> SolicitudEmail(string Email)
        {
            IEnumerable<SolicitudResponse> resultResponse = await _service.GetByEmail(Email);

            var result = _mapper.Map<IEnumerable<SolicitudResDto>>(resultResponse);
            return Ok(result);
        }

        [HttpGet("SolicitudGetColaborator")]
        public async Task<IActionResult> SolicitudGetColaborator(int so_col_id, int so_es_id)
        {
            IEnumerable<SolicitudResponse> resultResponse = await _service.GetByColaborator(so_col_id, so_es_id);

            var result = _mapper.Map<IEnumerable<SolicitudResDto>>(resultResponse);
            return Ok(result);
        }

        [HttpPost("RespuestaSolicitud")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> RespuestaSolicitud(SolicitudReqDto solicitudReqDto, CancellationToken cancellationToken)
        {

            var solicitud = _mapper.Map<SolicitudRequest>(solicitudReqDto);

            var solicitudResuelta = await _service.Update(solicitud.so_id, solicitud, cancellationToken);
            return Ok(solicitudResuelta);

        }

        #endregion

    }
}
