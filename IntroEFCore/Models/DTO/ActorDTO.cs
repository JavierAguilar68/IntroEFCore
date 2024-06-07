using System.ComponentModel.DataAnnotations;

namespace IntroEFCore.Models.DTO
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Nombre { get; set; } = null!;
    }
}
