using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PlanteraMera_v2.Models;
using PlanteraMera_v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string apiRootUrl;

        public OrderService(IHttpClientFactory clientFactory, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _clientFactory = clientFactory;
            _config = config;
            _userManager = userManager;
            apiRootUrl = _config.GetValue(typeof(string), "orderApiRoot").ToString();
        }

        public async Task<bool> PlaceOrder(Order order)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{apiRootUrl}Order/Create");

            var postJson = JsonSerializer.Serialize(order);

            request.Headers.Add("User-Agent", "PlanteraMera_v2");
            request.Content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
