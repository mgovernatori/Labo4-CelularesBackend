using CelularesAPI.Config;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Color;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace CelularesAPI.Repositories
{
    public interface IColorRepository : IRepository<Color> { }

    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context) { }

    }
 
}
