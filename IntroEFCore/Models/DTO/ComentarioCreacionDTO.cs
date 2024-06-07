using System.ComponentModel.DataAnnotations;

namespace IntroEFCore.Models.DTO
{
    public class ComentarioCreacionDTO
    {
        [MaxLength(255)]
        public string? Contenido { get; set; }
        public bool Recomendar { get; set; }
    }
}
