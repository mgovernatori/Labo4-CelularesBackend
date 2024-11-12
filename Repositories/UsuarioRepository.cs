using CelularesAPI.Models.Usuario;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using CelularesAPI.Config;
using CelularesAPI.Repositories;

namespace CelularesAPI.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario> { }

    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public new async Task<Usuario> GetOne(Expression<Func<Usuario, bool>>? filter = null)
        {
            IQueryable<Usuario> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).Include(u => u.Roles);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
