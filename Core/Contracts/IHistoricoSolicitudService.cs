using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;

namespace Core.Contracts;

public interface IHistoricoSolicitudService
{
    public Task<HistoricoSolicitudResponse> Add(HistoricoSolicitudRequest request, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
    public Task<bool> Update(int id, HistoricoSolicitudRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<HistoricoSolicitudResponse>> GetAll();
    public Task<HistoricoSolicitudResponse> GetById(int id);
    public Task<IEnumerable<HistoricoSolicitudResponse>> GetByHistoric(string number);
}
