using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class TelefonosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TelefonosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {
            var personaDbContext = _context.Telefonos.Include(t => t.DuenioNavigation);
            return View(await personaDbContext.ToListAsync());
        }

        // GET: Telefonos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonos/Create
        public IActionResult Create()
        {
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc");
            return View();
        }

        // POST: Telefonos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            // Verificar si el teléfono ya existe
            bool telefonoExiste = await _context.Telefonos
                .AnyAsync(t => t.Num == telefono.Num);

            // Si el teléfono ya existe, se muestra en la pantalla
            if (telefonoExiste)
            {
                ModelState.AddModelError("Num", "El número de teléfono ya existe.");
            }
            else
            {
                var persona = await _context.Personas.FirstOrDefaultAsync(m =>
                    m.Cc == telefono.Duenio
                );
                telefono.DuenioNavigation = persona;
                ModelState.Clear();
                TryValidateModel(telefono);

                if (ModelState.IsValid) // Solo si no hay errores en el ModelState
                {
                    _context.Add(telefono);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }


        // GET: Telefonos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // POST: Telefonos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(m =>
                m.Cc == telefono.Duenio
            );
            telefono.DuenioNavigation = persona;
            ModelState.Clear();
            TryValidateModel(telefono);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Num))
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
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(string id)
        {
            return _context.Telefonos.Any(e => e.Num == id);
        }
    }
}