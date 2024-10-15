using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using personapi_dotnet.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _appDbcontext;
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public HomeController(ApplicationDbContext appDbContext, IEstudioRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _appDbcontext = appDbContext;
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        // ---- MÉTODOS DE VISTAS ----

        // Vista para listar todas las profesiones
        [HttpGet]
        public async Task<IActionResult> ListaProfesion()
        {
            List<Profesion> lista = await _appDbcontext.Profesiones.ToListAsync();
            return View(lista);
        }

        // Vista para crear una nueva profesión
        [HttpGet]
        public IActionResult NuevoProfesion()
        {
            return View();
        }

        // Proceso para crear una nueva profesión
        [HttpPost]
        public async Task<IActionResult> NuevoProfesion(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.AddProfesionAsync(profesion);
                return RedirectToAction("ListaProfesion");
            }
            return View(profesion);
        }

        // Vista para listar todos los estudios
        [HttpGet]
        public async Task<IActionResult> ListaEstudios()
        {
            List<Estudio> lista = await _appDbcontext.Estudios.ToListAsync();
            return View(lista);
        }

        // Vista para crear un nuevo estudio
        [HttpGet("estudios/create")]
        public async Task<IActionResult> CreateEstudio()
        {
            ViewBag.Personas = await _personaRepository.GetAllPersonasAsync();
            ViewBag.Profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return View();
        }

        // Proceso de creación del estudio (Vistas)
        [HttpPost("estudios/create")]
        public async Task<IActionResult> CreateEstudio(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.AddEstudioAsync(estudio);
                return RedirectToAction("ListaEstudios");
            }
            return View(estudio);
        }

        // Vista para editar un Estudio
        [HttpGet("estudios/edit/{idProf}")]
        public async Task<IActionResult> EditEstudio(int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf);
            if (estudio == null) return NotFound();

            ViewBag.Personas = await _personaRepository.GetAllPersonasAsync();
            ViewBag.Profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return View(estudio);
        }

        // Proceso de edición del estudio (Vistas)
        [HttpPost("estudios/edit/{idProf}")]
        public async Task<IActionResult> EditEstudio(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.UpdateEstudioAsync(estudio);
                return RedirectToAction("ListaEstudios");
            }
            return View(estudio);
        }

        // Vista para eliminar un estudio
        [HttpPost("estudios/delete/{idProf}")]
        public async Task<IActionResult> DeleteEstudio(int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            await _estudioRepository.DeleteEstudioAsync(idProf);
            return RedirectToAction("ListaEstudios");
        }
    }
}
