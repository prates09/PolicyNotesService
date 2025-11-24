using System.Net;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolicyNotesService.Data;
using Xunit;

namespace PolicyNotesService.Tests.IntegrationTests
{
    public class NotesEndpointsIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private record PolicyNoteCreateDto(string PolicyNumber, string Note);
        private record PolicyNoteDto(int Id, string PolicyNumber, string Note);

        public NotesEndpointsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the real DB
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<PolicyNotesDbContext>)
                    );

                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Add InMemory DB for integration testing
                    services.AddDbContext<PolicyNotesDbContext>(options =>
                        options.UseInMemoryDatabase("IntegrationTestDb"));
                });
            });
        }

        private HttpClient CreateClient() => _factory.CreateClient();


        // 1️⃣ POST /notes → 201 Created
        [Fact]
        public async Task PostNotes_Returns201Created()
        {
            var client = CreateClient();
            var dto = new PolicyNoteCreateDto("POL-200", "Integration test note");

            var response = await client.PostAsJsonAsync("/notes", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<PolicyNoteDto>();
            Assert.NotNull(created);
            Assert.True(created!.Id > 0);
        }


        // 2️⃣ GET /notes → 200 OK
        [Fact]
        public async Task GetNotes_Returns200Ok()
        {
            var client = CreateClient();

            await client.PostAsJsonAsync("/notes",
                new PolicyNoteCreateDto("POL-300", "Another note"));

            var response = await client.GetAsync("/notes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        // 3️⃣ GET /notes/{id} → 200 OK (when found)
        [Fact]
        public async Task GetNoteById_Returns200Ok_WhenFound()
        {
            var client = CreateClient();

            var postResponse = await client.PostAsJsonAsync("/notes",
                new PolicyNoteCreateDto("POL-400", "Specific note"));

            var created = await postResponse.Content.ReadFromJsonAsync<PolicyNoteDto>();
            Assert.NotNull(created);

            var getResponse = await client.GetAsync($"/notes/{created!.Id}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }


        // 4️⃣ GET /notes/{id} → 404 NotFound (when missing)
        [Fact]
        public async Task GetNoteById_Returns404NotFound_WhenMissing()
        {
            var client = CreateClient();

            var response = await client.GetAsync("/notes/99999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
