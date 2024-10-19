using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioAPIController : ControllerBase
    {
        private readonly IEstudiosRepository _estudiosRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudioAPIController(IEstudiosRepository estudiosRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudiosRepository = estudiosRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetAllEstudios()
        {
            var estudios = await _estudiosRepository.GetAllAsync();
            return Ok(estudios);
        }

        [HttpGet("{ccPer}/{idProf}")]
        public async Task<ActionResult<Estudio>> GetEstudioById(int ccPer, int idProf)
        {
            var estudio = await _estudiosRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            return Ok(estudio);
        }

        
        [HttpPost]
        public async Task<ActionResult> AddEstudio(int profesion, int cedula, DateOnly date, string universidad)
        {
            // Verificar si la persona ya existe
            var persona = await _personaRepository.GetPersonaByIdAsync(cedula);
            if (persona == null)
            {
                return NotFound($"La persona con cédula {cedula} no existe.");
            }

            // Verificar si la profesión ya existe
            var profesionExistente = await _profesionRepository.GetProfesionByIdAsync(profesion);
            if (profesionExistente == null)
            {
                return NotFound($"La profesión con ID {profesion} no existe.");
            }

            // Crear el nuevo estudio y asociar las entidades existentes
            var newEstudio = new Estudio
            {
                IdProf = profesionExistente.Id,  // Usar la profesión existente
                CcPer = persona.Cc,              // Usar la persona existente
                Fecha = date,
                Univer = universidad,
                CcPerNavigation = persona,
                IdProfNavigation = profesionExistente
            };

            // Añadir el nuevo estudio sin modificar las entidades relacionadas
            try
            {
                await _estudiosRepository.AddEstudioAsync(newEstudio);
                return CreatedAtAction(nameof(GetEstudioById), new { ccPer = newEstudio.CcPer, idProf = newEstudio.IdProf }, newEstudio);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al agregar el estudio: {ex.Message}");
            }
        }



        [HttpPut("{ccPer}/{idProf}")]
        public async Task<IActionResult> Update(int ccPer, int idProf, string universidad, DateOnly date)
        {
            // Obtener el estudio a actualizar
            var estudio = await _estudiosRepository.GetEstudioByIdAsync(ccPer, idProf);

            // Verificar si el estudio existe
            if (estudio == null)
            {
                return NotFound();
            }

            // Actualizar los campos de universidad si no están vacíos
            if (!string.IsNullOrEmpty(universidad))
            {
                estudio.Univer = universidad;
            }

            // Actualizar el campo de fecha si no está vacío
            if (date != default)
            {
                estudio.Fecha = date;
            }

            // Actualizar el estudio en el repositorio
            await _estudiosRepository.UpdateEstudioAsync(estudio);

            return NoContent();
        }

        [HttpDelete("{ccPer}/{idProf}")]
        public async Task<ActionResult> DeleteEstudio(int ccPer, int idProf)
        {
            var estudio = await _estudiosRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            await _estudiosRepository.DeleteEstudioAsync(ccPer, idProf);
            return NoContent();
        }
    }
}