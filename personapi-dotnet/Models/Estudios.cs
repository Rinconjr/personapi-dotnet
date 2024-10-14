namespace personapi_dotnet.Models
{
    public class Estudios
    {
        public int IdProf { get; set; }
        public int CcPer { get; set; }
        public string Univer { get; set; }
        public DateTime? Fecha { get; set; }

        // Relaciones
        public Persona Persona { get; set; }
        public Profesiones Profesion { get; set; }
    }
}
