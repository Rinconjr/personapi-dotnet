using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionAPIController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionAPIController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        // GET: api/profesion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetAllProfesiones()
        {
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return Ok(profesiones);
        }

        // GET: api/profesion/{id}
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

        // POST: api/profesion
        [HttpPost]
        public async Task<ActionResult> AddProfesion(string nombre, string? descripcion)
        {
            var profesion = new Profesion
            {
                Nom = nombre,
                Des = descripcion
            };

            await _profesionRepository.AddProfesionAsync(profesion);
            return CreatedAtAction(nameof(GetProfesionById), new { id = profesion.Id }, profesion);
        }

        // PUT: api/profesion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesion(int id, string nombre, string? descripcion)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            profesion.Nom = nombre;
            profesion.Des = descripcion;

            await _profesionRepository.UpdateProfesionAsync(profesion);
            return NoContent();
        }

        // DELETE: api/profesion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesion(int id)
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
