using CelularesAPI.Models.Marca.DTO;
using CelularesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CelularesAPI.Models.Sistema.DTO;
using CelularesAPI.Models.Sistema;
using Microsoft.AspNetCore.Authorization;
using CelularesAPI.Models.Marca;

namespace CelularesAPI.Controllers
{
    [Route("api/sistemas")]
    [ApiController]

    public class SistemaController : ControllerBase
    {
        private readonly SistemaServices _sistemaServices;

        public SistemaController(SistemaServices sistemaServices)
        {
            _sistemaServices = sistemaServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<Marca>>> Get()
        {
            try
            {
                var sistemas = await _sistemaServices.GetAll();
                return Ok(sistemas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Marca>> Get(int id)
        {
            try
            {
                var sistema = await _sistemaServices.GetOne(id);
                return Ok(sistema);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Sistema>> Post([FromBody] CreateSistemaDTO createSistemaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sistema = await _sistemaServices.CreateOne(createSistemaDTO);
                return Created(nameof(Post), sistema);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Sistema>> Put(int id, [FromBody] UpdateSistemaDTO updateSistemaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var sistema = await _sistemaServices.UpdateOne(id, updateSistemaDTO);
                return Ok(sistema);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _sistemaServices.DeleteOne(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
