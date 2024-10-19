using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoAPIController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonoAPIController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        // GET: api/telefono
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetAllTelefonos()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
            return Ok(telefonos);
        }

        // GET: api/telefono/{duenio}
        [HttpGet("{num}")]
        public async Task<ActionResult<Telefono>> GetTelefonoByDuenio(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return Ok(telefono);
        }

        // POST: api/telefono
        [HttpPost]
        public async Task<ActionResult<Telefono>> CreateTelefonoAsync(string num, string oper, int duenio)
        {
            var telefono = new Telefono
            {
                Num = num,
                Oper = oper,
                Duenio = duenio
            };

            await _telefonoRepository.AddTelefonoAsync(telefono);
            return CreatedAtAction(nameof(GetTelefonoByDuenio), new { num = telefono.Num }, telefono);
        }

        // PUT: api/telefono/{duenio}
        [HttpPut("{duenio}")]
        public async Task<IActionResult> UpdateTelefono(string numero, string operador, int dueno)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }

            telefono.Num = numero;
            telefono.Oper = operador;

            await _telefonoRepository.UpdateTelefonoAsync(telefono);
            return NoContent();
        }

        // DELETE: api/telefono/{numero}
        [HttpDelete("{numero}")]
        public async Task<IActionResult> DeleteTelefono(string numero)
        {
            var telefonoToDelete = await _telefonoRepository.GetTelefonoByIdAsync(numero);
            if (telefonoToDelete == null)
            {
                return NotFound();
            }

            await _telefonoRepository.DeleteTelefonoAsync(numero);
            return NoContent();
        }
    }
}
