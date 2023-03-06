using System.ComponentModel.DataAnnotations;

namespace StreamingPlanet.Models
{
    public class CinemaRoom
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Nome da sala")]
        public string RoomName { get; set; } // Ex: 12A

        [Display(Name = "Capacidade Máxima")]
        public int MaxCapacity { get; set; }

        [Display(Name = "Disponibilidade")]
        public bool IsAvailable { get; set; } = false;


    }
}
