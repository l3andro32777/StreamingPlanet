using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreamingPlanet.Models;

namespace StreamingPlanet.Data
{
    public class ApplicationDbContext : IdentityDbContext<CinemaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}