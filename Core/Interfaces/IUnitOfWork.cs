namespace Core.Interfaces;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    INumeroSolicitudRepository NumeroSolicitudRepository { get; }
    ISolicitudRepository SolicitudRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    ICantidadSolicitudRepository CantidadSolicitudRepository { get; }
    IColaboradorRepository ColaboradorRepository { get; }
    ILoginRepository LoginRepository { get; }
    IHistoricoSolicitudRepository HistoricoSolicitudRepository { get; }
    IEstadoSolicitudRepository EstadoSolicitudRepository { get; }
    ITipoSolicitudRepository TipoSolicitudRepository { get; }
}
