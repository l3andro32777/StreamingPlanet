using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StreamingPlanet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlanetTest
{
    public class ApplicationDbContextFixture : IDisposable
    {
        public ApplicationDbContext DbContext { get; private set; }

        public ApplicationDbContextFixture()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;
            DbContext = new ApplicationDbContext(options);

            DbContext.Database.EnsureCreated();

            DbContext.SaveChanges();
        }

        public void Dispose() => DbContext.Dispose();
    }
}
