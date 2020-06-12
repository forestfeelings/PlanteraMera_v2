using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
using PlanteraMera_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Services
{
    public class SeedService : ISeedService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string apiRootUrl;


        public SeedService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;

            apiRootUrl = _config.GetValue(typeof(string), "seedApiRoot").ToString();
        }

        /// <summary>
        /// Hämtar alla frön asynkront
        /// </summary>
        /// <returns>En samling frön</returns>
        
        public async Task<IEnumerable<Seed>> GetAll()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiRootUrl}Seed/GetAll");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "PlanteraMera_v2");

            var seedApiKey = _config.GetValue<string>("ApiKeys:SeedApiKey");
            request.Headers.Add("ApiKey", seedApiKey);

            // Skicka förfrågan och invänta svar från microservicen
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Läs in innehållet av microservice-responsen och omvandla enligt modellen
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var seeds = await JsonSerializer.DeserializeAsync<List<Seed>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                var orderedSeeds = seeds.OrderBy(x => x.Type);

                return orderedSeeds;
            }
            else
            {
                return new List<Seed>();
            }
        }

        /// <summary>
        /// Hämtar ett frö asynkront med id
        /// </summary>
        /// <param name="id">Id för fröet som ska hämtas</param>
        /// <returns>Ett frö eller null om det inte lyckas</returns>
        
        public async Task<Seed> GetSeedById(Guid id)
        {
            // Hämta specifikt frö (med id)
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiRootUrl}Seed/GetById?id={id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "PlanteraMera_v2");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var seed = await JsonSerializer.DeserializeAsync<Seed>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return seed;
            }
            else
            {
                return null;
            }
        }

    }
}
