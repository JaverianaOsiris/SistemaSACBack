using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork:IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public INumeroSolicitudRepository NumeroSolicitudRepository { get; private set; }
    public ISolicitudRepository SolicitudRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        SolicitudRepository = new SolicitudRepository(_context);
        NumeroSolicitudRepository = new NumeroSolicitudRepository(_context);
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
