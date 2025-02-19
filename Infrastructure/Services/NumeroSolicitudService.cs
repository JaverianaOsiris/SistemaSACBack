using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;


namespace Infrastructure.Services;

public class NumeroSolicitudService : INumeroSolicitudService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NumeroSolicitudService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NumeroSolicitudResponse> Add(CancellationToken cancellationToken)
    {
        var entity = new Numeros_Solicitudes();
        var consecutivo = await CreateConsecutive();

        entity.ns_numero = consecutivo;
        await _unitOfWork.NumeroSolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (result > 0)
        {
            var entityResponse = _mapper.Map<NumeroSolicitudResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.NumeroSolicitudRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<NumeroSolicitudResponse>> GetAll()
    {
        IEnumerable<Numeros_Solicitudes?> data = await _unitOfWork.NumeroSolicitudRepository.ReadAll();
        IEnumerable<NumeroSolicitudResponse> response = _mapper.Map<IEnumerable<NumeroSolicitudResponse>>(data);
        return response;
    }

    public async Task<NumeroSolicitudResponse> GetById(int id)
    {
        Numeros_Solicitudes? entity = await _unitOfWork.NumeroSolicitudRepository.ReadById(x => x.ns_id.Equals(id), includeProperties: string.Empty);
        NumeroSolicitudResponse response = _mapper.Map<NumeroSolicitudResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, NumeroSolicitudRequest request, CancellationToken cancellationToken)
    {
        Numeros_Solicitudes entity = _mapper.Map<Numeros_Solicitudes>(request);
        await _unitOfWork.NumeroSolicitudRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    private async Task<NumeroSolicitudResponse> GetLast()
    {
        var data = await _unitOfWork.NumeroSolicitudRepository.ReadAll(); 

        var lastNumeroSolicitud = data.OrderByDescending(ns => ns.ns_id).FirstOrDefault();

        if (lastNumeroSolicitud == null)
        {
            return null;
        }

        var response = _mapper.Map<NumeroSolicitudResponse>(lastNumeroSolicitud);

        return new NumeroSolicitudResponse
        {
            ns_numero = response.ns_numero 
        };
    }

    private async Task<string> CreateConsecutive()
    {
        var ultimoConsecutivo = await GetLast(); 

        if (ultimoConsecutivo == null)
        {
            return "A00000001";
        }

        string consecutivo = ultimoConsecutivo.ns_numero; 

        string numero = consecutivo.Substring(consecutivo.Length - 8); 
        string prefijo = consecutivo.Substring(0, consecutivo.Length - 8); 

        int numeroActual = int.Parse(numero);

        numeroActual++;

        if (numeroActual > 99999999)
        {
            numeroActual = 1; 
            prefijo = IncrementPrefix(prefijo); 
        }

        string nuevoNumero = numeroActual.ToString("D8");  

        return prefijo + nuevoNumero;
    }

    private string IncrementPrefix(string prefijo)
    {
        if (prefijo.Length == 1)
        {
            if (prefijo == "Z")
            {
                return "AA";
            }
            else
            {
                return ((char)(prefijo[0] + 1)).ToString();
            }
        }
        else
        {
            char lastChar = prefijo[prefijo.Length - 1];

            if (lastChar == 'Z')
            {
                return IncrementPrefix(prefijo.Substring(0, prefijo.Length - 1)) + "A";
            }
            else
            {
                return prefijo.Substring(0, prefijo.Length - 1) + (char)(lastChar + 1);
            }
        }
    }

}
