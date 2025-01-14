// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using System.Text;
using AzFunctionReferenceSolution.Models;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctionReferenceSolution.Functions
{
    public class DefenderResultHandler(ILogger<DefenderResultHandler> logger)
    {
        [Function(nameof(DefenderResultHandler))]
        [EventHubOutput("ns1", Connection = "APP_TARGET_EVENT_HUB")]
        public async Task<DefenderScanResult> Run(
            [EventGridTrigger] EventGridEvent resultEvent,
            [BlobInput("samples-workitems/{data.scanResultType}")] string myBlob,
            [BlobInput("samples-workitems")] BlobContainerClient bcClient,
            FunctionContext context)
        {
            logger.LogInformation("Received event with subject: {subject}", resultEvent.Subject);

            var foo = context.BindingContext.BindingData;

            var foo2 = bcClient.GetBlobClient("sample1.txt"); // resultEvent.Subject);
            var foo3 = await foo2.DownloadContentAsync();

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


        [Function(nameof(AlternativeEventHandler))]
        public void AlternativeEventHandler(
            [EventGridTrigger] MyEventType eventGridEvent,
            FunctionContext context)
        {
            logger.LogInformation("Received event with subject: {subject}", eventGridEvent.Subject);
            var foo = context.BindingContext.BindingData;
        }



        [Function(nameof(AltnerativeWebHookHandler))]
        public void AltnerativeWebHookHandler(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "webhook")]
                Microsoft.Azure.Functions.Worker.Http.HttpRequestData req,
                FunctionContext context)
        {
            var foo = context.BindingContext.BindingData;
        }

    }

    public class MyEventType
    {
        public string? Id { get; set; }

        public string? Topic { get; set; }

        public string? Subject { get; set; }

        public string? EventType { get; set; }

        public DateTime EventTime { get; set; }

        public IDictionary<string, object>? Data { get; set; }
    }
}
