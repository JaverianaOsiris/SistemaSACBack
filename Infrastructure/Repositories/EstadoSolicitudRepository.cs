using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EstadoSolicitudRepository : GenericRepository<Estados_Solicitudes>, IEstadoSolicitudRepository
{
    public EstadoSolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }
}

