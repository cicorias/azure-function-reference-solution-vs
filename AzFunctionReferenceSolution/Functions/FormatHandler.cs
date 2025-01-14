using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctionReferenceSolution.Functions
{
    public class FormatHandler(ILogger<FormatHandler> logger)
    {
        [Function(nameof(FormatHandler))]
        public async Task Run(
            [ServiceBusTrigger("%APP_FIRST_QUEUE%", Connection = "APP_TARGET_SERVICE_BUS_QUEUE")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            logger.LogInformation("Message ID: {id}", message.MessageId);
            logger.LogInformation("Message Body: {body}", message.Body);
            logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
