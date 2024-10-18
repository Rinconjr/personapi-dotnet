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
        private readonly IEstudiosRepository _estudioRepo;
        private readonly IPersonaRepository _personaRepo;
        private readonly IProfesionRepository _profesionRepo;

        public EstudiosController(IEstudiosRepository estudioRepo, IPersonaRepository personaRepo, IProfesionRepository profesionRepo)
        {
            _estudioRepo = estudioRepo;
            _personaRepo = personaRepo;
            _profesionRepo = profesionRepo;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepo.GetAllAsync();
            return View(estudios);
        }

        // GET: Estudios/Create
        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepo.GetAllPersonasAsync();
            var profesiones = await _profesionRepo.GetAllProfesionesAsync();

            if (personas == null || !personas.Any())
            {
                ModelState.AddModelError(string.Empty, "No se encontraron personas disponibles.");
            }

            if (profesiones == null || !profesiones.Any())
            {
                ModelState.AddModelError(string.Empty, "No se encontraron profesiones disponibles.");
            }

            if (!ModelState.IsValid)
            {
                return View(); // Muestra la vista con los errores
            }

            ViewData["CcPer"] = new SelectList(personas, "Cc", "Nombre");
            ViewData["IdProf"] = new SelectList(profesiones, "Id", "Nom");

            return View();
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            var profPerExiste = await _estudioRepo.GetEstudioByIdAsync(estudio.CcPer, estudio.IdProf) != null;

            if (profPerExiste)
            {
                ModelState.AddModelError("IdProf", "La combinación de profesión y persona ya existe.");
            }

            if (ModelState.IsValid)
            {
                await _estudioRepo.AddEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepo.GetAllPersonasAsync();
            var profesiones = await _profesionRepo.GetAllProfesionesAsync();
            ViewData["CcPer"] = new SelectList(personas, "Cc", "Nombre", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(profesiones, "Id", "Nom", estudio.IdProf);

            return View(estudio);
        }

        // GET: Estudios/Edit/5
        public async Task<IActionResult> Edit(int? ccPer, int? idProf)
        {
            if (!ccPer.HasValue || !idProf.HasValue)
            {
                return NotFound();
            }

            var estudio = await _estudioRepo.GetEstudioByIdAsync(ccPer.Value, idProf.Value);
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
        public async Task<IActionResult> Edit(int ccPer, int idProf, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (ccPer != estudio.CcPer || idProf != estudio.IdProf)
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
                    if (!await EstudioExists(estudio.CcPer, estudio.IdProf))
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
        public async Task<IActionResult> Delete(int? ccPer, int? idProf)
        {
            if (!ccPer.HasValue || !idProf.HasValue)
            {
                return NotFound();
            }

            var estudio = await _estudioRepo.GetEstudioByIdAsync(ccPer.Value, idProf.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ccPer, int idProf)
        {
            await _estudioRepo.DeleteEstudioAsync(ccPer, idProf);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EstudioExists(int ccPer, int idProf)
        {
            return await _estudioRepo.GetEstudioByIdAsync(ccPer, idProf) != null;
        }
    }
}
