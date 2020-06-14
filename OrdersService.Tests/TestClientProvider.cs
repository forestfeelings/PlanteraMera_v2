using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Data;
using System;
using System.Net.Http;

namespace OrdersService.Tests
{
    public class TestClientProvider : IDisposable
    {
        public TestServer Server { get; private set; }

        public HttpClient Client { get; private set; }

        /// <summary>
        /// Genererar HttpClient som körs på en TestServer. Servern är konfigurerad med hjälp av en kopia av appsettings.json-filen från test-target projektet.
        /// Detta gör att testservern har tillgång till databasen.
        /// </summary>

        public TestClientProvider()
        {
            // Skapar en serverkonfigurering baserad på appsettings.json-filen
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();
            // Konfigurerar testserverns services till att använda dbcontext och connectionstring från appsettings.json
            webHostBuilder.ConfigureServices(s => s.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));
            // Använder Startup.cs från test-target projektet
            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseConfiguration(configuration);

            Server = new TestServer(webHostBuilder);

            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
