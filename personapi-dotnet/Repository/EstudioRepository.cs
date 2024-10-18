using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
namespace personapi_dotnet.Repository
{
    public class EstudiosRepository : IEstudiosRepository
    {
        private readonly ArqPerDbContext _context;

        public EstudiosRepository(ArqPerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetAllAsync()
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .ToListAsync();
        }

        public async Task<Estudio?> GetEstudioByIdAsync(int ccPer, int idProf)
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(e => e.CcPer == ccPer && e.IdProf == idProf);
        }

        public async Task AddEstudioAsync(Estudio estudio)
        {
            try
            {
                // Verificar si la entidad Persona ya está adjunta al contexto
                var existingPersona = await _context.Personas.FindAsync(estudio.CcPer);
                if (existingPersona != null)
                {
                    // Adjuntar la entidad Persona existente al contexto sin cambiar su estado
                    _context.Attach(existingPersona);
                    estudio.CcPerNavigation = existingPersona; // Asegurar que la navegación use la persona existente
                }
                else
                {
                    throw new Exception("La persona no existe en la base de datos.");
                }

                // Verificar si la entidad Profesion ya está adjunta al contexto
                var existingProfesion = await _context.Profesions.FindAsync(estudio.IdProf);
                if (existingProfesion != null)
                {
                    // Adjuntar la entidad Profesion existente al contexto sin cambiar su estado
                    _context.Attach(existingProfesion);
                    estudio.IdProfNavigation = existingProfesion; // Asegurar que la navegación use la profesión existente
                }
                else
                {
                    throw new Exception("La profesión no existe en la base de datos.");
                }

                // Agregar el nuevo estudio
                await _context.Estudios.AddAsync(estudio);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Captura la excepción y maneja el error
                throw new Exception("Error al agregar el estudio: " + ex.Message, ex);
            }
        }




        public async Task UpdateEstudioAsync(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudioAsync(int ccPer, int idProf)
        {
            var estudio = await _context.Estudios
                .FirstOrDefaultAsync(e => e.CcPer == ccPer && e.IdProf == idProf);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }
    }
}