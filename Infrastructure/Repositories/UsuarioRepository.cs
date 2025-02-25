using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UsuarioRepository : GenericRepository<Usuarios>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }
}
