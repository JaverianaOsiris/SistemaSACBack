namespace Core.Interfaces;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    INumeroSolicitudRepository NumeroSolicitudRepository { get; }
}
