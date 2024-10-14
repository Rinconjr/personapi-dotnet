using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models;

namespace personapi_dotnet.Repository
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfesionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesiones>> GetAllProfesionesAsync()
        {
            return await _context.Profesiones.AsNoTracking().ToListAsync();
        }

        public async Task<Profesiones> GetProfesionByIdAsync(int id)
        {
            return await _context.Profesiones.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProfesionAsync(Profesiones profesion)
        {
            try
            {
                _context.Profesiones.Add(profesion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al agregar la profesión", ex);
            }
        }

        public async Task UpdateProfesionAsync(Profesiones profesion)
        {
            if (!await _context.Profesiones.AnyAsync(p => p.Id == profesion.Id))
            {
                throw new KeyNotFoundException("La profesión no existe");
            }

            _context.Entry(profesion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesionAsync(int id)
        {
            var profesion = await _context.Profesiones.FindAsync(id);
            if (profesion != null)
            {
                _context.Profesiones.Remove(profesion);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("La profesión no existe");
            }
        }
    }
}
