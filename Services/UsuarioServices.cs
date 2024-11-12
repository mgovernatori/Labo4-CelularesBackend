using AutoMapper;
using CelularesAPI.Models.Celular.DTO;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Marca;
using CelularesAPI.Models.Marca.DTO;
using CelularesAPI.Models.Usuario;
using CelularesAPI.Models.Usuario.DTO;
using CelularesAPI.Repositories;
using CelularesAPI.Models.Rol;

namespace CelularesAPI.Services
{
    public class UsuarioServices
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHashingServices _hashingServices;

        public UsuarioServices(IMapper mapper, IUsuarioRepository usuarioRepository, IHashingServices hashingServices)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _hashingServices = hashingServices;
        }
        

        public async Task<Usuario> GetOneById(int id)
        {
            var usuario = await _usuarioRepository.GetOne(u => u.Id == id);
            if (usuario == null)
            {
                throw new Exception();
            }
            return usuario;
        }


        public async Task<List<UsuariosDTO>> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAll();
            return _mapper.Map<List<UsuariosDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetOne(int id)
        {
            var usuario = await GetOneById(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<Usuario> GetOneByUserOrEmail(string? user, string? email)
        {
            Usuario usuario;

            if (user == null && email == null)
            {
                throw new Exception();
            }
            if (email != null)
            {
                usuario = await _usuarioRepository.GetOne(u => u.Email == email);
            }
            else if (user != null)
            {
                usuario = await _usuarioRepository.GetOne(u => u.Username == user);
            }
            else
            {
                throw new Exception("Credenciales inválidas");
            }
            return usuario;
                
        }

        public async Task<Usuario> CreateOne(CreateUsuarioDTO createUsuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(createUsuarioDTO);

            usuario.Contraseña = _hashingServices.Hash(usuario.Contraseña);
            await _usuarioRepository.Add(usuario);
            return usuario;
        }

        public async Task<Usuario> UpdateOne(int id, UpdateUsuarioDTO updateUsuarioDTO)
        {
            var usuario = await GetOneById(id);
            var usuarioMapped = _mapper.Map(updateUsuarioDTO, usuario);

            await _usuarioRepository.Update(usuario);
            return usuarioMapped;
        }

        public async Task<Usuario> UpdateRoles(int id, List<Rol> Roles)
        {
            var usuario = await GetOneById(id);
            usuario.Roles = Roles;

            await _usuarioRepository.Update(usuario);
            return usuario;
        }

        public async Task DeleteOne(int id)
        {
            var usuario = await GetOneById(id);
            await _usuarioRepository.Delete(usuario);
        }
    }
}
