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

        public DbSet<CinemaUser> CinemaUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Guid> CinemaRoomsIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            modelBuilder.Entity<CinemaRoom>().HasData(
                    new CinemaRoom
                    {
                        Id = CinemaRoomsIds[0],
                        RoomName = "1A",
                        MaxCapacity = 12
                    },
                    new CinemaRoom
                    {
                        Id = CinemaRoomsIds[1],
                        RoomName = "1B",
                        MaxCapacity = 12
                    },
                    new CinemaRoom
                    {
                        Id = CinemaRoomsIds[2],
                        RoomName = "2A",
                        MaxCapacity = 12
                    },
                    new CinemaRoom
                    {
                        Id = CinemaRoomsIds[3],
                        RoomName = "2B",
                        MaxCapacity = 12
                    }
                );


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<StreamingPlanet.Models.CinemaRoom> CinemaRoom { get; set; }
    }
}