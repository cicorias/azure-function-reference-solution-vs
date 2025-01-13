namespace AzFunctionReferenceSolution.Models
{
    public class DefenderScanResult
    {
        public string? Topic { get; set; }

        public string? Subject { get; set; }

        public string? EventType { get; set; }

        public DateTimeOffset EventTime { get; set; }

        public IDictionary<string, object>? Data { get; set; }
    }
}
