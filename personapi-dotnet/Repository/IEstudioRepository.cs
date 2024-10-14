using personapi_dotnet.Models;

namespace personapi_dotnet.Repository
{
    public interface IEstudioRepository
    {
        Task<Estudios> GetEstudioByIdAsync(int idProf);
        Task<IEnumerable<Estudios>> GetAllEstudiosAsync();
        Task AddEstudioAsync(Estudios estudio);
        Task UpdateEstudioAsync(Estudios estudio);
        Task DeleteEstudioAsync(int idProf);
    }
}
