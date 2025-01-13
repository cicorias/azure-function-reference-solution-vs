// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using System.Text;
using AzFunctionReferenceSolution.Models;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctionReferenceSolution.Functions
{
    public class DefenderResultHandler(ILogger<DefenderResultHandler> logger)
    {
        [Function(nameof(DefenderResultHandler))]
        [EventHubOutput("ns1", Connection = "APP_TARGET_EVENT_HUB")]
        public DefenderScanResult Run([EventGridTrigger] EventGridEvent resultEvent)
        {
            logger.LogInformation("Received event with subject: {subject}", resultEvent.Subject);

            //var ev = new EventData(resultEvent.Data);  //Encoding.UTF8.GetBytes(resultEvent.Data.ToString()))
            ////{
            ////    ContentType = "application/json",
            ////};

            //ev.Properties["siteid"] = "ABC1234";

            var rv = new DefenderScanResult
            {
                Topic = resultEvent.Topic,
                Subject = resultEvent.Subject,
                EventType = resultEvent.EventType,
                EventTime = DateTimeOffset.UtcNow,
                Data = new Dictionary<string, object>
                {
                    { "siteid", "ABC1234" }
                }
            };

            return rv;
        }
    }
}
