using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models;
using personapi_dotnet.Repository;
using personapi_dotnet.Data;


namespace personapi_dotnet.Repository
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly ApplicationDbContext _context; // Asume que Estudio está en el mismo DbContext

        public EstudioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudios>> GetAllEstudiosAsync()
        {
            return await _context.Estudios.ToListAsync();
        }

        public async Task<Estudios> GetEstudioByIdAsync(int idProf)
        {
            return await _context.Estudios.FirstOrDefaultAsync(e => e.IdProf == idProf);
        }

        public async Task AddEstudioAsync(Estudios estudio)
        {
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstudioAsync(Estudios estudio)
        {
            _context.Entry(estudio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudioAsync(int idProf)
        {
            var estudio = await _context.Estudios.FindAsync(idProf);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
