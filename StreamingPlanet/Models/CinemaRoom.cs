using System.ComponentModel.DataAnnotations;

namespace StreamingPlanet.Models
{
    public class CinemaRoom
    {
        [Key]
        public Guid RoomId { get; set; }

        [Required]
        [MaxLength(20)]
        public string RoomName { get; set; } // Ex: 12A


    }
}
