using Core.Request;
using Core.Response;

namespace Core.Contracts;

public interface ISolicitudService
{
    #region CRUD
    public Task<SolicitudResponse> Add(SolicitudRequest request, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
    public Task<bool> Update(int id, SolicitudRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<SolicitudResponse>> GetAll();
    public Task<SolicitudResponse> GetById(int id);
    #endregion
}
