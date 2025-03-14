using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class HistoricoSolicitudRepository : GenericRepository<Historicos_Solicitudes>, IHistoricoSolicitudRepository
{
    public HistoricoSolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }
}
