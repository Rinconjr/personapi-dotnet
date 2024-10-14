﻿using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models
{
    public class Telefono
    {
        [Key] // Esta línea indica que 'Num' es la clave primaria
        public string Num { get; set; }

        public string Oper { get; set; }

        // Relación con Persona
        public int Duenio { get; set; }
        public Persona? Persona { get; set; }
    }
}
