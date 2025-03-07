using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;

namespace Core.Contracts;

public interface ISolicitudService
{
    #region CRUD
    public Task<SolicitudResponse> Add(SolicitudRequest request, IFormFile file, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
    public Task<SolicitudResponse> Update(int id, SolicitudRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<SolicitudResponse>> GetAll();
    public Task<SolicitudResponse> GetById(int id);
    public Task<IEnumerable<SolicitudResponse>> GetByNumber(string number);
    public Task<IEnumerable<SolicitudResponse>> GetByEmail(string email);
    public Task<IEnumerable<SolicitudResponse>> GetByColaborator(int so_col_id, int so_es_id);
    #endregion
}
