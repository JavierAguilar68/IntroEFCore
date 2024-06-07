using System.ComponentModel.DataAnnotations;

namespace IntroEFCore.Models.DTO
{
    public class ActorCreacionDTO
    {
        [StringLength(maximumLength:150)]
        public string Nombre { get; set; } = null!;
        public decimal Fortuna { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
