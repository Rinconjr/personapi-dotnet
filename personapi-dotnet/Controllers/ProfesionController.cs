using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using personapi_dotnet.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace personapi_dotnet.Controllers
{
    public class ProfesionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profesion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesiones.ToListAsync());
        }

        // GET: Profesion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: Profesion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] Profesion profesion)
        {
            bool profesionExiste = await _context.Profesiones
                .AnyAsync(t => t.Id == profesion.Id);

            if (profesionExiste)
            {
                ModelState.AddModelError("Id", "El id de profesión ya existe.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(profesion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(profesion);
        }

        // GET: Profesion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesiones.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionExists(profesion.Id))
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
            return View(profesion);
        }

        // GET: Profesion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // POST: Profesion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesion = await _context.Profesiones.FindAsync(id);
            if (profesion != null)
            {
                var estudiosAsociados = await _context.Estudios
                    .Where(e => e.IdProf == id)
                    .ToListAsync();

                if (estudiosAsociados.Any())
                {
                    _context.Estudios.RemoveRange(estudiosAsociados);
                }

                _context.Profesiones.Remove(profesion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionExists(int id)
        {
            return _context.Profesiones.Any(e => e.Id == id);
        }
    }
}
