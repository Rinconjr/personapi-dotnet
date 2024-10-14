using personapi_dotnet.Models;

namespace personapi_dotnet.Repository
{
    /// <summary>
    /// Interfaz para el repositorio de Profesiones.
    /// </summary>
    public interface IProfesionRepository
    {
        /// <summary>
        /// Obtiene una profesión por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la profesión.</param>
        /// <returns>La profesión encontrada.</returns>
        Task<Profesiones> GetProfesionByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las profesiones.
        /// </summary>
        /// <returns>Una colección de profesiones.</returns>
        Task<IEnumerable<Profesiones>> GetAllProfesionesAsync();

        /// <summary>
        /// Agrega una nueva profesión.
        /// </summary>
        /// <param name="profesion">La profesión a agregar.</param>
        Task AddProfesionAsync(Profesiones profesion);

        /// <summary>
        /// Actualiza una profesión existente.
        /// </summary>
        /// <param name="profesion">La profesión con los datos actualizados.</param>
        Task UpdateProfesionAsync(Profesiones profesion);

        /// <summary>
        /// Elimina una profesión por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la profesión a eliminar.</param>
        Task DeleteProfesionAsync(int id);
    }
}
