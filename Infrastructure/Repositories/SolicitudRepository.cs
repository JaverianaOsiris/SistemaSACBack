using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class SolicitudRepository : GenericRepository<Solicitudes>, ISolicitudRepository
{
    public SolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }
}
