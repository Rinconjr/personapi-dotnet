using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models;
using personapi_dotnet.Repository;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnifiedController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;
        private readonly ITelefonoRepository _telefonoRepository;

        public UnifiedController(IEstudioRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository, ITelefonoRepository telefonoRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
            _telefonoRepository = telefonoRepository;
        }


        [HttpGet("estudios")]
        public async Task<ActionResult<IEnumerable<Estudios>>> GetEstudiosAsync()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();
            return Ok(estudios);
        }

        [HttpGet("estudios/{idProf}")]
        public async Task<ActionResult<Estudios>> GetEstudioById(int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf);
            if (estudio == null)
            {
                return NotFound();
            }
            return estudio;
        }

        [HttpPost("estudios")]
        public async Task<ActionResult<Estudios>> CreateEstudioAsync(Estudios estudio)
        {
            await _estudioRepository.AddEstudioAsync(estudio);
            return CreatedAtAction(nameof(GetEstudioById), new { idProf = estudio.IdProf }, estudio);
        }

        [HttpPut("estudios/{idProf}")]
        public async Task<IActionResult> UpdateEstudio(int idProf, Estudios estudio)
        {
            if (idProf != estudio.IdProf)
            {
                return BadRequest();
            }

            await _estudioRepository.UpdateEstudioAsync(estudio);

            return NoContent();
        }

        [HttpDelete("estudios/{idProf}")]
        public async Task<IActionResult> DeleteEstudio(int idProf)
        {
            var estudioToDelete = await _estudioRepository.GetEstudioByIdAsync(idProf);
            if (estudioToDelete == null)
            {
                return NotFound();
            }

            await _estudioRepository.DeleteEstudioAsync(idProf);

            return NoContent();
        }

        // Personas Endpoints
        [HttpGet("personas")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAllPersonas()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return Ok(personas);
        }

        [HttpGet("personas/{id}")]
        public async Task<ActionResult<Persona>> GetPersonaById(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        [HttpPost("personas")]
        public async Task<ActionResult> CreatePersona(Persona persona)
        {
            await _personaRepository.AddPersonaAsync(persona);
            return CreatedAtAction(nameof(GetPersonaById), new { id = persona.Cc }, persona);
        }

        [HttpPut("personas/{id}")]
        public async Task<IActionResult> UpdatePersona(int id, Persona persona)
        {
            if (id != persona.Cc) return BadRequest();
            await _personaRepository.UpdatePersonaAsync(persona);
            return NoContent();
        }

        [HttpDelete("personas/{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            await _personaRepository.DeletePersonaAsync(id);
            return NoContent();
        }

        // Profesiones Endpoints
        [HttpGet("profesiones")]
        public async Task<ActionResult<IEnumerable<Profesiones>>> GetProfesionesAsync()
        {
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return Ok(profesiones);
        }

        [HttpGet("profesiones/{id}")]
        public async Task<ActionResult<Profesiones>> GetProfesionById(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return Ok(profesion);
        }

        [HttpPost("profesiones")]
        public async Task<ActionResult<Profesiones>> CreateProfesionAsync(Profesiones profesiones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _profesionRepository.AddProfesionAsync(profesiones);
            return CreatedAtAction(nameof(GetProfesionById), new { id = profesiones.Id }, profesiones);
        }

        [HttpPut("profesiones/{id}")]
        public async Task<IActionResult> UpdateProfesion(int id, Profesiones profesiones)
        {
            if (id != profesiones.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _profesionRepository.UpdateProfesionAsync(profesiones);
            return NoContent();
        }

        [HttpDelete("profesiones/{id}")]
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

        // Telefonos Endpoints
        [HttpGet("telefonos")]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonosAsync()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
            return Ok(telefonos);
        }

        [HttpGet("telefonos/{duenio}")]
        public async Task<ActionResult<Telefono>> GetTelefonoByDuenio(int duenio)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(duenio);
            if (telefono == null)
            {
                return NotFound();
            }
            return telefono;
        }

        [HttpPost("telefonos")]
        public async Task<ActionResult<Telefono>> CreateTelefonoAsync(Telefono telefono)
        {
            await _telefonoRepository.AddTelefonoAsync(telefono);
            return CreatedAtAction(nameof(GetTelefonoByDuenio), new { duenio = telefono.Duenio }, telefono);
        }

        [HttpPut("telefonos/{duenio}")]
        public async Task<IActionResult> UpdateTelefono(int duenio, Telefono telefono)
        {
            if (duenio != telefono.Duenio)
            {
                return BadRequest();
            }

            await _telefonoRepository.UpdateTelefonoAsync(telefono);

            return NoContent();
        }

        [HttpDelete("telefonos/{duenio}")]
        public async Task<IActionResult> DeleteTelefono(int duenio)
        {
            var telefonoToDelete = await _telefonoRepository.GetTelefonoByIdAsync(duenio);
            if (telefonoToDelete == null)
            {
                return NotFound();
            }

            await _telefonoRepository.DeleteTelefonoAsync(duenio);

            return NoContent();
        }
    }
}