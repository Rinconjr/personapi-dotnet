using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models;
using personapi_dotnet.Repository;


namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;

        public EstudioController(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudios>>> GetEstudiosAsync()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();
            return Ok(estudios);
        }

        [HttpGet("{idProf}")]
        public async Task<ActionResult<Estudios>> GetEstudioById(int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf);
            if (estudio == null)
            {
                return NotFound();
            }
            return estudio;
        }

        [HttpPost]
        public async Task<ActionResult<Estudios>> CreateEstudioAsync(Estudios estudio)
        {
            await _estudioRepository.AddEstudioAsync(estudio);
            return CreatedAtAction(nameof(GetEstudioById), new { idProf = estudio.IdProf }, estudio);
        }

        [HttpPut("{idProf}")]
        public async Task<IActionResult> UpdateEstudio(int idProf, Estudios estudio)
        {
            if (idProf != estudio.IdProf)
            {
                return BadRequest();
            }

            await _estudioRepository.UpdateEstudioAsync(estudio);

            return NoContent();
        }

        [HttpDelete("{idProf}")]
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
    }
}
