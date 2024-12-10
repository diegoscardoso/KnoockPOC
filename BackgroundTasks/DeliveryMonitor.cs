using System;
using System.Text;
using KoockPOC.Core.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BackgroundTasks
{
    public class DeliveryMonitor
    {
        private readonly ILogger _logger;
        private readonly HttpClient HttpClient = new HttpClient();
        private readonly string DeliveryHubEndpoint = "https://localhost:7042/notify-pending-delivery";

        public DeliveryMonitor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DeliveryMonitor>();
        }

        [Function(nameof(DeliveryMonitor))]
        public async Task Run([TimerTrigger("*/10 * * * * *", RunOnStartup = true)] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Get All pending Deliveries (Repository, Service, CQRS, etc) 
            
            // Pending Delivery Example
            var delivery = new Delivery 
            { 
                DeliveryID = Guid.NewGuid(), 
                DeliveryStatus = KoockPOC.Core.Enums.DeliveryStatus.Pending 
            };
            
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(delivery), Encoding.UTF8, "application/json");
              
                var response = await HttpClient.PostAsync(DeliveryHubEndpoint, content);
                response.EnsureSuccessStatusCode();

                _logger.LogInformation("Delivery notification sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending delivery notification: {ex.Message}");
            }
        }
    }
}
