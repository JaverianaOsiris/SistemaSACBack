using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;

namespace Infrastructure.Services;

public class ColaboradorService : IColaboradorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ColaboradorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ColaboradorResponse> Add(ColaboradorRequest request, CancellationToken cancellationToken)
    {
        Colaboradores entity = _mapper.Map<Colaboradores>(request);

        await _unitOfWork.ColaboradorRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result > 0)
        {
            var entityResponse = _mapper.Map<ColaboradorResponse>(entity);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.ColaboradorRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<ColaboradorResponse>> GetAll()
    {
        IEnumerable<Colaboradores?> data = await _unitOfWork.ColaboradorRepository.ReadAll();
        IEnumerable<ColaboradorResponse> response = _mapper.Map<IEnumerable<ColaboradorResponse>>(data);
        return response;
    }

    public async Task<ColaboradorResponse> GetById(int id)
    {
        Colaboradores? entity = await _unitOfWork.ColaboradorRepository.ReadById(x => x.col_id.Equals(id));
        ColaboradorResponse response = _mapper.Map<ColaboradorResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, ColaboradorRequest request, CancellationToken cancellationToken)
    {
        Colaboradores entity = _mapper.Map<Colaboradores>(request);
        entity.col_id = id;
        await _unitOfWork.ColaboradorRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
