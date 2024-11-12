using CelularesAPI.Models.Rol;
using CelularesAPI.Repositories;

namespace CelularesAPI.Services
{
    public class RolServices
    {
        private readonly IRolRepository _rolRepository;
        
        public RolServices(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<Rol> GetOneByNombre(string nombre)
        {
            var rol = await _rolRepository.GetOne(r => r.Nombre == nombre);
            if (rol == null)
            {
                throw new Exception("No se encontró el rol.");
            }
            return rol;
        }
    }
}
