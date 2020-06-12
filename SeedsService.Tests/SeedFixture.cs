using SeedsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeedsService.Tests
{
    public class SeedFixture : IDisposable
    {
        public Seed seed { get; private set; }        

        public SeedFixture()
        {
            seed = Initialize().Result;
        }

        private async Task<Seed> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
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
                    var createdSeed = await JsonSerializer.DeserializeAsync<Seed>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return createdSeed;
                }
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/seed/delete?id={seed.SeedId}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
