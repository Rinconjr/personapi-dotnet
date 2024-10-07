using System.Collections.Generic;

namespace personapi_dotnet.Models
{
    public class Profesion
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Des { get; set; }

        // Relación con Estudios
        public ICollection<Estudios> Estudios { get; set; }
    }
}
