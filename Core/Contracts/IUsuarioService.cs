using Core.Request;
using Core.Response;

namespace Core.Contracts;

public interface IUsuarioService
{
    #region CRUD
    public Task<UsuarioResponse> Add(UsuarioRequest request, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
    public Task<bool> Update(int id, UsuarioRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<UsuarioResponse>> GetAll();
    public Task<UsuarioResponse> GetById(int id);
    public Task<UsuarioResponse> GetByEmail(string correo);
    #endregion
}
