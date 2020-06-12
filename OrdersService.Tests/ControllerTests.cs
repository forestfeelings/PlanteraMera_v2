using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using SeedsService.Models;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Net.Http;
using OrdersService.Models;

namespace OrdersService.Tests
{
    /* Checking */
    public class ControllerTests
    {
        [Fact]
        public async Task CreateOrder_Returns_Created_Order()
        {
            using (var client = new TestClientProvider().Client)
            {
                int id = 0;
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {
                        OrderDate = DateTime.Now,
                        UserId = Guid.Empty,
                        OrderId = Guid.Parse("637cd533-23eb-4922-8778-3985b514f125")
                    }
                    );
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/order/create", content);

                Order order = null;

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    id = order.Id;

                    Assert.NotNull(order);
                    Assert.NotEqual<int>(0, id);
                }

                var deleteResponse = await client.DeleteAsync($"/api/order/delete?id={id}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<int>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }

        [Fact]
        public async Task DeleteOrder_Returns_Deleted_Id()
        {
            using (var client = new TestClientProvider().Client)
            {
                int id = 0;
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {
                        OrderDate = DateTime.Now,
                        UserId = Guid.Empty,
                        OrderId = Guid.Parse("637cd533-23eb-4922-8778-3985b514f125")
                    }
                    );
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/order/create", content);

                Order order = null;

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    id = order.Id;

                    Assert.NotNull(order);
                    Assert.NotEqual<int>(0, id);
                }

                var deleteResponse = await client.DeleteAsync($"/api/order/delete?id={id}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<int>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(id, deletedId);
                }
            }
        }
    }
}
