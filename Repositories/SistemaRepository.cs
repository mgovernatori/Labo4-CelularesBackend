using CelularesAPI.Models.Sistema;
using CelularesAPI.Config;
using CelularesAPI.Repositories;

namespace CelularesAPI.Repositories
{
    public interface ISistemaRepository : IRepository<Sistema> { }

    public class SistemaRepository : Repository<Sistema>, ISistemaRepository
    {
        public SistemaRepository(AppDbContext context) : base(context) { }
    }
}
