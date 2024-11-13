using CelularesAPI.Config;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Celular.DTO;
using CelularesAPI.Models.Color;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CelularesAPI.Repositories
{
    public interface IColoresCelularRepository : IRepository<ColoresCelular> { }

    public class ColoresCelularRepository : Repository<ColoresCelular>, IColoresCelularRepository
    {
        public ColoresCelularRepository(AppDbContext context) : base(context) { }
    }
}
