using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models
{
    public class Persona
    {
        [Key] // Esta línea indica que 'Cc' es la clave primaria
        public int Cc { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public char Genero { get; set; }
        public int? Edad { get; set; }

        // Relación con Estudios
        public ICollection<Estudios>? Estudios { get; set; }
        public ICollection<Telefono>? Telefonos { get; set; }
    }
}
