using CelularesAPI.Config;
using CelularesAPI.Models.Marca;
using CelularesAPI.Repositories;

namespace CelularesAPI.Repositories
{
    public interface IMarcaRepository : IRepository<Marca> { }

    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        public MarcaRepository(AppDbContext context) : base(context) { }   

    }
}
