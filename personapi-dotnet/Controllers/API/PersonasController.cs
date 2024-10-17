using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasAPIController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonasAPIController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetById(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Persona persona)
        {
            await _personaRepository.AddPersonaAsync(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Persona persona)
        {
            if (id != persona.Cc) return BadRequest();
            await _personaRepository.UpdatePersonaAsync(persona);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personaRepository.DeletePersonaAsync(id);
            return NoContent();
        }
    }
}
