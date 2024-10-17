using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Profesion>>> GetProfesionesAsync()
        {
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return Ok(profesiones);
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
        public async Task<ActionResult<Profesion>> CreateProfesionAsync([FromBody] Profesion profesiones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _profesionRepository.AddProfesionAsync(profesiones);
            return CreatedAtAction(nameof(GetProfesionById), new { id = profesiones.Id }, profesiones);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesion(int id, [FromBody] Profesion profesiones)
        {
            if (id != profesiones.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _profesionRepository.UpdateProfesionAsync(profesiones);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesion(int id)
        {
            var profesionToDelete = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesionToDelete == null)
            {
                return NotFound();
            }

            await _profesionRepository.DeleteProfesionAsync(id);
            return NoContent();
        }
    }
}
