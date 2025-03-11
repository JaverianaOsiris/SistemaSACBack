using Core.Request;
using Core.Response;

namespace Core.Contracts;

public interface ILoginService
{
    #region CRUD
    //public Task<ColaboradorResponse> Add(LoginRequest request, CancellationToken cancellationToken);
    //public Task<bool> Delete(int id, CancellationToken cancellationToken);
    //public Task<bool> Update(int id, LoginRequest request, CancellationToken cancellationToken);
    //public Task<IEnumerable<ColaboradorResponse>> GetAll();
    public Task<ColaboradorResponse> GetByUser(LoginRequest loginRequest);

    #endregion
}
