namespace IMRReader.Domain.Models
{
    public record MeasurementMetrics
    {
        public required double[] XData { get; set; }
        public required double[] YData { get; set; }
    }
}
