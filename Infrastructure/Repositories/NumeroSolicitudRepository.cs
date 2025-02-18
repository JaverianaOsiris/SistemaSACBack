using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class NumeroSolicitudRepository : GenericRepository<Numeros_Solicitudes>, INumeroSolicitudRepository
{
    public NumeroSolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }
}
