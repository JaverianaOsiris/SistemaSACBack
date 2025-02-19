using Core.Request;
using Core.Response;

namespace Core.Contracts
{
    public interface INumeroSolicitudService
    {
        #region CRUD
        public Task<NumeroSolicitudResponse> Add(CancellationToken cancellationToken);
        public Task<bool> Delete(int id, CancellationToken cancellationToken);
        public Task<bool> Update(int id, NumeroSolicitudRequest request, CancellationToken cancellationToken);
        public Task<IEnumerable<NumeroSolicitudResponse>> GetAll();
        public Task<NumeroSolicitudResponse> GetById(int id);
        #endregion
    }
}
