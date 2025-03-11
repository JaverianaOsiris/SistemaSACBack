using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class LoginRepository : GenericRepository<Login>, ILoginRepository
{
    public LoginRepository(ApplicationDbContext context) : base(context)
    {
    }
}
