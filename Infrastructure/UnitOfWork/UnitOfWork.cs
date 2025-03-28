using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork:IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public INumeroSolicitudRepository NumeroSolicitudRepository { get; private set; }
    public ISolicitudRepository SolicitudRepository { get; private set; }
    public IUsuarioRepository UsuarioRepository { get; private set; }
    public ICantidadSolicitudRepository CantidadSolicitudRepository { get; private set; }
    public IColaboradorRepository ColaboradorRepository { get; private set; }
    public ILoginRepository LoginRepository { get; private set; }
    public IHistoricoSolicitudRepository HistoricoSolicitudRepository { get; private set; }
    public IEstadoSolicitudRepository EstadoSolicitudRepository { get; private set; }
    public ITipoSolicitudRepository TipoSolicitudRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        SolicitudRepository = new SolicitudRepository(_context);
        NumeroSolicitudRepository = new NumeroSolicitudRepository(_context);
        UsuarioRepository = new UsuarioRepository(_context);
        CantidadSolicitudRepository = new CantidadSolicitudRepository(_context);
        ColaboradorRepository = new ColaboradorRepository(_context);
        LoginRepository = new LoginRepository(_context);
        HistoricoSolicitudRepository = new HistoricoSolicitudRepository(_context);
        EstadoSolicitudRepository = new EstadoSolicitudRepository(_context);
        TipoSolicitudRepository = new TipoSolicitudRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
