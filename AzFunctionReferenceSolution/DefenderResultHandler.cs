// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctionReferenceSolution
{
    public class DefenderResultHandler(ILogger<DefenderResultHandler> logger)
    {
        [Function(nameof(DefenderResultHandler))]
        [EventGridOutput()]
        public void Run([EventGridTrigger] EventGridEvent resultEvent)
        {
            logger.LogInformation("Received event with subject: {subject}", resultEvent.Subject);
            //logger.LogInformation("Event type: {type}, Event subject: {subject}", cloudEvent.Type, cloudEvent.Subject);
        }
    }
}
