// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctionReferenceSolution
{
    public class DefenderResultHandler(ILogger<DefenderResultHandler> logger)
    {
        [Function(nameof(DefenderResultHandler))]
        public void Run([EventGridTrigger] CloudEvent cloudEvent)
        {
            logger.LogInformation("Event type: {type}, Event subject: {subject}", cloudEvent.Type, cloudEvent.Subject);
        }
    }
}
