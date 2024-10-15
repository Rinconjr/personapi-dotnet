using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly IEstudioRepository _estudioRepo;
        private readonly IPersonaRepository _personaRepo;
        private readonly IProfesionRepository _profesionRepo;

        public EstudiosController(IEstudioRepository estudioRepo, IPersonaRepository personaRepo, IProfesionRepository profesionRepo)
        {
            _estudioRepo = estudioRepo;
            _personaRepo = personaRepo;
            _profesionRepo = profesionRepo;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepo.GetAllEstudiosAsync();
            return View(estudios);
        }

        // GET: Estudios/Create
        public async Task<IActionResult> Create()
        {
            // Obtener listas de personas y profesiones
            var personas = await _personaRepo.GetAllPersonasAsync();
            var profesiones = await _profesionRepo.GetAllProfesionesAsync();

            // Verificar si las listas son nulas o vacías y manejar el error adecuadamente
            if (personas == null || !personas.Any())
            {
                ModelState.AddModelError(string.Empty, "No se encontraron personas disponibles.");
            }

            if (profesiones == null || !profesiones.Any())
            {
                ModelState.AddModelError(string.Empty, "No se encontraron profesiones disponibles.");
            }

            // Si hay errores en el modelo, devolver la vista con los errores
            if (!ModelState.IsValid)
            {
                return View(); // Muestra la vista con los errores
            }

            // Asignar las listas al ViewBag para pasarlas a la vista
            ViewData["CcPer"] = new SelectList(personas, "Cc", "Nombre");
            ViewData["IdProf"] = new SelectList(profesiones, "Id", "Nom");

            return View();
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            // Verificar si ya existe un estudio para la combinación de IdProf y CcPer
            var profPerExiste = await _estudioRepo.GetEstudioByIdAsync(estudio.IdProf) != null;

            if (profPerExiste)
            {
                ModelState.AddModelError("IdProf", "La profesión para esta persona ya existe.");
            }

            if (ModelState.IsValid)
            {
                await _estudioRepo.AddEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            // Recargar los SelectList en caso de error
            var personas = await _personaRepo.GetAllPersonasAsync();
            var profesiones = await _profesionRepo.GetAllProfesionesAsync();
            ViewData["CcPer"] = new SelectList(personas, "Cc", "Nombre", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(profesiones, "Id", "Nom", estudio.IdProf);

            return View(estudio);
        }
        // GET: Estudios/Edit/5
        public async Task<IActionResult> Edit(int? idProf)
        {
            if (!idProf.HasValue)
            {
                return NotFound();
            }

            var estudio = await _estudioRepo.GetEstudioByIdAsync(idProf.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            ViewData["CcPer"] = new SelectList(await _personaRepo.GetAllPersonasAsync(), "Cc", "Nombre", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepo.GetAllProfesionesAsync(), "Id", "Nombre", estudio.IdProf);
            return View(estudio);
        }

        // POST: Estudios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (idProf != estudio.IdProf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _estudioRepo.UpdateEstudioAsync(estudio);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EstudioExists(estudio.IdProf))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewData["CcPer"] = new SelectList(await _personaRepo.GetAllPersonasAsync(), "Cc", "Nombre", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepo.GetAllProfesionesAsync(), "Id", "Nombre", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Delete/5
        public async Task<IActionResult> Delete(int? idProf)
        {
            if (!idProf.HasValue)
            {
                return NotFound();
            }

            var estudio = await _estudioRepo.GetEstudioByIdAsync(idProf.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf)
        {
            await _estudioRepo.DeleteEstudioAsync(idProf);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EstudioExists(int idProf)
        {
            return await _estudioRepo.GetEstudioByIdAsync(idProf) != null;
        }
    }
}
