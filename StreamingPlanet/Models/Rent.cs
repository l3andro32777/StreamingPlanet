using System.ComponentModel.DataAnnotations;
using StreamingPlanet.Data;

namespace StreamingPlanet.Models
{
    public class Rent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        //Tempo é independente do dia e é fixado para cada 30 mins.
        public TimeSlot timeSlot { get; set; }

    }
}
