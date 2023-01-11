using Microsoft.AspNetCore.Identity;

namespace StreamingPlanet.Models
{
    public class CinemaUser : IdentityUser
    {
        [PersonalData]
        public String Name { get; set; }
    }
}
