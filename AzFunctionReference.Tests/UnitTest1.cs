using Microsoft.Extensions.Logging.Abstractions;

using AzFunctionReferenceSolution;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using System.Diagnostics;


namespace AzFunctionReference.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var nl = NullLoggerFactory.Instance.CreateLogger<DefenderResultHandler>();
            var foo = new DefenderResultHandler(nl);
            //act

            var eventGridEvent = new EventGridEvent(
                "EventSubject",
                "EventType",
                "1.0",
                "This is the event data");
            //eventOp.EventType = SystemEventNames.EventGridSubscriptionValidation;


            //assert

            foo.Run(eventGridEvent);
        }
    }
}