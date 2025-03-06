using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;

namespace Core.Contracts;

public interface ICantidadSolicitudService
{
    #region CRUD
    public Task<CantidadSolicitudResponse> Add(CantidadSolicitudRequest request, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
    public Task<bool> Update(int id, CantidadSolicitudRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<CantidadSolicitudResponse>> GetAll();
    public Task<CantidadSolicitudResponse> GetById(int id);
    #endregion
}
