// using Microsoft.AspNetCore.Mvc;
// using personapi_dotnet.Models.Entities;
// using personapi_dotnet.Repository;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace personapi_dotnet.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UnifiedController : ControllerBase
//     {
//         private readonly IEstudioRepository _estudioRepository;
//         private readonly IPersonaRepository _personaRepository;
//         private readonly IProfesionRepository _profesionRepository;
//         private readonly ITelefonoRepository _telefonoRepository;

//         public UnifiedController(IEstudioRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository, ITelefonoRepository telefonoRepository)
//         {
//             _estudioRepository = estudioRepository;
//             _personaRepository = personaRepository;
//             _profesionRepository = profesionRepository;
//             _telefonoRepository = telefonoRepository;
//         }

//         // Estudios Endpoints
//         [HttpGet("estudios")]
//         public async Task<ActionResult<IEnumerable<Estudio>>> GetEstudiosAsync()
//         {
//             var estudios = await _estudioRepository.GetAllEstudiosAsync();
//             return Ok(estudios);
//         }

//         [HttpGet("estudios/{idProf}")]
//         public async Task<ActionResult<Estudio>> GetEstudioById(int idProf)
//         {
//             var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf);
//             if (estudio == null)
//             {
//                 return NotFound($"No se encontró un estudio con IdProf: {idProf}");
//             }
//             return Ok(estudio);
//         }

//         [HttpPost("estudios")]
//         public async Task<ActionResult<Estudio>> CreateEstudioAsync(Estudio estudio)
//         {
//             if (estudio == null)
//             {
//                 return BadRequest("La información del estudio no puede ser nula.");
//             }

//             // Validación adicional si es necesario verificar duplicados
//             var existingEstudio = await _estudioRepository.GetEstudioByIdAsync(estudio.IdProf);
//             if (existingEstudio != null)
//             {
//                 return Conflict("El estudio ya existe.");
//             }

//             await _estudioRepository.AddEstudioAsync(estudio);
//             return CreatedAtAction(nameof(GetEstudioById), new { idProf = estudio.IdProf }, estudio);
//         }

//         [HttpPut("estudios/{idProf}")]
//         public async Task<IActionResult> UpdateEstudio(int idProf, Estudio estudio)
//         {
//             if (idProf != estudio.IdProf)
//             {
//                 return BadRequest("El ID de la ruta no coincide con el ID del estudio.");
//             }

//             var existingEstudio = await _estudioRepository.GetEstudioByIdAsync(idProf);
//             if (existingEstudio == null)
//             {
//                 return NotFound($"No se encontró un estudio con IdProf: {idProf}");
//             }

//             await _estudioRepository.UpdateEstudioAsync(estudio);
//             return NoContent();
//         }

//         [HttpDelete("estudios/{idProf}")]
//         public async Task<IActionResult> DeleteEstudio(int idProf)
//         {
//             var estudioToDelete = await _estudioRepository.GetEstudioByIdAsync(idProf);
//             if (estudioToDelete == null)
//             {
//                 return NotFound($"No se encontró un estudio con IdProf: {idProf}");
//             }

//             await _estudioRepository.DeleteEstudioAsync(idProf);
//             return NoContent();
//         }

//         // Personas Endpoints
//         [HttpGet("personas")]
//         public async Task<ActionResult<IEnumerable<Persona>>> GetAllPersonas()
//         {
//             var personas = await _personaRepository.GetAllPersonasAsync();
//             return Ok(personas);
//         }

//         [HttpGet("personas/{id}")]
//         public async Task<ActionResult<Persona>> GetPersonaById(int id)
//         {
//             var persona = await _personaRepository.GetPersonaByIdAsync(id);
//             if (persona == null) return NotFound($"No se encontró una persona con Id: {id}");
//             return Ok(persona);
//         }

//         [HttpPost("personas")]
//         public async Task<ActionResult> CreatePersona(Persona persona)
//         {
//             if (persona == null)
//             {
//                 return BadRequest("La información de la persona no puede ser nula.");
//             }

//             await _personaRepository.AddPersonaAsync(persona);
//             return CreatedAtAction(nameof(GetPersonaById), new { id = persona.Cc }, persona);
//         }

//         [HttpPut("personas/{id}")]
//         public async Task<IActionResult> UpdatePersona(int id, Persona persona)
//         {
//             if (id != persona.Cc) return BadRequest("El ID de la ruta no coincide con el ID de la persona.");

//             var existingPersona = await _personaRepository.GetPersonaByIdAsync(id);
//             if (existingPersona == null)
//             {
//                 return NotFound($"No se encontró una persona con Id: {id}");
//             }

//             await _personaRepository.UpdatePersonaAsync(persona);
//             return NoContent();
//         }

//         [HttpDelete("personas/{id}")]
//         public async Task<IActionResult> DeletePersona(int id)
//         {
//             var existingPersona = await _personaRepository.GetPersonaByIdAsync(id);
//             if (existingPersona == null)
//             {
//                 return NotFound($"No se encontró una persona con Id: {id}");
//             }

//             await _personaRepository.DeletePersonaAsync(id);
//             return NoContent();
//         }

//         // Profesiones Endpoints
//         [HttpGet("profesiones")]
//         public async Task<ActionResult<IEnumerable<Profesion>>> GetProfesionesAsync()
//         {
//             var profesiones = await _profesionRepository.GetAllProfesionesAsync();
//             return Ok(profesiones);
//         }

//         [HttpGet("profesiones/{id}")]
//         public async Task<ActionResult<Profesion>> GetProfesionById(int id)
//         {
//             var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
//             if (profesion == null)
//             {
//                 return NotFound($"No se encontró una profesión con Id: {id}");
//             }
//             return Ok(profesion);
//         }

//         [HttpPost("profesiones")]
//         public async Task<ActionResult<Profesion>> CreateProfesionAsync(Profesion profesion)
//         {
//             if (profesion == null)
//             {
//                 return BadRequest("La información de la profesión no puede ser nula.");
//             }

//             await _profesionRepository.AddProfesionAsync(profesion);
//             return CreatedAtAction(nameof(GetProfesionById), new { id = profesion.Id }, profesion);
//         }

//         [HttpPut("profesiones/{id}")]
//         public async Task<IActionResult> UpdateProfesion(int id, Profesion profesion)
//         {
//             if (id != profesion.Id)
//             {
//                 return BadRequest("El ID de la ruta no coincide con el ID de la profesión.");
//             }

//             var existingProfesion = await _profesionRepository.GetProfesionByIdAsync(id);
//             if (existingProfesion == null)
//             {
//                 return NotFound($"No se encontró una profesión con Id: {id}");
//             }

//             await _profesionRepository.UpdateProfesionAsync(profesion);
//             return NoContent();
//         }

//         [HttpDelete("profesiones/{id}")]
//         public async Task<IActionResult> DeleteProfesion(int id)
//         {
//             var existingProfesion = await _profesionRepository.GetProfesionByIdAsync(id);
//             if (existingProfesion == null)
//             {
//                 return NotFound($"No se encontró una profesión con Id: {id}");
//             }

//             await _profesionRepository.DeleteProfesionAsync(id);
//             return NoContent();
//         }

//         // Teléfonos Endpoints
//         [HttpGet("telefonos")]
//         public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonosAsync()
//         {
//             var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
//             return Ok(telefonos);
//         }

//         [HttpGet("telefonos/{num}")]
//         public async Task<ActionResult<Telefono>> GetTelefonoByNum(string num)
//         {
//             var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
//             if (telefono == null)
//             {
//                 return NotFound($"No se encontró un teléfono con el número: {num}");
//             }
//             return Ok(telefono);
//         }

//         [HttpPost("telefonos")]
//         public async Task<ActionResult<Telefono>> CreateTelefonoAsync(Telefono telefono)
//         {
//             if (telefono == null)
//             {
//                 return BadRequest("La información del teléfono no puede ser nula.");
//             }

//             await _telefonoRepository.AddTelefonoAsync(telefono);
//             return CreatedAtAction(nameof(GetTelefonoByNum), new { num = telefono.Num }, telefono);
//         }

//         [HttpPut("telefonos/{num}")]
//         public async Task<IActionResult> UpdateTelefono(string num, Telefono telefono)
//         {
//             if (num != telefono.Num)
//             {
//                 return BadRequest("El número de la ruta no coincide con el número del teléfono.");
//             }

//             var existingTelefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
//             if (existingTelefono == null)
//             {
//                 return NotFound($"No se encontró un teléfono con el número: {num}");
//             }

//             await _telefonoRepository.UpdateTelefonoAsync(telefono);
//             return NoContent();
//         }

//         [HttpDelete("telefonos/{num}")]
//         public async Task<IActionResult> DeleteTelefono(string num)
//         {
//             var existingTelefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
//             if (existingTelefono == null)
//             {
//                 return NotFound($"No se encontró un teléfono con el número: {num}");
//             }

//             await _telefonoRepository.DeleteTelefonoAsync(num);
//             return NoContent();
//         }
//     }
// }
