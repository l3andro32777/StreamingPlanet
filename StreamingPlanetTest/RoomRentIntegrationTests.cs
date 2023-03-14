using Microsoft.AspNetCore.Mvc.Testing;
using StreamingPlanet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlanetTest
{
    public class RoomRentIntegrationTests : IClassFixture<ApplicationDbContextFixture>
    {
        private ApplicationDbContext _context;
        public RoomRentIntegrationTests(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;
        }

        [Fact]
        public async Task Create_Room_Without_Login_Redirects_To_Login_Page()
        {
            //Arrange
            var webAppFactory = new WebApplicationFactory<Program>();

            var _httpClient = webAppFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "/CinemaRooms/Create");

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal("/Identity/Account/Login", response.RequestMessage.RequestUri.LocalPath);
        }
    }
}
