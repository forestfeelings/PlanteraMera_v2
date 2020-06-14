using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using SeedsService.Models;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Net.Http;

namespace SeedsService.Tests
{
    /* Checking */
    public class ControllerTests : IClassFixture<SeedFixture>
    {
        private readonly SeedFixture _fixture;

        public ControllerTests(SeedFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]

        public async Task GetAllSeeds_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "SecretSeedKey");
                var response = await client.GetAsync("/api/seed/getall");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]

        public async Task GetSeedById_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "SecretSeedKey");
                var response = await client.GetAsync($"/api/seed/getbyid?id{Guid.Empty}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]

        public async Task GetSeedById_Returns_Seed()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "SecretSeedKey");
                var seedResponse = await client.GetAsync($"/api/seed/getbyid?id={_fixture.seed.SeedId}");

                using (var responseStream = await seedResponse.Content.ReadAsStreamAsync())
                {
                    var seed = await JsonSerializer.DeserializeAsync<Seed>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.seed.SeedId, seed.SeedId);
                }
            }
        }

        [Fact]

        public async Task CreateSeed_Returns_Created_Seed()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "SecretSeedKey");
                Guid seedId = Guid.Empty;
                var payload = JsonSerializer.Serialize(
                    new Seed()
                    {
                        Name = "Testfrö",
                        LatinName = "Test semina",
                        BotanicalFamily = "Semina Seminae",
                        DaysToDevelop = 50,
                        Annuality = "Ettårig",
                        Type = "Frö",
                        Description = "Ett helt vanligt frö.",
                        HeightCm = 10,
                        Price = 35,
                        IsBeginnerSeed = false
                    }
                    );
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/seed/create", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var seed = await JsonSerializer.DeserializeAsync<Seed>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    seedId = seed.SeedId;

                    Assert.NotNull(seed);
                    Assert.NotEqual<Guid>(Guid.Empty, seedId);
                }

                var deleteResponse = await client.DeleteAsync($"/api/seed/delete?id={seedId}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }

        [Fact]

        public async Task DeleteSeed_Returns_Deleted_Id()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("ApiKey", "SecretSeedKey");
                Guid seedId = Guid.Empty;
                var payload = JsonSerializer.Serialize(
                    new Seed()
                    {
                        Name = "Testfrö",
                        LatinName = "Test semina",
                        BotanicalFamily = "Semina Seminae",
                        DaysToDevelop = 50,
                        Annuality = "Ettårig",
                        Type = "Frö",
                        Description = "Ett helt vanligt frö.",
                        HeightCm = 10,
                        Price = 35,
                        IsBeginnerSeed = false
                    }
                    );
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/seed/create", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var seed = await JsonSerializer.DeserializeAsync<Seed>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    seedId = seed.SeedId;
                }

                var deleteResponse = await client.DeleteAsync($"/api/seed/delete?id={seedId}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(seedId, deletedId);
                }
            }
        }
    }
}
