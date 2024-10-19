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

        // GET: api/personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return Ok(personas);
        }

        // GET: api/personas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetById(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        // POST: api/personas
        [HttpPost]
        public async Task<ActionResult> Create(string nombre, string apellido, int edad, string? genero, int cc)
        {
            var persona = new Persona
            {
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Genero = genero,
                Cc = cc
            };

            await _personaRepository.AddPersonaAsync(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        // PUT: api/personas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string nombre, string apellido, int? edad, string? genero)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null) return NotFound();

            persona.Nombre = nombre;
            persona.Apellido = apellido;
            if (edad.HasValue) persona.Edad = edad.Value;
            persona.Genero = genero;

            await _personaRepository.UpdatePersonaAsync(persona);
            return NoContent();
        }

        // DELETE: api/personas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null) return NotFound();

            await _personaRepository.DeletePersonaAsync(id);
            return NoContent();
        }
    }
}
