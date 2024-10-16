﻿using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProfesionAPIController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionAPIController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetAllProfesiones()
        {
            var telefonos = await _profesionRepository.GetAllProfesionesAsync();
            return Ok(telefonos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> GetProfesionById(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            return Ok(profesion);
        }

        [HttpPost]
        public async Task<ActionResult> AddProfesion([FromBody] Profesion profesion)
        {
            await _profesionRepository.AddProfesionAsync(profesion);
            return CreatedAtAction(nameof(GetProfesionById), new { id = profesion.Id }, profesion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProfesion(int id, [FromBody] Profesion profesion)
        {
            await _profesionRepository.UpdateProfesionAsync(profesion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTelefono(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            await _profesionRepository.DeleteProfesionAsync(id);
            return NoContent();
        }
    }
}