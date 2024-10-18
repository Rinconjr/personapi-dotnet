using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Repository;


namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly IEstudiosRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudiosController(IEstudiosRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllAsync();
            return View(estudios);
        }

        // GET: Estudios/Details/#
        public async Task<IActionResult> Details(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAsync((int)ccPer, (int)idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: Estudios/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Id");
            return View();
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            var profPerExiste = await _estudioRepository.GetEstudioByIdAsync(estudio.CcPer, estudio.IdProf) != null;

            if (profPerExiste)
            {
                ModelState.AddModelError("CcPer", "La profesión para esa persona ya existe.");
            }
            else
            {
                var profesion = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);
                estudio.IdProfNavigation = profesion;

                var persona = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);
                estudio.CcPerNavigation = persona;

                ModelState.Clear();
                TryValidateModel(estudio);

                if (ModelState.IsValid)
                {
                    await _estudioRepository.AddEstudioAsync(estudio);
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Edit/#
        public async Task<IActionResult> Edit(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAsync((int)ccPer, (int)idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // POST: Estudios/Edit/#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return NotFound();
            }

            var profesion = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);
            estudio.IdProfNavigation = profesion;

            var persona = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);
            estudio.CcPerNavigation = persona;

            ModelState.Clear();
            TryValidateModel(estudio);

            if (ModelState.IsValid)
            {
                try
                {
                    await _estudioRepository.UpdateEstudioAsync(estudio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EstudioExists(estudio.IdProf, estudio.CcPer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Delete/#
        public async Task<IActionResult> Delete(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAsync((int)ccPer, (int)idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/#
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteEstudioAsync(ccPer, idProf);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EstudioExists(int idProf, int ccPer)
        {
            return await _estudioRepository.GetEstudioByIdAsync(ccPer, idProf) != null;
        }
    }
}