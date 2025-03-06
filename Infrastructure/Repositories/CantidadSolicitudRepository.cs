using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CantidadSolicitudRepository : GenericRepository<Cantidad_Solicitudes>, ICantidadSolicitudRepository
{
    public CantidadSolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }
}
