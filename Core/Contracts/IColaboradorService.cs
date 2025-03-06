using Core.Request;
using Core.Response;

namespace Core.Contracts;

public interface IColaboradorService
{
    #region CRUD
    public Task<ColaboradorResponse> Add(ColaboradorRequest request, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
    public Task<bool> Update(int id, ColaboradorRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<ColaboradorResponse>> GetAll();
    public Task<ColaboradorResponse> GetById(int id);
    #endregion
}
