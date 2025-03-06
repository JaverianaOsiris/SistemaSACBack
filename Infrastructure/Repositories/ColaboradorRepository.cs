using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ColaboradorRepository : GenericRepository<Colaboradores>, IColaboradorRepository
{
    public ColaboradorRepository(ApplicationDbContext context) : base(context)
    {
    }
}
