using System;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using AzFunctionReferenceSolution.Models;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctionReferenceSolution.Functions
{
    public class PrimaryIngestion(ILogger<PrimaryIngestion> logger)
    {
        [Function(nameof(PrimaryIngestion))]
        [ServiceBusOutput("%APP_FIRST_QUEUE%", Connection = "APP_TARGET_SERVICE_BUS_QUEUE")]
        public BusinessEvent[] Run([EventHubTrigger("ns1", Connection = "APP_TARGET_EVENT_HUB", ConsumerGroup = "primaryConsumerGroup")] DefenderScanResult[] events)
        {
            var businessEvents = new List<BusinessEvent>();
            foreach (DefenderScanResult @event in events)
            {
                string? messageBody = @event.Subject;
                logger.LogInformation("Event Body: {body}", messageBody);

                businessEvents.Add(new BusinessEvent
                {
                    Topic = @event.Topic,
                    Subject = @event.Subject,
                    EventType = @event.EventType,
                    EventTime = DateTimeOffset.UtcNow,
                    Data = @event.Data
                });
            }


            return [.. businessEvents];



        }
    }
}
