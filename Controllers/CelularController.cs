using AutoMapper;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Celular.DTO;
using CelularesAPI.Models.Color.DTO;
using CelularesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelularesAPI.Controllers
{
    [Route("api/celulares")]
    [ApiController]

    public class CelularController : ControllerBase
    {
        private readonly CelularServices _celularServices;

        public CelularController(CelularServices celularServices)
        {
            _celularServices = celularServices;
        }   

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<CelularesDTO>>> Get()
        {
            try
            {
                var celulares = await _celularServices.GetAll();
                return Ok(celulares);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("marca/{marca}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<CelularesDTO>> GetByMarca(string marca)
        {
            try
            {
                var celulares = await _celularServices.GetAllByMarca(marca);

                if (celulares == null || !celulares.Any())
                {
                    return NotFound();
                }
                return Ok(celulares);
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

        public async Task<ActionResult<CelularDTO>> Get(int id)
        {
            try
            {
                var celular = await _celularServices.GetOne(id);
                if (celular  == null)
                {
                    return NotFound(id);
                }
                return Ok(celular);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Celular>> Post([FromBody] CreateCelularDTO createCelularDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var celular = await _celularServices.CreateOne(createCelularDTO);
                return Created(nameof(Post), celular);
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

        public async Task<ActionResult<Celular>> Put(int id, [FromBody] UpdateCelularDTO updateCelularDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var celular = await _celularServices.UpdateOne(id, updateCelularDTO);
                return Ok(celular);
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
                await _celularServices.DeleteOne(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}