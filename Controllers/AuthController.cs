using AutoMapper;
using CelularesAPI.Models.Auth;
using CelularesAPI.Models.Usuario.DTO;
using CelularesAPI.Services;
using CelularesAPI.Models.Rol;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CelularesAPI.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly UsuarioServices _usuarioServices;
        private readonly IHashingServices _hashingServices;
        private readonly RolServices _rolServices;

        public AuthController(IAuthServices authServices, UsuarioServices usuarioServices, IHashingServices hashingServices, RolServices rolServices)
        {
            _authServices = authServices;
            _usuarioServices = usuarioServices;
            _hashingServices = hashingServices;
            _rolServices = rolServices;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Login([FromBody] Login login)
        {
            try
            {
                var usuario = await _usuarioServices.GetOneByUserOrEmail(login.Username, login.Email);

                var correctPass = _hashingServices.Verify(login.Contraseña, usuario.Contraseña);

                if (!correctPass)
                {
                    return Unauthorized("Acceso inválido");
                }

                var token = _authServices.GenerateToken(usuario);
                return Ok(new { user = usuario, token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<UsuarioDTO>> Register([FromBody] CreateUsuarioDTO createUsuarioDTO)
        {
            try
            {
                var usuarioVerif = await _usuarioServices.GetOneByUserOrEmail(createUsuarioDTO.Username, createUsuarioDTO.Email);

                if (usuarioVerif != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El usuario ya existe");
                }

                var usuario = await _usuarioServices.CreateOne(createUsuarioDTO);
                var rolAsignado = await _rolServices.GetOneByNombre("Usuario");

                await _usuarioServices.UpdateRoles(usuario.Id, new List<Rol> { rolAsignado });

                return Created("Registrar", usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


    }
}
