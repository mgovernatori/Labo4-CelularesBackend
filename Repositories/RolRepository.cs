using CelularesAPI.Models.Marca;
using CelularesAPI.Config;
using CelularesAPI.Models.Rol;
using Microsoft.EntityFrameworkCore;

namespace CelularesAPI.Repositories
{

    public interface IRolRepository : IRepository<Rol> { }
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        public RolRepository(AppDbContext context) : base(context) { }

    }

}
