using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    /// <summary>
    /// Interfaz para el repositorio de Profesion.
    /// </summary>
    public interface IProfesionRepository
    {
        /// <summary>
        /// Obtiene una profesión por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la profesión.</param>
        /// <returns>La profesión encontrada.</returns>
        Task<Profesion> GetProfesionByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las Profesion.
        /// </summary>
        /// <returns>Una colección de Profesion.</returns>
        Task<IEnumerable<Profesion>> GetAllProfesionAsync();

        /// <summary>
        /// Agrega una nueva profesión.
        /// </summary>
        /// <param name="profesion">La profesión a agregar.</param>
        Task AddProfesionAsync(Profesion profesion);

        /// <summary>
        /// Actualiza una profesión existente.
        /// </summary>
        /// <param name="profesion">La profesión con los datos actualizados.</param>
        Task UpdateProfesionAsync(Profesion profesion);

        /// <summary>
        /// Elimina una profesión por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la profesión a eliminar.</param>
        Task DeleteProfesionAsync(int id);
    }
}
