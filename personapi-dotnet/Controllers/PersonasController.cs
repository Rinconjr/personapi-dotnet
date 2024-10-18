using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IEstudiosRepository _estudioRepository;

        public PersonasController(IPersonaRepository personaRepository, ITelefonoRepository telefonoRepository, IEstudiosRepository estudioRepository)
        {
            _personaRepository = personaRepository;
            _telefonoRepository = telefonoRepository;
            _estudioRepository = estudioRepository;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return View(personas);
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            var ccExiste = await _personaRepository.GetPersonaByIdAsync(persona.Cc) != null;

            if (ccExiste)
            {
                ModelState.AddModelError("Cc", "La persona con esa cédula ya existe.");
            }

            if (ModelState.IsValid)
            {
                await _personaRepository.AddPersonaAsync(persona);
                return RedirectToAction(nameof(Index));
            }

            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (id != persona.Cc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _personaRepository.UpdatePersonaAsync(persona);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PersonaExists(persona.Cc))
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

            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            // Eliminar todos los teléfonos asociados a la persona
            var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
            foreach (var telefono in telefonos.Where(t => t.Duenio == id))
            {
                await _telefonoRepository.DeleteTelefonoAsync(telefono.Num);
            }

            // Eliminar todos los estudios asociados a la persona
            var estudios = await _estudioRepository.GetAllAsync();
            foreach (var estudio in estudios.Where(e => e.CcPer == id))
            {
                await _estudioRepository.DeleteEstudioAsync(estudio.IdProf,estudio.CcPer);
            }

            // Eliminar la persona
            await _personaRepository.DeletePersonaAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PersonaExists(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            return persona != null;
        }
    }
}
