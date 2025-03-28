using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class TipoSolicitudRepository : GenericRepository<Tipos_Solicitudes>, ITipoSolicitudRepository
{
    public TipoSolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }
}
