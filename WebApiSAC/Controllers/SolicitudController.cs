using AutoMapper;
using AutoMapper.Internal;
using Core.Contracts;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using WebApiSAC.Dtos;

namespace WebApiSAC.Controllers
{
    public class SolicitudController : BaseApiController
    {
        private readonly INumeroSolicitudService _service;
        private readonly IMapper _mapper;

        public SolicitudController(INumeroSolicitudService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<NumeroSolicitudResponse> resultResponse = await _service.GetAll();

            var result = _mapper.Map<IEnumerable<NumeroSolicitudResDto>>(resultResponse);
            return Ok(result);
            //return Json(new { data = result });
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NumeroSolicitudReqDto numero, CancellationToken cancellationToken)
        {
            var numReq = _mapper.Map<NumeroSolicitudRequest>(numero);
            if (numReq.ns_id == 0)
            {
                    
                await _service.Add(numReq, cancellationToken);
                    
            }
            else
            {
                await _service.Update(numReq.ns_id, numReq, cancellationToken);
            }

            return Ok("Bien");


        }

        #endregion
    }
}
