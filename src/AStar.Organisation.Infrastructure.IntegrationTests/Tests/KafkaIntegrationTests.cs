using System.Net;
using System.Text;
using System.Text.Json;
using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.API.Utills;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organisation.Infrastructure.IntegrationTests.Factories;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Tests
{
    [TestFixture]
    public class KafkaIntegrationTests
    {
        private IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        
        [Test, Order(1)]
        public async Task Kafaka_GetEntities_Test()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customers = EntityInitilizeUtill.GetCustomers();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                
                var dtos = customers.Select(e => new CustomerDto
                {
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone
                });

                foreach (var dto in dtos)
                {
                    customerService.Create(dto);
                }
            }
            
            var config = new ProducerConfig { BootstrapServers = "your_kafka_broker_address" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var message = new Message<Null, string> { Value = "Hello Kafka!" };
                var topic = "your_topic_name";

                var deliveryReport = await producer.ProduceAsync(topic, message);
                Console.WriteLine($"Delivered '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
            }
            
            // Act


            // Assert
            
        }
    }
}