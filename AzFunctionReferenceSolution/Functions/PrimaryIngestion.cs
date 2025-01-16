using System;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using AzFunctionReferenceSolution.Models;
using Azure.Messaging.EventHubs;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
            logger.LogInformation("------- Received from Event Hub ------");
            var businessEvents = new List<BusinessEvent>();
            foreach (DefenderScanResult @event in events)
            {
                string? messageBody = @event.Subject;
                logger.LogInformation("Message Subject: {subject}", @event.Subject);
                logger.LogInformation("Message Topic: {topic}", @event.Topic);

                if (@event.Data != null)
                {
                    foreach (var item in @event.Data)
                    {
                        logger.LogInformation("Data: {key} - {value}", item.Key, item.Value);
                    }
                }

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
