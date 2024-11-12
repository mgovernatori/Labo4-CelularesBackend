using CelularesAPI.Models.Celular;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using CelularesAPI.Models.Marca;
using CelularesAPI.Config;
using CelularesAPI.Repositories;

namespace CelularesAPI.Repositories
{
    public interface ICelularRepository : IRepository<Celular> { }


    public class CelularRepository : Repository<Celular>, ICelularRepository
    {
        public CelularRepository(AppDbContext context) : base(context) { }

        public new async Task<Celular> GetOne(Expression<Func<Celular, bool>>? filter = null)
        {
            IQueryable<Celular> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter).Include(c => c.Marca).Include(c => c.Sistema).Include(c => c.Colores);

            }

            return await query.FirstOrDefaultAsync();
        }

        public new async Task<IEnumerable<Celular>> GetAll(Expression<Func<Celular, bool>>? filter = null)
        {
            IQueryable<Celular> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.Include(c => c.Marca).Include(c => c.Sistema).Include(c => c.Colores).ToListAsync();
        }
    }
}


